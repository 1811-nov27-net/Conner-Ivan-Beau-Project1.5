using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporAPI.Library
{
    public class User
    {
        [Required]
        public string UserName { get; set; }


        // Regular Expression Attribute allows only uppercase and lowercase letters.
        // Also enforeces a range of (1, 100)
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
         ErrorMessage = "Characters are not allowed.")]
        public string FirstName { get; set; }

        // Regular Expression Attribute allows only uppercase and lowercase letters.
        // Also enforeces a range of (1, 100)
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
         ErrorMessage = "Characters are not allowed.")]
        public string LastName { get; set; }

        [Required]
        
        public string Password { get; set; }

        public decimal? Wallet { get; set; }

        public string CreditCard { get; set; }

        public bool Admin { get; set; }

        public bool SuggestTags(List<Game> game)
        {
            return false;

        }
    }
}
