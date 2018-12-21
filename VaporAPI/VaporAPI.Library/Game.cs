using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public class Game
    {
        public int GameId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int? DeveloperId { get; set; }

        public string Image { get; set; }

        public string Trailer { get; set; }
    }
}
