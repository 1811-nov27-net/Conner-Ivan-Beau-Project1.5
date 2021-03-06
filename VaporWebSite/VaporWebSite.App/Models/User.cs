﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporWebSite.App.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }


        // Regular Expression Attribute allows only uppercase and lowercase letters.
        // Also enforeces a range of (1, 100)
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
         ErrorMessage = "Only letters allowed.")]
        public string FirstName { get; set; }

        // Regular Expression Attribute allows only uppercase and lowercase letters.
        // Also enforeces a range of (1, 100)
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
         ErrorMessage = "Only letters allowed")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z | 0-9\s]{1,100}$",
         ErrorMessage = "Only alphanumeric characters are allowed.")]
        public string Password { get; set; }

        [Range(0, 1000000)]
        public decimal? Wallet { get; set; }

        //should this really be a string?
        //[RegularExpression(@"^[0-9\s]{16, 16}$",
        //    ErrorMessage ="Not a valid Credit Card Number")]
        // could make it an integer?
        public string CreditCard { get; set; }

        [Required]
        public bool Admin { get; set; }

    }
}
