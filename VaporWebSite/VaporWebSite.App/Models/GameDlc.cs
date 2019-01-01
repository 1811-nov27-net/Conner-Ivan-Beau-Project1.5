using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaporWebSite.App.Models
{
    public class GameDlc
    {
        public Game Game { get; set; }
        public Developer Developer { get; set; }
        public List<Dlc> Dlcs { get; set; }
    }
}
