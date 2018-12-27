using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporAPI.Library
{
    public class GameTag
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}
