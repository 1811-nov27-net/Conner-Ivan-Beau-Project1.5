using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public class FullUserGame
    {
        public UserGame UserGame { get; set; }

        public Game Game { get; set; }

        public List<Dlc> Dlcs { get; set; }

        public Developer Developer { get; set; }
    }
}
