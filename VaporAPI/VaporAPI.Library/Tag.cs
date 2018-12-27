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
        public string GenreName { get; set; }
    }
}
