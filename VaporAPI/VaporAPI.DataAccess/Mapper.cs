using System;
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
        public static Library.Game Map(DataAccess.Game game) => new Library.Game
        {
            GameId = game.GameId,
            Name = game.Name,
            Price = game.Price,
            Description = game.Description,
            DeveloperId = game.DeveloperId,
            Image = game.Image,
            Trailer = game.Trailer
        };
        public static IEnumerable<Library.Game> Map(IEnumerable<DataAccess.Game> game) => game.Select(Map);

        // mapping library class to DB entity
        public static DataAccess.Game Map(Library.Game game) => new DataAccess.Game
        {
            GameId = game.GameId,
            Name = game.Name,
            Price = game.Price,
            Description = game.Description,
            DeveloperId = game.DeveloperId,
            Image = game.Image,
            Trailer = game.Trailer
        };
        public static IEnumerable<DataAccess.Game> Map(IEnumerable<Library.Game> game) => game.Select(Map);

        // mappings for tag objects
        // mapping DB entity to library class
        public static Library.Tag Map(DataAccess.Tag tag) => new Library.Tag
        {
            TagId = tag.TagId,
            GenreName = tag.GenreName
        };
        public static IEnumerable<Library.Tag> Map(IEnumerable<DataAccess.Tag> tag) => tag.Select(Map);

        // mapping library class to DB entity
        public static DataAccess.Tag Map(Library.Tag tag) => new DataAccess.Tag
        {
            TagId = tag.TagId,
            GenreName = tag.GenreName
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
        public static IEnumerable<DataAccess.User> Map(IEnumerable<Library.User> user) => user.Select(Map);
    }
}
