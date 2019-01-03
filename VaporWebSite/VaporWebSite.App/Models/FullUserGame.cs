using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaporWebSite.App.Models
{
    public class FullUserGame
    {
        public UserGame UserGame { get; set; }

        public Game Game { get; set; }

        public List<Dlc> Dlcs { get; set; }

        public Developer Developer { get; set; }

        public bool Selected { get; set; }

        public List<FullDlc> SelectDlcs { get; set; }
    }
}
