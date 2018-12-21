using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class Dlc
    {
        public Dlc()
        {
            UserDlc = new HashSet<UserDlc>();
        }

        public int Dlcid { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public virtual ICollection<UserDlc> UserDlc { get; set; }
    }
}
