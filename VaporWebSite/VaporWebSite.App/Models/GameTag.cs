using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporWebSite.App.Models
{
    public class GameTag
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}
