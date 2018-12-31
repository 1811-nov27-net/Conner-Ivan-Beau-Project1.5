using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VaporWebSite.App.Models;
using Xunit;

namespace VaporWebSite.Test
{
    public class TestDeserialize
    {
        [Fact]
        public void Test1()
        {
            string responseBody = "[{\"user\":{\"userName\":\"Conner\",\"firstName\":null,\"lastName\":null,\"password\":\"Conner\",\"wallet\":0.0000,\"creditCard\":null,\"admin\":false},\"game\":{\"gameId\":1,\"name\":\"Mass Effect\",\"price\":39.9900,\"description\":\"space shooty game\",\"developerId\":3,\"image\":\"a\",\"trailer\":\"a\",\"tags\":[{\"tagId\":1,\"name\":\"Action\"},{\"tagId\":2,\"name\":\"RPG\"},{\"tagId\":10,\"name\":\"Alien\"}]},\"purchaseDate\":\"0001-01-01T00:00:00\",\"review\":null,\"score\":0},{\"user\":{\"userName\":\"Conner\",\"firstName\":null,\"lastName\":null,\"password\":\"Conner\",\"wallet\":0.0000,\"creditCard\":null,\"admin\":false},\"game\":{\"gameId\":2,\"name\":\"Skyrim\",\"price\":59.9900,\"description\":\"fight nords and elves and stuff\",\"developerId\":2,\"image\":\"a\",\"trailer\":\"a\",\"tags\":[{\"tagId\":1,\"name\":\"Action\"},{\"tagId\":2,\"name\":\"RPG\"},{\"tagId\":5,\"name\":\"Adventure\"},{\"tagId\":6,\"name\":\"Fantasy\"}]},\"purchaseDate\":\"0001-01-01T00:00:00\",\"review\":null,\"score\":0}]";
            List<UserGame>userGames = JsonConvert.DeserializeObject<List<UserGame>>(responseBody);
        }
    }
}
