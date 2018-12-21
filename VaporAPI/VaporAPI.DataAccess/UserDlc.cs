using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class UserDlc
    {
        public string UserName { get; set; }
        public int Dlcid { get; set; }

        public virtual Dlc Dlc { get; set; }
        public virtual User UserNameNavigation { get; set; }
    }
}
