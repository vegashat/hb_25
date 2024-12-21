using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class Setting
    {
        public int BoardId { get; set; }
        public string Key {get; set;}
        public string Value {get; set;}
    }
}