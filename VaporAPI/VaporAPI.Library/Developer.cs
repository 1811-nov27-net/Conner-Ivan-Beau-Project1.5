﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporAPI.Library
{
    public class Developer
    {
        public int DeveloperId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,100}$",
            ErrorMessage ="Only letters are allowed.")]
        public string Name { get; set; }

        [Required]
        public DateTime FoundingDate { get; set; }

        [Required]
        public string Website { get; set; }
    }
}
