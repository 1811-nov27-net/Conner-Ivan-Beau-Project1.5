using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class Tag
    {
        public Tag()
        {
            GameTag = new HashSet<GameTag>();
        }

        public int TagId { get; set; }
        public string GenreName { get; set; }

        public virtual ICollection<GameTag> GameTag { get; set; }
    }
}
