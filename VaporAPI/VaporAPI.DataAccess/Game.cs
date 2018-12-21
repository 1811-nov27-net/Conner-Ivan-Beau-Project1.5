using System;
using System.Collections.Generic;

namespace VaporAPI.DataAccess
{
    public partial class Game
    {
        public Game()
        {
            Dlc = new HashSet<Dlc>();
            GameTag = new HashSet<GameTag>();
            UserGame = new HashSet<UserGame>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? DeveloperId { get; set; }
        public string Image { get; set; }
        public string Trailer { get; set; }

        public virtual Developer Developer { get; set; }
        public virtual ICollection<Dlc> Dlc { get; set; }
        public virtual ICollection<GameTag> GameTag { get; set; }
        public virtual ICollection<UserGame> UserGame { get; set; }
    }
}
