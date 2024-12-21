using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBTaskRunner.Models
{
    public class Photo
    {
        public int Board_Id { get; set; }
        public string? Guid { get; set; }
        public Boolean HasBeenDisplayed { get; set; }
    }
}