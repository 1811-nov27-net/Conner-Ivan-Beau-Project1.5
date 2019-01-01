using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporWebSite.App.Models
{
    public class Dlc
    {
        public int Dlcid { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z | 0-9]{1,100}$",
            ErrorMessage = "Only alphanumeric characters are allowed.")]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]     
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?",
            ErrorMessage = "Please type a decimal number from 0 to 1000")]
        public decimal Price { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}
