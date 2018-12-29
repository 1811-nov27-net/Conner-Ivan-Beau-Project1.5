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
                var list = repo.GetUsers(1).ToList();
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
            { Name = "Bungie", FoundingDate = DateTime.Now, Website = "http://www.bungie.com/" };
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
            { Name = "Ubisoft", FoundingDate = DateTime.Now, Website = "http://www.ubisoft.com/" };
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
            { Name = "Ubisoft", FoundingDate = DateTime.Now, Website = "http://www.ubisoft.com/" };
            Data.Developer newdeveloper2 = new Data.Developer
            { Name = "Bungie", FoundingDate = DateTime.Now, Website = "http://www.bungie.com/" };
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
        public void GrabGameByIdTest() { }
        //[Fact]
        //public void GrabListofGamesTest() { }
        //[Fact]
        //public void GrabUsersByDlcTest()
        //{
        //}
        //[Fact]
        //public void GrabUsersByGameTest() { }
        //[Fact]
        //public void AddDlcTest()
        //{
        //}
        //[Fact]
        //public void GrabDlcByIdTest() { }
        //[Fact]
        //public void UpdateDlcByPriceTest() { }
        //[Fact]
        //public void GrabDlcsByGameTest() { }
        //[Fact]
        //public void AddUserGameTest()
        //{
        //}
        //[Fact]
        //public void GrabUserGamesTest() { }
        //[Fact]
        //public void GrabOneUserGameTest() { }
        //[Fact]
        //public void AddReviewTest() { }
        //[Fact]
        //public void GrabReviewsByGameAndSortTest() { }
        //[Fact]
        //public void GrabReviewsByUserAndSortTest() { }
        //[Fact]
        //public void SuggestGameTest() { }
    }
}
