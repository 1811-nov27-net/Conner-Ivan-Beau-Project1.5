using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class User
    {
        public User()
        {
            UserDlc = new HashSet<UserDlc>();
            UserGame = new HashSet<UserGame>();
        }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public decimal? Wallet { get; set; }
        public string CreditCard { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<UserDlc> UserDlc { get; set; }
        public virtual ICollection<UserGame> UserGame { get; set; }
    }
}
