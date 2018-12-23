using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public class UserGame
    {
        public User User { get; set; }

        public Game Game { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }


    }
}
