using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporAPI.Library
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        [RegularExpression(@"[^a-zA-Z | 0-9\s],{1,100}",
            ErrorMessage = "Only alphanumeric characters are allowed.")]
        public string Name { get; set; }

        private decimal _price; 

        [Required]
        //[Range(0, 1000)]
        [RegularExpression(@"^[0-9 | '.']{0,1000}$",
            ErrorMessage = "Please type a decimal number between 0 and 1000, inclusive.")]
        public decimal Price
        {
            get { return _price; }
            set
            {
                try
                {
                    if (value >= 0)
                    {
                        _price = value;
                    }
                    else
                    {
                        throw new ValidationException();
                    }
                }
                catch
                {
                    Console.WriteLine("Insufficient funds");
                }

            }
        }

        public string Description { get; set; }

        public int? DeveloperId { get; set; }

        public string Image { get; set; }

        public string Trailer { get; set; }
    }
}
