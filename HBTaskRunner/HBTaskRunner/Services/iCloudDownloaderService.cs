using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBTaskRunner.Models;
using HBTaskRunner.Repositores;
using HBTaskRunner.Util;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using Dapper;

namespace HBTaskRunner
{
    public class iCloudDownloaderService
    {
        internal class Response
        {

            public string userLastName { get; set; }
            public string streamCtag { get; set; }
            public string itemsReturned { get; set; }
            public string userFirstName { get; set; }
            public string streamName { get; set; }
            public List<Photo> photos { get; set; }

        }
        internal class Photo
        {
            public string batchGuid { get; set; }
            public string contributorLastName { get; set; }
            public DateTime batchDateCreated { get; set; }
            public DateTime dateCreated { get; set; }
            public string contributorFirstName { get; set; }
            public string photoGuid { get; set; }
            public string contributorFullName { get; set; }
            public string width { get; set; }
            public string caption { get; set; }
            public string height { get; set; }
            public IEnumerable<Derivative> Sizes { get; set; }
        }

        internal class Derivative
        {

            public string key { get; set; }
            public string checkSum { get; set; }
            public int fileSize { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        HBRepository? _repository;
        private static readonly Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Origin", "https://www.icloud.com" },
            { "Accept-Language", "en-US,en;q=0.8" },
            { "User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36" },
            { "Accept", "*/*" },
            { "Referer", "https://www.icloud.com/sharedalbum/" },
            { "Connection", "keep-alive" }
        };

        public iCloudDownloaderService()
        {
            _repository = new HBRepository();
        }

        const string PHOTO_DIRECTORY = "/images/";

        public void ProcessAlbums()
        {
            //Make sure the photos directory exits
            if (!Directory.Exists(PHOTO_DIRECTORY))
            {
                Directory.CreateDirectory(PHOTO_DIRECTORY);
            }

            var albums = _repository?.GetActivePhotoAlbums();

            foreach (var album in albums)
            {
                ProcessAlbum(album);
            }
        }

        private void ProcessAlbum(PhotoAlbum album)
        {
            var downloadedPhotos = _repository?.GetCurrentPhotos(album.BoardId).Select(p => p.Guid);

            var albumPhotos = GetAlbumGuids(album.Token);
            var albumPhotosGuids = albumPhotos.Select(p => p.photoGuid);

            var photosToDownload = albumPhotosGuids.Except(downloadedPhotos);

            var chunks = photosToDownload.Chunk(25);

            foreach (var chunk in chunks)
            {
                var photos = GetPhotoUrl(album.Token, chunk, albumPhotos);
                foreach (var photo in photos)
                {
                    try
                    {
                        if (DownloadPhoto(photo))
                        {
                            _repository.InsertPhoto(album.BoardId, photo.Guid);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private IEnumerable<Photo> GetAlbumGuids(string token)
        {
            string url = GetBaseUrl(token);
            url += "webstream";

            string dataString = JsonConvert.SerializeObject(new { streamCtag = (object)null });

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));

                Console.WriteLine($"url: {url}");
                HttpResponseMessage response = client.PostAsync(url, new StringContent(dataString, Encoding.UTF8, "application/json")).Result;

                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    dynamic? responseAsJson = JsonConvert.DeserializeObject(responseData);
                    var data = JsonConvert.DeserializeObject<Response>(responseData);

                    var photos = new List<Photo>();

                    foreach (var photoData in responseAsJson.photos)
                    {
                        Photo photo = new Photo()
                        {
                            batchDateCreated = photoData.batchDateCreated,
                            photoGuid = photoData.photoGuid,
                            height = photoData.height,
                            width = photoData.width
                        };
                        var sizes = new List<Derivative>();
                        int i = 0;
                        foreach (var size in photoData.derivatives)
                        {
                            var sizeData = photoData.derivatives[size.Name];
                            sizes.Add(new Derivative()
                            {
                                key = size.Name,
                                checkSum = sizeData.checksum,
                                height = sizeData.width,
                                width = sizeData.height,
                            });
                            i++;
                        }
                        photo.Sizes = new List<Derivative>();
                        photo.Sizes.AsList().AddRange(sizes);

                        photos.Add(photo);
                    }
                    return photos;

                }
                else
                {
                    throw new Exception("Failed to get response");
                }
            }
        }

        private IEnumerable<PhotoInfo> GetPhotoUrl(string token, IEnumerable<string> guids, IEnumerable<Photo> photos)
        {
            var url = GetBaseUrl(token);
            url += "webasseturls";

            IList<PhotoInfo> photoInfos = new List<PhotoInfo>();
            string dataString = JsonConvert.SerializeObject(new { photoGuids = guids });

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = client.PostAsync(url, new StringContent(dataString, Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    dynamic? data = JsonConvert.DeserializeObject(responseData);

                    if (data != null)
                    {
                        foreach (var photo in photos)
                        {
                            if (photo != null)
                            {
                                var urlInfo = data.items[photo.Sizes.OrderByDescending(s => s.fileSize).First().checkSum];
                                if (urlInfo != null)
                                {
                                    var info = new PhotoInfo()
                                    {
                                        Guid = photo.photoGuid,
                                        Url = $"https://{urlInfo.url_location}{urlInfo.url_path}"
                                    };
                                    photoInfos.Add(info);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Failed to get response");
                }
            }

            return photoInfos;
        }
        private bool DownloadPhoto(PhotoInfo photo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var stream = client.GetStreamAsync(photo.Url))
                    {
                        using (var fs = new FileStream($"{PHOTO_DIRECTORY}{photo.Guid}.jpg", FileMode.OpenOrCreate))
                        {
                            stream.Result.CopyTo(fs);
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false; ;
            }

        }

        private string GetBaseUrl(string token)
        {
            const string BASE_62_CHAR_SET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            int Base62ToInt(string e)
            {
                int t = 0;
                for (int n = 0; n < e.Length; n++)
                {
                    t = t * 62 + BASE_62_CHAR_SET.IndexOf(e[n]);
                }
                return t;
            }

            string e = token;
            char t = e[0];
            int n = t == 'A' ? Base62ToInt(e[1].ToString()) : Base62ToInt(e.Substring(1, 2));
            int i = e.IndexOf(';');
            string r = e;
            string s = string.Empty;

            if (i >= 0)
            {
                s = e.Substring(i + 1);
                r = r.Replace(";" + s, "");
            }

            int serverPartition = n;

            string baseUrl = "https://p";

            baseUrl += serverPartition < 10 ? "0" + serverPartition : serverPartition.ToString();
            baseUrl += "-sharedstreams.icloud.com";
            baseUrl += "/";
            baseUrl += token;
            baseUrl += "/sharedstreams/";

            return baseUrl;
        }
    }
}