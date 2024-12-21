// See https://aka.ms/new-console-template for more information
using HBTaskRunner;
using System.Threading;

Console.WriteLine("Beginning");

iCloudDownloaderService service = new iCloudDownloaderService();

while(true){
    service.ProcessAlbums();
    Thread.Sleep(60000);
}

Console.WriteLine("Ending");

