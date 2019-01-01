using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaporWebSite.App.Models
{
    public class FullDlc
    {
        public Dlc Dlc { get; set; }

        public Game Game { get; set; }

        public List<Game> AllGames { get; set; }
    }
}
