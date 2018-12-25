using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaporAPI.Library;

namespace VaporAPI.DataAccess
{
    public class Repository : IRepository
    {
        private VaporDBContext _db;

        public Repository(VaporDBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool AddDeveloper(Library.Developer developer)
        {
            bool success = true;
            try
            {
                Developer developerDB = Mapper.Map(developer);
                _db.Add(developerDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }

        }

        public bool AddDlc(Library.Dlc dlc)
        {
            bool success = true;
            try
            {
                var dlcDB = Mapper.Map(dlc);
                _db.Add(dlcDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool AddGame(Library.Game game)
        {
            bool success = true;
            try
            {
                var gameDB = Mapper.Map(game);
                _db.Add(gameDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool AddReview(Library.UserGame review)
        {
            bool success = true;
            try
            {
                User user = _db.User.Find(review.User.UserName);
                UserGame usergame = user.UserGame.Where(g => g.GameId == review.Game.GameId).First();
                usergame.Score = review.Score;
                usergame.Review = review.Review;
                _db.Update(user);
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
            
        }

        public bool AddTag(Library.Tag tag)
        {
            bool success = true;
            try
            {
                var tagDB = Mapper.Map(tag);
                _db.Add(tagDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool AddUser(Library.User user)
        {
            bool success = true;
            try
            {
                var userDB = Mapper.Map(user);
                _db.Add(user);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool AddUserGame(Library.UserGame userGame)
        {
            try
            {
                _db.UserGame.Add(Mapper.Map(userGame));
                _db.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public bool UpdateUserGame(Library.UserGame userGame)
        {
            try { 
                //_db.UserGame.First(a => a.UserName == userGame.User.UserName && a.GameId == userGame.Game.GameId);
                _db.UserGame.Update(Mapper.Map(userGame));
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool DeleteUserGame(string username, int gameid)
        {
            try
            {
                _db.UserGame.Remove(_db.UserGame.First(a => a.UserName == username && a.GameId == gameid));
                return true;
            }
            catch
            {

                return false;
            }
        }

        public bool DeleteDeveloper(int id)
        {
            bool success = true;
            try
            {
                _db.Remove(_db.Developer.Where(d => d.DeveloperId == id).First());
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }   
        }

        public bool DeleteDlc(int id)
        {
            bool success = true;
            try
            {
                _db.Remove(_db.Dlc.Where(d => d.Dlcid == id).First());
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool DeleteGame(int id)
        {
            bool success = true;
            try
            {
                _db.Remove(_db.Game.Where(d => d.GameId == id).First());
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool DeleteReview(Library.UserGame review)
        {
            DataAccess.UserGame ug = _db.UserGame.Where(a => a.UserName == review.User.UserName && a.GameId == review.Game.GameId).First();
            if (ug != null)
            {
                ug.Score = review.Score;
                ug.Review = review.Review;
                return true;
            }
            return false;
        }

        public bool DeleteTag(string genrename)
        {
            bool success = true;
            try
            {
                _db.Remove(_db.Tag.Where(d => d.GenreName == genrename).First());
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool DeleteUser(string username)
        {
            bool success = true;
            try
            {
                _db.Remove(_db.User.Where(d => d.UserName == username).First());
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public Library.Developer GetDeveloper(int developerid)
        {
            DataAccess.Developer dev = _db.Developer.Find(developerid);
            return dev == null ? null : Mapper.Map(dev);
        }

        public ICollection<Library.Developer> GetDevelopers()
        {
            return _db.Developer.AsNoTracking().Select(a => Mapper.Map(a)).ToList();
        }

        public Library.Dlc GetDlc(int dlcid)
        {
            DataAccess.Dlc dlc = _db.Dlc.Find(dlcid);
            return dlc == null ? null : Mapper.Map(dlc);
        }

        public Library.Game GetGame(int id)
        {

            Game game = _db.Game.Where(g => g.GameId == id).FirstOrDefault();
            Library.Game gameLib = Mapper.Map(game);
            return gameLib;
        }

        public ICollection<Library.Dlc> GetGameDlcs(int gameid)
        {
            ICollection<DataAccess.Dlc> dlcs = _db.Dlc.Where(a => a.GameId == gameid).ToList();
            return dlcs == null ? null : Mapper.Map(dlcs).ToList();
        }

        public ICollection<Library.Game> GetGames(params int[] sort)
        {
            throw new NotImplementedException();
            // no parameters passes represents a sort of gameId
            //if (sort.Length == 0)
            //{
            //    ICollection<Game> games = (ICollection<Game>) _db.Game.OrderBy(g => g.GameId);
            //    var gamesLib = Mapper.Map(games);
            //    return games;
            //}
            //else
            //{
            //    // a value of 1 represent a sort by GameName 
            //    if (sort[1] == 1)
            //    {
            //        IEnumerable<Game> games = (IEnumerable<Game>)_db.Game.OrderBy(g => g.Name);
            //    }

            //    // a value of 2 represent a sort by price (cheapest first)
            //    else if (sort[1] == 2)
            //    {
            //        IEnumerable<Game> games = (IEnumerable<Game>)_db.Game.OrderBy(g => g.Price);
            //    }

            //    // a value of 3 represent a sort by price (most expensive first)
            //    else if (sort[1] == 3)
            //    {
            //        IEnumerable<Game> games = (IEnumerable<Game>)_db.Game.OrderByDescending(g => g.Price);
            //    }
            //}
        }

        //wait to implement these Review functions
        public ICollection<Library.UserGame> GetReviewbyGame(int id)
        {
            throw new NotImplementedException();
        }

        public Library.UserGame GetReviewbyUser(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.UserGame> GetReviewsByGame(int id, params int[] sort)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.UserGame> GetReviewsbyUser(string username, params int[] sort)
        {
            throw new NotImplementedException();
        }





        public Library.Tag GetTag(int tagid)
        {
            DataAccess.Tag tag = _db.Tag.Find(tagid);
            return tag == null ? null : Mapper.Map(tag);
        }

        public ICollection<Library.Tag> GetTags()
        {
            ICollection<DataAccess.Tag> tags = _db.Tag.AsNoTracking().ToList();
            return tags == null ? null : Mapper.Map(tags).ToList();
        }

        public Library.User GetUser(string username)
        {
            var user = _db.User.Where(u => u.UserName == username).FirstOrDefault();
            var userLib = Mapper.Map(user);
            return userLib;
        }

        public Library.UserGame GetUserGame(string username, int gameid)
        {
            //shouldn't ever be multiple elements returned
            DataAccess.UserGame ug = _db.UserGame.Where(a => a.UserName == username && a.GameId == gameid).First();
            return ug == null ? null : Mapper.Map(ug);
        }

        public ICollection<Library.UserGame> GetUserGames(string username)
        {
            ICollection<DataAccess.UserGame> ugs = _db.UserGame.Where(a => a.UserName == username).ToList();
            return ugs == null ? null : Mapper.Map(ugs).ToList();
        }

        public ICollection<Library.User> GetUsers(params int[] sort)
        {
            //sort not  implemented yet
            List<DataAccess.User> users = _db.User.AsNoTracking().ToList();
            return users == null ? null : Mapper.Map(users).ToList();
        }

        //not sure we will need these

        public ICollection<Library.User> GetUsersbyDlc(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.User> GetUsersbyGame(int id)
        {
            throw new NotImplementedException();
        }

        //idea: get a dictionary of all the tags for the games the user owns, find the most popular tags
        // and look for games that have that tag that the user does not own
        public Library.Game SuggestGamebyuser(string username)
        {
            throw new NotImplementedException();
        }


        public bool UpdateDeveloper(Library.Developer developer)
        {
            DataAccess.Developer dev = Mapper.Map(developer);
            if (_db.Developer.Find(developer.DeveloperId) != null)
            {
                _db.Developer.Update(dev);
                _db.SaveChanges();
                return true;
            }
            return false;
            
        }

        public bool UpdateDlc(Library.Dlc dlc)
        {
            DataAccess.Dlc newdlc = Mapper.Map(dlc);
            if (_db.Dlc.Find(dlc.Dlcid) != null)
            {
                _db.Dlc.Update(newdlc);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        //not sure we will need this

        public bool UpdateDlcbyPrice(int id, decimal price)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGame(Library.Game game)
        {
            bool success = true;
            try
            {
                Game gameDB = Mapper.Map(game);
                _db.Update(gameDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }  
        }

        /*
         * probably don't need this
        public bool UpdateReviewbyScore(Library.UserGame review)
        {
            throw new NotImplementedException();
        }
        */

        public bool UpdateUser(Library.User user)
        {
            bool success = true;
            try
            {
                User userDB = Mapper.Map(user);
                _db.Update(userDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }
    }
}
