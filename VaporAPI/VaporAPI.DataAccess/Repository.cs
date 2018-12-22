using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public bool AddDlc(Library.Dlc dlc)
        {
            throw new NotImplementedException();
        }

        public bool AddGame(Library.Game game)
        {
            throw new NotImplementedException();
        }

        public bool AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public bool AddTag(Library.Tag tag)
        {
            throw new NotImplementedException();
        }

        public bool AddUser(Library.User user)
        {
            throw new NotImplementedException();
        }

        public bool CreateDlc(Library.Dlc dlc)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDeveloper(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDlc(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGame(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTag(string genrename)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.Dlc> GetDlcbyGame(int id)
        {
            throw new NotImplementedException();
        }

        public Library.Game GetGame(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.Game> GetGames(params int[] sort)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetReviewbyGame(int id)
        {
            throw new NotImplementedException();
        }

        public Review GetReviewbyUser(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetReviewsByGame(int id, params int[] sort)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetReviewsbyUser(string username, params int[] sort)
        {
            throw new NotImplementedException();
        }

        public Library.Tag GetTag(int tagid)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.Tag> GetTags()
        {
            throw new NotImplementedException();
        }

        public Library.User GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.User> GetUsers(params int[] sort)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.User> GetUsersbyDlc(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Library.User> GetUsersbyGame(int id)
        {
            throw new NotImplementedException();
        }

        public Library.Game SuggestGamebyuser(string username)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDlcbyPrice(int id, decimal price)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGame(Library.Game game)
        {
            throw new NotImplementedException();
        }

        public bool UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public bool UpdateReviewbyScore(Review review)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(Library.User user)
        {
            throw new NotImplementedException();
        }
    }
}
