using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public interface IRepository
    {
        //CRUD for review and score for table UserGame
        bool AddReview(Review review);
        bool UpdateReviewbyScore(Review review);
        bool DeleteReview(Review review);

        //grab reviews from table UserGame by some parameters
        ICollection<Review> GetReviewbyGame(int id);
        Review GetReviewbyUser(string username);
        ICollection<Review> GetReviewsbyUser(string username, params int[] sort);
        ICollection<Review> GetReviewsByGame(int id, params int[] sort);

        //crud for users
        bool AddUser(User user);
        bool DeleteUser(string username);
        bool UpdateUser(User user);
        //grab users by some parameters
        ICollection<User> GetUsers(params int[] sort);
        ICollection<User> GetUsersbyGame(int id);
        ICollection<User> GetUsersbyDlc(int id);
        User GetUser(string username);

        //CRUD for games
        bool AddGame(Game game);
        bool DeleteGame(int id);
        bool UpdateGame(Game game);
        Game GetGame(int id);
        ICollection<Game> GetGames(params int[] sort);

        //CRUD for develpoer
        bool AddDeveloper(Developer developer);
        bool DeleteDeveloper(int id);
        //bool

        //ICollection<Dlc> GetDlcbyGame(int id);
        bool AddDlc(Dlc dlc);
        bool DeleteDlc(int id);
        bool UpdateDlcbyPrice(int id, decimal price);

        //bool CreateUser(User user);
        //bool DeleteUser(User user);

        //void addgame

        bool AddTag(Tag tag);
        bool DeleteTag(string genrename);
        Tag GetTag(int tagid);
        ICollection<Tag> GetTags();

        Game SuggestGamebyuser(string username);

        Dlc GetDlc(int dlcid);
        ICollection<Dlc> GetGameDlcs(int gameid);
        bool UpdateDlc(Dlc dlc);

        ICollection<Developer> GetDevelopers();
        Developer GetDeveloper(int developerid);
        bool UpdateDeveloper(Developer developer);
        //ame SuggestGamebyDevel

    }
}
