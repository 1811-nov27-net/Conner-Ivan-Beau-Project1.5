﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VaporAPI.Library
{
    public interface IRepository
    {
        //CRUD for review and score for table UserGame
        bool AddReview(UserGame review);
        bool AddReview(string username, int id, ScoreReview review);
        bool UpdateReview(UserGame review);
        bool DeleteReview(UserGame review);

        //grab reviews from table UserGame by some parameters
        ICollection<Library.UserGame> GetReviews(int sort = 0);
        ICollection<UserGame> GetReviewsbyUser(string username, int sort = 0);
        ICollection<UserGame> GetReviewsByGame(int id, int sort = 0);
        ICollection<UserGame> GetReviewsHelper(ICollection<UserGame> oldUserGamesLib, int sort = 0);

        //crud for userdlc
        bool AddUserDlc(UserDlc userDlc);
        bool DeleteUserDlc(string username, int id);
        ICollection<UserDlc> GetUserDlcs(string username);
        UserDlc GetUserDlc(string username, int id);

        Library.FullUserGame GetFullUserGame(string username, int id);



        //crud for users
        bool AddUser(User user);
        bool DeleteUser(string username);
        bool UpdateUser(User user);
        //grab users by some parameters
        ICollection<User> GetUsers(int sort = 0);
        ICollection<User> GetUsersbyGame(int id);
        ICollection<User> GetUsersbyDlc(int id);
        User GetUser(string username);

        //CRUD for games
        bool AddGame(Game game);
        bool DeleteGame(int id);
        bool UpdateGame(Game game);
        Game GetGame(int id);
        ICollection<Game> GetGames(int sort = 0);
        ICollection<Game> GetBetweenPriceGames(int[] price);
        ICollection<Library.Game> GetBetweenRatingsGames(int[] rating);
        ICollection<Library.Game> GetGamesByDeveloper(params int[] devIds);
        ICollection<Library.Game> GetGamesByTags(params int[] tagIds);
        ICollection<Library.Game> FilterGames(params ICollection<Library.Game>[] gamesToFilter);
        ICollection<Library.Game> GetGamesHelper(ICollection<Game> oldGames, int sort = 0);
        Library.UserGame GetReviewByUserGame(string username, int id);
        decimal AverageScoreGame(Library.Game game);

        //CRUD for developer
        bool AddDeveloper(Developer developer);
        bool DeleteDeveloper(int id);
        //bool

        //ICollection<Dlc> GetDlcbyGame(int id);
        bool AddDlc(Dlc dlc);
        bool DeleteDlc(int id);
        List<Game> GetGameBySearchName(string searchString);
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

        ICollection<UserGame> GetUserGames(string username);
        UserGame GetUserGame(string username, int gameid);
        bool AddUserGame(UserGame review);
        bool DeleteUserGame(string username, int gameid);
        bool UpdateUserGame(Library.UserGame userGame);

        ICollection<Library.Dlc> GetDlcs();

    }
}
