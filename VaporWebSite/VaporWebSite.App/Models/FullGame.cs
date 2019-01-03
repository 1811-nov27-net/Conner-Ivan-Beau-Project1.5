using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaporWebSite.App.Models
{
    public class FullGame
    {
        public List<Developer> Developers { get; set; }

        public List<FilterTag> Tags { get; set; }

        public Game Game { get; set; }

        public decimal Score { get; set; }

        public Boolean Selected { get; set; }
    }
}
