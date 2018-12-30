using System;
using System.Collections.Generic;
using System.Text;
using VaporWebSite.App.Models;

namespace VaporWebSite.App.Models
{
    public class UserGame
    {
        public User User { get; set; }

        public Game Game { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Review { get; set; }

        public int Score { get; set; }


    }
}
