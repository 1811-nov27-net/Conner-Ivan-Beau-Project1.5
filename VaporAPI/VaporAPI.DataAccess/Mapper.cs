﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaporAPI.Library;

namespace VaporAPI.DataAccess
{
    public class Mapper
    {
        // mappings for developer objects 
        // mapping DB entity to library class
        public static Library.Developer Map(DataAccess.Developer developer) => new Library.Developer
        {
            DeveloperId = developer.DeveloperId,
            Name = developer.Name,
            FoundingDate = developer.FoundingDate,
            Website = developer.Website
        };
        public static IEnumerable<Library.Developer> Map(IEnumerable<DataAccess.Developer> developer) => developer.Select(Map);

        // mapping library class to DB entity
        public static DataAccess.Developer Map(Library.Developer developer) => new DataAccess.Developer
        {
            DeveloperId = developer.DeveloperId,
            Name = developer.Name,
            FoundingDate = developer.FoundingDate,
            Website = developer.Website
        };
        public static IEnumerable<DataAccess.Developer> Map(IEnumerable<Library.Developer> developer) => developer.Select(Map);


        // mappings for dlc objects
        // mapping DB entity to library class
        public static Library.Dlc Map(DataAccess.Dlc dlc) => new Library.Dlc
        {
            Dlcid = dlc.Dlcid,
            Name = dlc.Name,
            Price = dlc.Price,
            GameId = dlc.GameId

        };
        public static IEnumerable<Library.Dlc> Map(IEnumerable<DataAccess.Dlc> dlc) => dlc.Select(Map);

        // mapping library class to DB entity
        public static DataAccess.Dlc Map(Library.Dlc dlc) => new DataAccess.Dlc
        {
            Dlcid = dlc.Dlcid,
            Name = dlc.Name,
            Price = dlc.Price,
            GameId = dlc.GameId
        };
        public static IEnumerable<DataAccess.Dlc> Map(IEnumerable<Library.Dlc> dlc) => dlc.Select(Map);

        // mappings for game objects
        // mapping DB entity to library class
        public static Library.GameTag Map(DataAccess.GameTag gametag) => new Library.GameTag
        {
            GameId = gametag.GameId,
            TagId = gametag.TagId,
        };
        public static IEnumerable<Library.GameTag> Map(IEnumerable<DataAccess.GameTag> game) => game.Select(Map);
        public static IEnumerable<DataAccess.GameTag> Map(IEnumerable<Library.GameTag> game) => game.Select(Map);
        public static DataAccess.GameTag Map(Library.GameTag gametag) => new DataAccess.GameTag
        {
            GameId = gametag.GameId,
            TagId = gametag.TagId,
        };

        public static Library.Game Map(DataAccess.Game game) => new Library.Game
        {
            GameId = game.GameId,
            Name = game.Name,
            Price = game.Price,
            Description = game.Description,
            DeveloperId = game.DeveloperId,
            Image = game.Image,
            Trailer = game.Trailer,
        };
        //need the tags to be pase

        public static IEnumerable<Library.Game> Map(IEnumerable<DataAccess.Game> game) => game.Select(Map);

        public static List<DataAccess.GameTag> MapTagstoGTs(List<Library.Tag> tags, int gameid)
        {
            if (tags == null)
            {
                return null;
            }
            List<DataAccess.GameTag> newlist = new List<DataAccess.GameTag>();
            foreach (var item in tags)
            {
                DataAccess.GameTag tagitem = new GameTag
                {
                    GameId = gameid,
                    TagId = item.TagId,
                };
                newlist.Add(tagitem);
            }
            return newlist;
        }

        // mapping library class to DB entity
        public static DataAccess.Game Map(Library.Game game) => new DataAccess.Game
        {
            GameId = game.GameId,
            Name = game.Name,
            Price = game.Price,
            Description = game.Description,
            DeveloperId = game.DeveloperId,
            Image = game.Image,
            Trailer = game.Trailer,

            GameTag = MapTagstoGTs(game.Tags, game.GameId),
        };
        public static IEnumerable<DataAccess.Game> Map(IEnumerable<Library.Game> game) => game.Select(Map);

        // mappings for tag objects
        // mapping DB entity to library class
        public static Library.Tag Map(DataAccess.Tag tag) => new Library.Tag
        {
            TagId = tag.TagId,
            Name = tag.GenreName
        };
        public static IEnumerable<Library.Tag> Map(IEnumerable<DataAccess.Tag> tag) => tag.Select(Map);

        // mapping library class to DB entity
        public static DataAccess.Tag Map(Library.Tag tag) => new DataAccess.Tag
        {
            //TagId = tag.TagId,
            GenreName = tag.Name
        };
        public static IEnumerable<DataAccess.Tag> Map(IEnumerable<Library.Tag> tag) => tag.Select(Map);

        // mappings for user objects
        // mapping DB entity to library class
        public static Library.User Map(DataAccess.User user) => new Library.User
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Wallet = user.Wallet,
            CreditCard = user.CreditCard,
            Admin = user.Admin
        };
        public static IEnumerable<Library.User> Map(IEnumerable<DataAccess.User> user) => user.Select(Map);

        // mapping library class to DB entity
        public static DataAccess.User Map(Library.User user) => new DataAccess.User
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Wallet = user.Wallet,
            CreditCard = user.CreditCard,
            Admin = user.Admin
        };

        //public static DataAccess.UserGame Map(Library.UserGame userGame) => new DataAccess.UserGame
        //{
        //    //GameId = userGame.Game.GameId,
        //    //UserName = userGame.User.UserName,

        //    //there might be a problem here if the game and/or user already exist
        //    //then the map tries to map it again
        //    //the library.usergame might need to be changed aswell 
        //    Game = Mapper.Map(userGame.Game),
        //    UserNameNavigation = Mapper.Map(userGame.User),
        //    Review = userGame.Review,
        //    Score = userGame.Score,
        //    DatePurchased = userGame.PurchaseDate,

        //};

        public static Library.UserGame Map(DataAccess.UserGame userGame) => new Library.UserGame
        {
            Game = userGame.Game == null ? null : Mapper.Map(userGame.Game),
            //dont know why the name is so weird here
            User = userGame.UserNameNavigation == null ? null : Mapper.Map(userGame.UserNameNavigation),
            Review = userGame.Review,
            //if score is null then set score as -1?
            //TODO: decide what this case should be
            Score = userGame.Score ?? -1,
            PurchaseDate = userGame.DatePurchased,
        };

        public static Library.UserDlc Map(DataAccess.UserDlc userdlc) => new Library.UserDlc
        {
            User = Mapper.Map(userdlc.UserNameNavigation),
            Dlc = Mapper.Map(userdlc.Dlc)
        };

        public static DataAccess.UserDlc Map(Library.UserDlc userdlc) => new DataAccess.UserDlc
        {
            UserNameNavigation = Mapper.Map(userdlc.User),
            Dlc = Mapper.Map(userdlc.Dlc)
        };

        public static IEnumerable<Library.UserGame> Map(IEnumerable<DataAccess.UserGame> userGames) => userGames.Select(Map);
       
        public static IEnumerable<DataAccess.UserGame> Map(IEnumerable<Library.UserGame> userGames) => userGames.Select(Repository.Map);

        public static IEnumerable<DataAccess.User> Map(IEnumerable<Library.User> user) => user.Select(Map);

    }
}
