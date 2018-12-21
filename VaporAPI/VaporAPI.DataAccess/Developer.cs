using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class Developer
    {
        public Developer()
        {
            Game = new HashSet<Game>();
        }

        public int DeveloperId { get; set; }
        public string Name { get; set; }
        public DateTime FoundingDate { get; set; }
        public string Website { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
