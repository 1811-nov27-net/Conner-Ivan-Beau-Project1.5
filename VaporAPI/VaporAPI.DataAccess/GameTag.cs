using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class GameTag
    {
        public int GameId { get; set; }
        public int TagId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
