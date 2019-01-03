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
                developer.DeveloperId = developerDB.DeveloperId;
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
                dlc.Dlcid = dlcDB.Dlcid;
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool AddUserDlc(Library.UserDlc userdlc)
        {
            try
            {
                var udlc = Mapper.Map(userdlc);
                _db.Add(udlc);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddGame(Library.Game game)
        {
            bool success = true;
            try
            {
                if (game.Tags == null) { return false; }
                var gameDB = Mapper.Map(game);
                _db.Add(gameDB);                //shit
                _db.SaveChanges();
                game.GameId = gameDB.GameId;

                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool AddReview(string username, int id, ScoreReview review)
        {
            try
            {
                DataAccess.UserGame usergame = _db.UserGame.First(a => a.UserName == username && a.GameId == id);
                usergame.Score = review.Score;
                usergame.Review = review.Review;
                //_db.Entry(_db.UserGame.Find(usergame.GameId, usergame.UserName)).CurrentValues.SetValues(usergame);
                _db.UserGame.Update(usergame);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddReview(Library.UserGame review)
        {
            bool success = true;
            try
            {
                //this wouldn't work unless we .Include the UserGame along with the user
                //User user = _db.User.Find(review.User.UserName);
                //DataAccess.UserGame usergame = Map3(review);
                DataAccess.UserGame usergame = _db.UserGame.First(a => a.UserName == review.User.UserName && a.GameId == review.Game.GameId);
                usergame.Score = review.Score;
                usergame.Review = review.Review;
                //_db.Entry(_db.UserGame.Find(usergame.GameId, usergame.UserName)).CurrentValues.SetValues(usergame);
                _db.UserGame.Update(usergame);
                _db.SaveChanges();
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
                tag.TagId = tagDB.TagId;
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
                _db.Add(userDB);
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
                if (_db.Game.Find(userGame.Game.GameId) == null || _db.User.Find(userGame.User.UserName) == null)
                {
                    return false;
                }
                //had to make two maps since the IEnumerable Mapper was giving me error
                _db.UserGame.Add(Map2(userGame));
                _db.SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }
        //had to make two maps since the IEnumerable Mapper was giving me error
        public DataAccess.UserGame Map2(Library.UserGame userGame) => new DataAccess.UserGame
        {
            //GameId = userGame.Game.GameId,
            //UserName = userGame.User.UserName,
            Game = _db.Game.Find(userGame.Game.GameId),
            UserNameNavigation = _db.User.Find(userGame.User.UserName),
            Review = userGame.Review,
            Score = userGame.Score,
            DatePurchased = userGame.PurchaseDate,
        };
        public DataAccess.UserGame Map3(Library.UserGame userGame) => new DataAccess.UserGame
        {
            Review = userGame.Review,
            Score = userGame.Score,
            DatePurchased = userGame.PurchaseDate,
            GameId = userGame.Game.GameId,
            UserName = userGame.User.UserName,
        };
        //had to make two maps since the IEnumerable Mapper was giving me error
        public static DataAccess.UserGame Map(Library.UserGame userGame) => new DataAccess.UserGame
        {
            //GameId = userGame.Game.GameId,
            //UserName = userGame.User.UserName,
            Game = Mapper.Map(userGame.Game),
            UserNameNavigation = Mapper.Map(userGame.User),
            Review = userGame.Review,
            Score = userGame.Score,
            DatePurchased = userGame.PurchaseDate,
        };

        public bool UpdateUserGame(Library.UserGame userGame)
        {
            try
            {
                //_db.UserGame.First(a => a.UserName == userGame.User.UserName && a.GameId == userGame.Game.GameId);
                if (_db.Game.Find(userGame.Game.GameId) == null || _db.User.Find(userGame.User.UserName) == null)
                {
                    return false;
                }
                //had to make two maps since the IEnumerable Mapper was giving me error

                _db.Entry(_db.UserGame.Find(userGame.Game.GameId, userGame.User.UserName))
                        .CurrentValues.SetValues(Map2(userGame));
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

        public bool DeleteUserDlc(string username, int id)
        {
            try
            {
                _db.UserDlc.Remove(_db.UserDlc.First(a => a.UserName == username && a.Dlcid == id));
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
                _db.Remove(_db.GameTag.Where(c => c.GameId == id));
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        /// <summary>
        /// deletes the review by setting it's values equal to null for the given UserGame
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>

        public bool DeleteReview(Library.UserGame review)
        {
            DataAccess.UserGame ug = _db.UserGame.Where(a => a.UserName == review.User.UserName && a.GameId == review.Game.GameId).First();
            if (ug != null)
            {
                ug.Score = null;//review.Score;
                ug.Review = null;//review.Review;

                _db.Entry(_db.UserGame.Find(ug.UserName, ug.GameId)).CurrentValues.SetValues(ug);
                // _db.UserGame.Update(ug);
                _db.SaveChanges();
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
            gameLib.Tags = GetGameTags(game.GameId);
            return gameLib;
        }

        public List<Library.Tag> GetGameTags(int id)
        {
            List<Library.Tag> tags = new List<Library.Tag>();
            Game game = _db.Game.Include("GameTag.Tag").First(a => a.GameId == id);
            foreach (var t in game.GameTag)
            {
                tags.Add(Mapper.Map(t.Tag));
            }
            return tags;
        }

        public ICollection<Library.Dlc> GetGameDlcs(int gameid)
        {
            ICollection<DataAccess.Dlc> dlcs = _db.Dlc.Where(a => a.GameId == gameid).ToList();
            return dlcs == null ? null : Mapper.Map(dlcs).ToList();
        }

        public decimal AverageScoreGame(Library.Game game)
        {
            List<UserGame> gameScores = _db.UserGame.Where(g => g.GameId == game.GameId).ToList();
            List<int> gameInts = new List<int>();
            foreach (var item in gameScores)
            {
                if (item.Score != null && item.Score != 0)
                {
                    // had to explicitly cast from int? to int to avoid compile error
                    var score = (int)item.Score;
                    gameInts.Add(score);
                }
            }

            var scores = gameInts.ToArray();

            int sum = 0;
            foreach (var item in scores)
            {
                sum += item;
            }
            try
            {
                return ((decimal)sum / (decimal)scores.Length);
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        public ICollection<Library.Game> GetBetweenPriceGames(int[] price)
        {
            int lowPrice = price[0];
            int highPrice = price[1];
            List<Game> gamesToAdd = _db.Game.Where(g => g.Price >= lowPrice && g.Price <= highPrice).ToList();
            List<Library.Game> games = Mapper.Map(gamesToAdd).ToList();
            return games;
        }

        public ICollection<Library.Game> GetBetweenRatingsGames(int[] rating)
        {
            int lowRating = rating[0];
            int highRating = rating[1];
            List<IGrouping<int, UserGame>> Scores = _db.UserGame.GroupBy(ug => ug.GameId).Where(ug => ug.Average(x => x.Score) >= lowRating && ug.Average(x => x.Score) <= highRating).ToList();
            List<UserGame> userGamesToAdd = Scores.SelectMany(g => g).ToList(); // parses out the UserGames from the grouping
            HashSet<int> gameIds = new HashSet<int>();
            foreach (var ele in userGamesToAdd)
            {
                gameIds.Add(ele.GameId);
            }
            List<int> gameIdsList = gameIds.ToList();
            List<Library.Game> games = new List<Library.Game>();
            foreach (var unit in gameIdsList)
            {
                games.Add(Mapper.Map(_db.Game.Where(g => g.GameId == unit).FirstOrDefault()));
            }
            return games;
        }

        public ICollection<Library.Game> GetGamesByDeveloper(params int[] devIds)
        {
            List<Game> games = new List<Game>();
            foreach (var item in devIds)
            {
                List<Game> gamesToAdd = _db.Game.Where(g => g.DeveloperId == item).ToList();
                foreach (var game in gamesToAdd)
                {
                    games.Add(game);
                }
            }
            List<Library.Game> gamesLib = Mapper.Map(games).ToList();
            return gamesLib;

        }

        public ICollection<Library.Game> GetGamesByTags(params int[] tagIds)
        {
            HashSet<Game> gamesSet = new HashSet<Game>();
            foreach (var item in tagIds)
            {
                List<Game> games = _db.GameTag.Include(gt => gt.Game).Where(gt => gt.TagId == item).Select(gt => gt.Game).ToList();
                foreach (var game in games)
                {
                    gamesSet.Add(game);
                }
            }

            return Mapper.Map(gamesSet).ToList();
        }

        // Bizarre double mapping to combat the fact that the framework is unable to recognize that
        // two identical Library.Game intances are the same. It is, however, able to do so with DB entites
        public ICollection<Library.Game> FilterGames(params ICollection<Library.Game>[] gamesToFilter)
        {
            var priceGames = gamesToFilter[0].ToList();
            var ratingGames = gamesToFilter[1].ToList();
            var devGames = gamesToFilter[2].ToList();
            var tagGames = gamesToFilter[3].ToList();

            int[] gameIdsPrice = new int[priceGames.Count];
            for (int i = 0; i < gameIdsPrice.Length; i ++)
            {
                Library.Game gamei = priceGames[i];
                int gameId = gamei.GameId;
                gameIdsPrice[i] = gameId;
            }

            int[] gameIdsRating = new int[ratingGames.Count];
            for (int i = 0; i < gameIdsRating.Length; i++)
            {
                Library.Game gamei = ratingGames[i];
                int gameId = gamei.GameId;
                gameIdsRating[i] = gameId;
            }

            int[] gameIdsDev = new int[devGames.Count];
            for (int i = 0; i < gameIdsDev.Length; i++)
            {
                Library.Game gamei = devGames[i];
                int gameId = gamei.GameId;
                gameIdsDev[i] = gameId;
            }

            int[] gameIdsTag = new int[tagGames.Count];
            for (int i = 0; i < gameIdsTag.Length; i++)
            {
                Library.Game gamei = tagGames[i];
                int gameId = gamei.GameId;
                gameIdsTag[i] = gameId;
            }

            List<int> gameIds = gameIdsPrice.Intersect(gameIdsRating.Intersect(gameIdsDev.Intersect(gameIdsTag))).ToList();
            List<Game> games = new List<Game>();
            foreach (int number in gameIds)
            {
                Game gameToAdd = _db.Game.Where(g => g.GameId == number).FirstOrDefault();
                games.Add(gameToAdd);
            }
            return Mapper.Map(games).ToList();
        }

        // retrieves all games, sorted as given
        public ICollection<Library.Game> GetGames(int sort = 0)
        {
            ICollection<Library.Game> games = GetGamesHelper(Mapper.Map(_db.Game).ToList(), sort);
            foreach (var g in games)
            {
                g.Tags = GetGameTags(g.GameId);
            }
            return games;
        }

        public List<Library.Game> GetGameBySearchName(string searchString)
        {
            if (searchString == "")
            {
                return Mapper.Map(_db.Game).ToList();
            }
            List<Game> games = _db.Game.Where(g => g.Name.ToLower().Contains(searchString.ToLower())).ToList();
            List<Library.Game> gamesLib = Mapper.Map(games).ToList();
            return gamesLib;
        }

        public ICollection<Library.Game> GetGamesHelper(ICollection<Library.Game> oldGamesLib, int sort = 0)
        {
            //ICollection<Game> oldGames = Mapper.Map(oldGamesLib).ToList();

            ICollection<Library.Game> games = new List<Library.Game>();

            switch (sort)
            {
                case 0:
                    games = oldGamesLib.OrderBy(g => g.GameId).ToList();
                    //return games0 == null ? null : Mapper.Map(games0).ToList();
                    break;
                case 1:
                    games = oldGamesLib.OrderBy(g => g.Name).ToList();
                    //return games1 == null ? null : Mapper.Map(games1).ToList();
                    break;
                case 2:
                    games = oldGamesLib.OrderBy(g => g.Price).ToList();
                    //return games2 == null ? null : Mapper.Map(games2).ToList();
                    break;

                case 3:
                    games = oldGamesLib.OrderByDescending(g => g.Price).ToList();
                    //return games3 == null ? null : Mapper.Map(games3).ToList();
                    break;
                default:
                    games = oldGamesLib.OrderBy(g => g.GameId).ToList();
                    //return gamesD == null ? null : Mapper.Map(gamesD).ToList();
                    break;
            }
            return games;
        }
        //retrieves all UserGames, sorted as given
        public ICollection<Library.UserGame> GetReviews(int sort = 0)
        {
            return GetReviewsHelper(Mapper.Map(_db.UserGame).ToList(), sort);
        }
        public Library.UserGame GetReviewByUserGame(string username, int id)
        {
            //var tags = _db.Tag.Include(t => t.TagId).ThenInclude(t => t.)
            var usergame = _db.UserGame.Include(g => g.Game).Include("UserNameNavigation").Where(ug => ug.UserName == username && ug.GameId == id).First();
            if(usergame == null)
            {
                return null;
            }
            
            Library.UserGame libusergame = Mapper.Map(usergame);
            libusergame.Game.Tags = GetGameTags(libusergame.Game.GameId);
            return libusergame;
        }
    

        public ICollection<Library.UserGame> GetReviewsbyUser(string username, int sort = 0)
        {
            IEnumerable<UserGame> userGames = _db.UserGame.Where(ug => ug.UserName == username).ToList();
            var listUG = Mapper.Map(userGames).ToList();
            return GetReviewsHelper(listUG, sort);
        }

        public ICollection<Library.UserGame> GetReviewsByGame(int id, int sort = 0)
        {
            IEnumerable<UserGame> userGames = _db.UserGame.Where(ug => ug.GameId == id).ToList();
            var listUG = Mapper.Map(userGames).ToList();
            return GetReviewsHelper(listUG, sort);
        }

        public ICollection<Library.UserGame> GetReviewsHelper(ICollection<Library.UserGame> oldUserGamesLib, int sort = 0)
        {
            ICollection<UserGame> oldUserGames = (ICollection<UserGame>)Mapper.Map(oldUserGamesLib);
            switch (sort)
            {
                // case 0 sorts by gameID
                case 0:
                    ICollection<UserGame> userGames0 = oldUserGames.OrderBy(ug => ug.GameId).ToList();
                    return userGames0 == null ? null : Mapper.Map(userGames0).ToList();

                // case 1 sorts by username
                case 1:
                    ICollection<UserGame> userGames1 = oldUserGames.OrderBy(ug => ug.UserName).ToList();
                    return userGames1 == null ? null : Mapper.Map(userGames1).ToList();

                // case 2 sprts by username descending
                case 2:
                    ICollection<UserGame> userGames2 = oldUserGames.OrderByDescending(ug => ug.UserName).ToList();
                    return userGames2 == null ? null : Mapper.Map(userGames2).ToList();

                // case 3 sorts by score
                case 3:
                    ICollection<UserGame> userGames3 = oldUserGames.OrderBy(ug => ug.Score).ToList();
                    return userGames3 == null ? null : Mapper.Map(userGames3).ToList();

                // case 4 sorts by score descending
                case 4:
                    ICollection<UserGame> userGame4 = oldUserGames.OrderByDescending(ug => ug.Score).ToList();
                    return userGame4 == null ? null : Mapper.Map(userGame4).ToList();

                // default case is sort by gameID
                default:
                    ICollection<UserGame> userGamesD = oldUserGames.OrderBy(ug => ug.GameId).ToList();
                    return userGamesD == null ? null : Mapper.Map(userGamesD).ToList();
            }
        }

        // Note that this method is not in the repository interface because
        // because the interface does not reference this DataAccess folder...
        // Similarly, the above method is not implemented because it takes
        // a collection of Library games...
        //public ICollection<Library.Game> GetGamesHelper(ICollection<DataAccess.Game> oldGames, int sort = 0)
        //{
        //    switch (sort)
        //    {
        //        case 0:
        //            ICollection<Game> games0 = oldGames.OrderBy(g => g.GameId).ToList();
        //            return games0 == null ? null : Mapper.Map(games0).ToList();
        //        case 1:
        //            ICollection<Game> games1 = oldGames.OrderBy(g => g.Name).ToList();
        //            return games1 == null ? null : Mapper.Map(games1).ToList();
        //        case 2:
        //            ICollection<Game> games2 = oldGames.OrderBy(g => g.Price).ToList();
        //            return games2 == null ? null : Mapper.Map(games2).ToList();

        //        case 3:
        //            ICollection<Game> games3 = oldGames.OrderByDescending(g => g.Price).ToList();
        //            return games3 == null ? null : Mapper.Map(games3).ToList();
        //        default:
        //            ICollection<Game> gamesD = oldGames.OrderBy(g => g.GameId).ToList();
        //            return gamesD == null ? null : Mapper.Map(gamesD).ToList();
        //    }
        //}

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
            if (ug == null)
            {
                return null;
            }
            Library.UserGame lug = Mapper.Map(ug);
            lug.Game.Tags = GetGameTags(lug.Game.GameId);
            return lug;
        }

        public ICollection<Library.UserGame> GetUserGames(string username)
        {
            ICollection<DataAccess.UserGame> ugs = _db.UserGame.Include("Game").Include("UserNameNavigation").Where(a => a.UserName == username).ToList();
            if (ugs == null)
            {
                return null;
            }
            ICollection<Library.UserGame> libugs = Mapper.Map(ugs).ToList();

            foreach (var ug in libugs)
            {
                ug.Game.Tags = GetGameTags(ug.Game.GameId);
            }
            return libugs;
        }

        public ICollection<Library.User> GetUsers(int sort = 0)
        {
            //sort not  implemented yet
            List<DataAccess.User> users = _db.User.AsNoTracking().ToList();
            return users == null ? null : Mapper.Map(users).ToList();
        }

        public ICollection<Library.UserDlc> GetUserDlcs(string username)
        {
            List<DataAccess.UserDlc> udlcs = _db.UserDlc.Where(a => a.UserName == username).Include("UserNameNavigation").Include("Dlc").AsNoTracking().ToList();
            return udlcs == null ? null : udlcs.Select(a => Mapper.Map(a)).ToList();
        }

        public Library.UserDlc GetUserDlc(string username, int id)
        {
            DataAccess.UserDlc dlc = _db.UserDlc.Include("UserNameNavigation").Include("Dlc").First(a => a.UserName == username && a.Dlcid == id);
            return dlc == null ? null : Mapper.Map(dlc);
        }

        //not sure we will need these
        // returns all users that have downloaded the given DLC
        public ICollection<Library.User> GetUsersbyDlc(int id)
        {
            List<string> userNames = _db.UserDlc.Where(u => u.Dlcid == id).Select(u => u.UserName).ToList();
            List<Library.User> usersDB = new List<Library.User>();
            foreach (var item in userNames)
            {
                var userToAddDB = _db.User.Where(u => u.UserName == item).FirstOrDefault();
                var userToAddLib = Mapper.Map(userToAddDB);
                usersDB.Add(userToAddLib);
            }
            return usersDB;
        }

        // returns all users that have purchased the given game
        public ICollection<Library.User> GetUsersbyGame(int id)
        {
            List<string> userNames = _db.UserGame.Where(u => u.GameId == id).Select(u => u.UserName).ToList();
            List<Library.User> usersDB = new List<Library.User>();
            foreach (var item in userNames)
            {
                var userToAddDB = _db.User.Where(u => u.UserName == item).FirstOrDefault();
                var userToAddLib = Mapper.Map(userToAddDB);
                usersDB.Add(userToAddLib);
            }
            return usersDB;
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
                _db.Entry(_db.Developer.Find(developer.DeveloperId))
                        .CurrentValues.SetValues(dev);
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
                _db.Entry(_db.Dlc.Find(newdlc.Dlcid)).CurrentValues.SetValues(newdlc);//(newdlc);
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
                _db.Entry(_db.Game.Find(gameDB.GameId)).CurrentValues.SetValues(gameDB);
                // _db.Update(gameDB);
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

        public ICollection<Library.Dlc> GetDlcs()
        {
            ICollection<DataAccess.Dlc> dlcs = _db.Dlc.ToList();
            return dlcs == null ? null : Mapper.Map(dlcs).ToList();
        }

        public bool UpdateUser(Library.User user)
        {
            bool success = true;
            try
            {
                User userDB = Mapper.Map(user);
                _db.Entry(_db.User.Find(userDB.UserName)).CurrentValues.SetValues(userDB);
                // _db.Update(userDB);
                _db.SaveChanges();
                return success;
            }
            catch
            {
                success = false;
                return success;
            }
        }

        public bool UpdateReview(Library.UserGame review)
        {
            return this.AddReview(review);
        }
    }
}
