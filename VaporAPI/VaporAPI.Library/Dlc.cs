using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporAPI.Library
{
    public class Dlc
    {
        public int Dlcid { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 1000)]
        
        public decimal Price { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}
