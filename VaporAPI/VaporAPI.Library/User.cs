using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public class User
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

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
