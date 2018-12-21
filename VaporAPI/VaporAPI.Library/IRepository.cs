using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public interface IRepository
    {
        //adds the review and score for table UserGame
        bool AddReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        Review GetReviewbyGame(int id);
        Review GetReviewbyUser(string username);
        ICollection<Review> GetReviewsbyUser(string username, params int[] sort);
        ICollection<Review> GetReviewsByGame(int id, params int[] sort);

        bool AddUser(User user);
        bool DeleteUser(string username);
        bool UpdateUser(User user);
        ICollection<User> GetUsers();
        User GetUser(string username);


        ICollection<Dlc> GetDlcbyGame(int id);
        bool CreateDlc(Dlc dlc);
       // bool 


        //bool CreateUser(User user);
        //bool DeleteUser(User user);

        //void addgame
        

        
    }
}
