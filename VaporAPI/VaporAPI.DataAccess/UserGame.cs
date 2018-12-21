using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class UserGame
    {
        public int GameId { get; set; }
        public string UserName { get; set; }
        public DateTime DatePurchased { get; set; }
        public int? Score { get; set; }
        public string Review { get; set; }

        public virtual Game Game { get; set; }
        public virtual User UserNameNavigation { get; set; }
    }
}
