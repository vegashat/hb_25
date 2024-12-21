using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class Board
    {
       public int BoardId { get; set; } 
       public string Name { get; set; } 
       public bool IsActive { get; set; } 
       public string City { get; set; } 
       public int Zip { get; set; } 
       public decimal Latitude {get; set;}
       public decimal Longitude {get; set;}
    }
}