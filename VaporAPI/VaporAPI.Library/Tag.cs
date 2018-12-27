using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporAPI.Library
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,100}$",
            ErrorMessage = "Only letters are allowed")]
        public string GenreName { get; set; }
    }
}
