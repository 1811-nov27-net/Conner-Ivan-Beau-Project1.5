using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Libra = VaporAPI.Library;
using Data = VaporAPI.DataAccess;
using Microsoft.EntityFrameworkCore;


namespace VaporAPI.Testing
{
    public class RepositoryTests
    {

        [Fact]
        public void AddUserTests()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("add_user_testing").Options;

            Libra.User newuser = new Libra.User();

            newuser.UserName = "SlimShady";
            newuser.FirstName = "Marshall";
            newuser.LastName = "Mathers";
            newuser.Wallet = 50m;
            newuser.Admin = false;
            newuser.Password = "one23";
            newuser.CreditCard = "1111111111111111";



            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.AddUser(newuser);
                db.SaveChanges();
            }

            Assert.True(isnull);
        }
        [Fact]
        public void UpdateUserTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("update_user_testing").Options;
            using (var db = new Data.VaporDBContext(options))
            {
                db.User.Add(new Data.User
                {
                    UserName = "SlimShady",
                    FirstName = "Marshall",
                    LastName = "Mathers",
                    Wallet = 50,
                    Admin = false,
                    Password = "one23",
                    CreditCard = "1111111111111111"
                });

                db.SaveChanges();
            }
            Libra.User changeduser = new Libra.User
            {
                UserName = "SlimShady",
                FirstName = "Marsha",
                LastName = "Mathews",
                Wallet = 100,
                Admin = false,
                Password = "fourfive6",
                CreditCard = "2222222222222222"
            };

            Libra.User checkuser = new Libra.User();
            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.UpdateUser(changeduser);
                db.SaveChanges();
                checkuser = repo.GetUser(changeduser.UserName);
            }

            Assert.True(isnull);
        }
        [Fact]
        public void GrabAllUsersTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("get_all_user_testing").Options;
            using (var db = new Data.VaporDBContext(options))
            {
                db.User.Add(new Data.User
                {
                    UserName = "User1",
                    FirstName = "Al",
                    LastName = "Berring",
                    Admin = false,
                    Password = "1234",
                    Wallet = 0
                });
                db.User.Add(new Data.User
                {
                    UserName = "User2",
                    FirstName = "Zeek",
                    LastName = "Young",
                    Admin = false,
                    Password = "9876",
                    Wallet = 100
                });
                db.SaveChanges();
            }
            List<Libra.User> allusers = new List<Libra.User>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                var list = repo.GetUsers(0).ToList();
                allusers = list;

            }

            Assert.Equal("User1", allusers[0].UserName);
            Assert.Equal("Al", allusers[0].FirstName);
            Assert.Equal("Berring", allusers[0].LastName);
            Assert.False(allusers[0].Admin);
            Assert.Equal("1234", allusers[0].Password);
            Assert.Equal(0, allusers[0].Wallet);

            Assert.Equal("User2", allusers[1].UserName);
            Assert.Equal("Zeek", allusers[1].FirstName);
            Assert.Equal("Young", allusers[1].LastName);
            Assert.False(allusers[1].Admin);
            Assert.Equal("9876", allusers[1].Password);
            Assert.Equal(100, allusers[1].Wallet);

        }
        [Fact]
        public void GrabSpecificUserTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("get_one_user_testing").Options;
            using (var db = new Data.VaporDBContext(options))
            {
                db.User.Add(new Data.User
                {
                    UserName = "User1",
                    FirstName = "Al",
                    LastName = "Berring",
                    Admin = false,
                    Password = "1234",
                    Wallet = 0
                });
                db.SaveChanges();
            }
            Libra.User testuser = new Libra.User
            {
                UserName = "User1",
                FirstName = "Al",
                LastName = "Berring",
                Admin = false,
                Password = "1234",
                Wallet = 0
            };
            Libra.User grabuser = new Libra.User();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                grabuser = repo.GetUser("User1");

            }

            Assert.Equal(testuser.UserName, grabuser.UserName);
            Assert.Equal(testuser.FirstName, grabuser.FirstName);
            Assert.Equal(testuser.Password, grabuser.Password);
            Assert.Equal(testuser.LastName, grabuser.LastName);
            Assert.Equal(testuser.Wallet, grabuser.Wallet);
        }
        [Fact]
        public void DeleteGameTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("delete_a_game_testing").Options;
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = 1,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };

            //  Libra.Game newgame = new Libra.Game {GameId = 1, };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.SaveChanges();
            }

            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.DeleteGame(newgame.GameId);
                db.SaveChanges();
            }

            Assert.True(isnull);
        }
        [Fact]
        public void DeleteTagTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("delete_tag_test").Options;
            Data.Tag tagone = new Data.Tag
            {
                GenreName = "RPG",
            };
            Data.Tag tagtwo = new Data.Tag
            { GenreName = "Shooter", };

            using (var db = new Data.VaporDBContext(options))
            {
                db.Tag.Add(tagone);
                db.Tag.Add(tagtwo);
                db.SaveChanges();
            }

            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.DeleteTag(tagone.GenreName);
                db.SaveChanges();
            }

            Assert.True(isnull);
        }
        [Fact]
        public void GrabAllTagsTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("get_all_tag_test").Options;
            Data.Tag tagone = new Data.Tag
            {
                GenreName = "RPG",
            };
            Data.Tag tagtwo = new Data.Tag
            { GenreName = "Shooter", };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Tag.Add(tagone);
                db.Tag.Add(tagtwo);
                db.SaveChanges();
            }

            List<Libra.Tag> taglist = new List<Libra.Tag>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                taglist = repo.GetTags().ToList();
            }

            Assert.Equal(tagone.TagId, taglist[0].TagId);
            Assert.Equal(tagtwo.TagId, taglist[1].TagId);
        }
        [Fact]
        public void UpdateDeveloperTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("change_devloper_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Bungie",
                FoundingDate = DateTime.Now,
                Website = "http://www.bungie.com/",
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Libra.Developer changedeveloper = new Libra.Developer
            {
                DeveloperId = newdeveloper.DeveloperId,
                Name = "BungieJump",
                FoundingDate = newdeveloper.FoundingDate,
                Website = "http://www.bungiejunp.net/"
            };
            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.UpdateDeveloper(changedeveloper);
                db.SaveChanges();
            }

            Assert.True(isnull);

        }
        [Fact]
        public void GrabDeveloperByIdTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("get_dev_by_id_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/", };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }

            Libra.Developer getdev = new Libra.Developer();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                getdev = repo.GetDeveloper(newdeveloper.DeveloperId);
            }

            Assert.Equal(getdev.DeveloperId, newdeveloper.DeveloperId);
        }
        [Fact]
        public void GrabAllDevelopersTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("get_all_devs_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/",
            };
            Data.Developer newdeveloper2 = new Data.Developer
            { Name = "Bungie",
                FoundingDate = DateTime.Now,
                Website = "http://www.bungie.com/",
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.Developer.Add(newdeveloper2);
                db.SaveChanges();
            }

            List<Libra.Developer> devlist = new List<Libra.Developer>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                devlist = repo.GetDevelopers().ToList();
            }

            Assert.Equal(newdeveloper.Name, devlist[0].Name);
            Assert.Equal(newdeveloper.Website, devlist[0].Website);
            Assert.Equal(newdeveloper.FoundingDate, devlist[0].FoundingDate);
            Assert.Equal(newdeveloper2.Name, devlist[1].Name);
            Assert.Equal(newdeveloper2.Website, devlist[1].Website);
            Assert.Equal(newdeveloper2.FoundingDate, devlist[1].FoundingDate);

        }



        [Fact]
        public void GrabGameByIdTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("grab_game_by_id_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/",
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.SaveChanges();
            }
            Libra.Game grabgame = new Libra.Game();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                grabgame = repo.GetGame(newgame.GameId);
            }
            Assert.Equal("Doom", grabgame.Name);
            Assert.Equal("Good Game", grabgame.Description);
            Assert.Equal(49.99m, grabgame.Price);
            Assert.Equal(newgame.GameId, grabgame.GameId);
            Assert.Equal("Img", grabgame.Image);
        }
        [Fact]
        public void GrabListofGamesTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("grab_list_games_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            Data.Developer newdeveloper2 = new Data.Developer
            { Name = "Bungie",
                FoundingDate = DateTime.Now,
                Website = "http://www.bungie.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.Developer.Add(newdeveloper2);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            Data.Game newgame2 = new Data.Game
            {
                Name = "Overwatch",
                DeveloperId = newdeveloper2.DeveloperId,
                Image = "Img2",
                Price = 19.99m,
                Trailer = "Trlr2",
                Description = "Bad Game",
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.Game.Add(newgame2);
                db.SaveChanges();
            }
            List<Libra.Game> listgames = new List<Libra.Game>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                listgames = repo.GetGames(0).ToList();
            }
            Assert.Equal(newgame.GameId, listgames[0].GameId);
            Assert.Equal(newgame.Price, listgames[0].Price);
            Assert.Equal(newgame.Image, listgames[0].Image);
            Assert.Equal(newgame2.Trailer, listgames[1].Trailer);
            Assert.Equal(newgame2.Description, listgames[1].Description);
            Assert.Equal(newgame2.DeveloperId, listgames[1].DeveloperId);
        }
        [Fact]
        public void GrabUsersByGameTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("grab_users_by_games_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            Data.Developer newdeveloper2 = new Data.Developer
            { Name = "Bungie",
                FoundingDate = DateTime.Now,
                Website = "http://www.bungie.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.Developer.Add(newdeveloper2);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            Data.Game newgame2 = new Data.Game
            {
                Name = "Overwatch",
                DeveloperId = newdeveloper2.DeveloperId,
                Image = "Img2",
                Price = 19.99m,
                Trailer = "Trlr2",
                Description = "Bad Game",
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.Game.Add(newgame2);
                db.SaveChanges();
            }
            Data.User newuser = new Data.User
            {
                UserName = "User1",
                FirstName = "Al",
                LastName = "Berring",
                Admin = false,
                Password = "1234",
                Wallet = 0m
            };
            Data.User newuser2 = new Data.User
            {
                UserName = "User2",
                FirstName = "Zeek",
                LastName = "Young",
                Admin = false,
                Password = "9876",
                Wallet = 100m
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.User.Add(newuser);
                db.User.Add(newuser2);
                db.SaveChanges();
            }
            Data.UserGame newusergame = new Data.UserGame
            {
                GameId = newgame.GameId,
                UserName = newuser.UserName,
                DatePurchased = DateTime.Now,
                Score = 8,
                Review = "Good Game",
                Game = newgame,
                UserNameNavigation = newuser,
            };
            Data.UserGame newusergame2 = new Data.UserGame
            {
                GameId = newgame.GameId,
                UserName = newuser2.UserName,
                DatePurchased = DateTime.Now,
                Score = 4,
                Review = "Bad Game",
                Game = newgame,
                UserNameNavigation = newuser2,
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.UserGame.Add(newusergame);
                db.UserGame.Add(newusergame2);
                db.SaveChanges();
            }
            List<Libra.User> listusers = new List<Libra.User>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                listusers = repo.GetUsersbyGame(newgame.GameId).ToList();
            }

            Assert.Equal(newuser.UserName, listusers[0].UserName);
            Assert.Equal(newuser2.UserName, listusers[1].UserName);
        }
        [Fact]
        public void AddUserGameTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("add_usergame_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Libra.Game game = new Libra.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            //using (var db = new Data.VaporDBContext(options))
            //{
            //    db.Game.Add(newgame);
            //    db.SaveChanges();
            //}
            Libra.User user = new Libra.User    ////////problem code
            {
                UserName = "User2",
                FirstName = "Zeek",
                LastName = "Young",
                Admin = false,          
                Password = "9876",
                Wallet = 100m
            };
            //Libra.User user = new Libra.User();
            //Libra.Game game = new Libra.Game();
            //using (var db = new Data.VaporDBContext(options))
            //{
            //    db.User.Add(newuser);
            //    db.SaveChanges();
            //    var repo = new Data.Repository(db);
            //    user = repo.GetUser(newuser.UserName);
            //    game = repo.GetGame(newgame.GameId);
            //}
            Libra.UserGame newusergame = new Libra.UserGame
            {
                PurchaseDate = DateTime.Now,
                Review ="Nothing like it",
                Score = 10,
                User = user,
                Game = game,
            };
            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.AddUserGame(newusergame);
                db.SaveChanges();
            }

            Assert.True(isnull);
        }
        [Fact]
        public void GrabUserGamesByUserTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("grab_usergames_by_user_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            Data.Developer newdeveloper2 = new Data.Developer
            { Name = "Bungie",
                FoundingDate = DateTime.Now,
                Website = "http://www.bungie.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.Developer.Add(newdeveloper2);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            Data.Game newgame2 = new Data.Game
            {
                Name = "Overwatch",
                DeveloperId = newdeveloper2.DeveloperId,
                Image = "Img2",
                Price = 19.99m,
                Trailer = "Trlr2",
                Description = "Bad Game",
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.Game.Add(newgame2);
                db.SaveChanges();
            }
            Data.User newuser = new Data.User
            {
                UserName = "User1",
                FirstName = "Al",
                LastName = "Berring",
                Admin = false,
                Password = "1234",
                Wallet = 0m
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.User.Add(newuser);
                db.SaveChanges();
            }
            Data.UserGame newusergame = new Data.UserGame
            {
                GameId = newgame.GameId,
                UserName = newuser.UserName,
                DatePurchased = DateTime.Now,
                Score = 8,
                Review = "Good Game",
                Game = newgame,
                UserNameNavigation = newuser,
            };
            Data.UserGame newusergame2 = new Data.UserGame
            {
                GameId = newgame2.GameId,
                UserName = newuser.UserName,
                DatePurchased = DateTime.Now,
                Score = 4,
                Review = "Bad Game",
                Game = newgame2,
                UserNameNavigation = newuser,
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.UserGame.Add(newusergame);
                db.UserGame.Add(newusergame2);
                db.SaveChanges();
            }
            List<Libra.UserGame> usergamelist = new List<Libra.UserGame>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                usergamelist = repo.GetUserGames(newuser.UserName).ToList();
            }

            Assert.Equal(newusergame.GameId, usergamelist[0].Game.GameId);
            Assert.Equal(newusergame.Score, usergamelist[0].Score);
            Assert.Equal(newusergame2.GameId, usergamelist[1].Game.GameId);
            Assert.Equal(newusergame2.Review, usergamelist[1].Review);
        }
        [Fact]
        public void GrabOneUserGameTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("grab_one_usergame_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            //using (var db = new Data.VaporDBContext(options))
            //{
            //    db.Game.Add(newgame);
            //    db.SaveChanges();
            //}
            Data.User newuser = new Data.User   /////////////problem code
            {
                UserName = "User1",
                FirstName = "Al",
                LastName = "Berring",
                Admin = false,
                Password = "1234",
                Wallet = 0m
            };
            //using (var db = new Data.VaporDBContext(options))
            //{
            //    db.User.Add(newuser);
            //    db.SaveChanges();
            //}
            Data.UserGame newusergame = new Data.UserGame
            {
                GameId = newgame.GameId,
                UserName = newuser.UserName,
                DatePurchased = DateTime.Now,
                Score = 8,
                Review = "Good Game",
                Game = newgame,
                UserNameNavigation = newuser,
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.UserGame.Add(newusergame);
                db.SaveChanges();
            }

            Libra.UserGame oneusergame = new Libra.UserGame();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                oneusergame = repo.GetUserGame(newuser.UserName, newgame.GameId);
            }

            Assert.Equal(newusergame.Score, oneusergame.Score);
            Assert.Equal(newusergame.GameId, oneusergame.Game.GameId);
            Assert.Equal(newusergame.UserName, oneusergame.User.UserName);
            Assert.Equal(newusergame.DatePurchased, oneusergame.PurchaseDate);
        }
        [Fact]
        public void AddDlcTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("add_dlc_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.SaveChanges();
            }
            Libra.Dlc newdlc = new Libra.Dlc
            {
                Name = "Doom: Eternal",
                GameId = newgame.GameId,
                Price = 9.99m,
            };
            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.AddDlc(newdlc);
                db.SaveChanges();
            }

            Assert.True(isnull);
        }
        [Fact]
        public void GrabDlcByIdTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("add_dlc_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.SaveChanges();
            }
            Data.Dlc newdlc = new Data.Dlc
            {
                Name = "Doom: Aerosal Arsenal",
                Price = 5m,
                GameId = newgame.GameId,
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Dlc.Add(newdlc);
                db.SaveChanges();
            }
            Libra.Dlc getdlc = new Libra.Dlc();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                getdlc = repo.GetDlc(newdlc.Dlcid);
            }

            Assert.Equal(newdlc.Dlcid , getdlc.Dlcid);
            Assert.Equal(newdlc.Name, getdlc.Name);
        }
        [Fact]
        public void UpdateDlcByPriceTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("update_dlc_price_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.SaveChanges();
            }
            Data.Dlc newdlc = new Data.Dlc
            {
                Name = "Doom: Aerosal Arsenal",
                Price = 5m,
                GameId = newgame.GameId,
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Dlc.Add(newdlc);
                db.SaveChanges();
            }
            Libra.Dlc changedlc = new Libra.Dlc
            {
                Dlcid = newdlc.Dlcid,
                Name = newdlc.Name,
                GameId = newdlc.GameId,
                Price = 10m,
            };
            bool isnull = false;
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                isnull = repo.UpdateDlc(changedlc);
                db.SaveChanges();
            }

            Assert.True(isnull);
        }
        [Fact]
        public void GrabDlcsByGameTest()
        {
            var options = new DbContextOptionsBuilder<Data.VaporDBContext>().UseInMemoryDatabase("grab_dlc_by_game_test").Options;
            Data.Developer newdeveloper = new Data.Developer
            { Name = "Ubisoft",
                FoundingDate = DateTime.Now,
                Website = "http://www.ubisoft.com/"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Developer.Add(newdeveloper);
                db.SaveChanges();
            }
            Data.Game newgame = new Data.Game
            {
                Name = "Doom",
                DeveloperId = newdeveloper.DeveloperId,
                Image = "Img",
                Price = 49.99m,
                Trailer = "Trlr",
                Description = "Good Game"
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Game.Add(newgame);
                db.SaveChanges();
            }
            Data.Dlc newdlc = new Data.Dlc
            {
                Name = "Doom: Aerosal Arsenal",
                Price = 5m,
                GameId = newgame.GameId,
            };
            Data.Dlc newdlc2 = new Data.Dlc
            {
                Name = "Doom: Eternal",
                GameId = newgame.GameId,
                Price = 9.99m,
            };
            using (var db = new Data.VaporDBContext(options))
            {
                db.Dlc.Add(newdlc);
                db.Dlc.Add(newdlc2);
                db.SaveChanges();
            }

            List<Libra.Dlc> dlclist = new List<Libra.Dlc>();
            using (var db = new Data.VaporDBContext(options))
            {
                var repo = new Data.Repository(db);
                dlclist = repo.GetGameDlcs(newdlc.GameId).ToList();
            }

            Assert.Equal(newdlc.Dlcid, dlclist[0].Dlcid);
            Assert.Equal(newdlc.Name, dlclist[0].Name);
            Assert.Equal(newdlc2.Dlcid, dlclist[1].Dlcid);
            Assert.Equal(newdlc2.Price, dlclist[1].Price);
        }

        //[Fact]
        //public void AddReviewTest() { }
        //[Fact]
        //public void GrabReviewsByGameAndSortTest() { }
        //[Fact]
        //public void GrabReviewsByUserAndSortTest() { }
        //[Fact]
        //public void SuggestGameTest() { }
        //[Fact]
        //public void GrabUsersByDlcTest()
        //{
        //}
    }
}
