using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaporWebSite.App.Models
{
    public class GameScore
    {
        public Game Game { get; set; }

        public decimal Score { get; set; }
    }
}
