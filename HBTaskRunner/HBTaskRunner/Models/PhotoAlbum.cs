using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBTaskRunner.Models
{
    public class PhotoAlbum
    {
        public int BoardId { get; set; }
        public string Guid { get; set; }
        public string? Token { get; set; }
    }
}