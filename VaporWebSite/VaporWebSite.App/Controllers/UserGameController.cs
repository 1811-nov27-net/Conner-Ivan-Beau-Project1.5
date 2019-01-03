using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VaporWebSite.App.Models;

namespace VaporWebSite.App.Controllers
{
    public class UserGameController : ARequestController
    {


        public UserGameController(HttpClient client) : base(client)
        {
            //nothing to do
        }


        // GET: UserGame
        public async Task<ActionResult> Index()
        {

            var username = ViewBag.LoggedInUser;
            if (username == "")
            {
                return RedirectToAction("Login", "Account");
            }

            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Game/Reviews");
            HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, $"api/User/{username}/Library");
            //HttpRequestMessage request3 = CreateRequest(HttpMethod.Get, $"api/User/{id}/Reviews");


            HttpResponseMessage response = await Client.SendAsync(request);
            HttpResponseMessage response2 = await Client.SendAsync(request2);


            if (!response.IsSuccessStatusCode || !response2.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized || response2.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Error", "Home");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            string responseBody2 = await response2.Content.ReadAsStringAsync();



            Dictionary<Game,decimal> games = JsonConvert.DeserializeObject<Dictionary<Game,decimal>>(responseBody);
            List<UserGame> userGames = JsonConvert.DeserializeObject<List<UserGame>>(responseBody2);

            List<FullGame> fullGames = new List<FullGame>();
            foreach(var g in games)
            {
                fullGames.Add(new FullGame { Game = g.Key, Score = g.Value, Selected = userGames.Any(a => a.Game.GameId == g.Key.GameId)});
            }
            return View(fullGames);

            //return View(games.Select(g => new FullGame{Game = g, Selected = userGames.Any(a => a.Game.GameId == g.GameId) }).ToList());
        }

        // GET: UserGame by Searched Name
        public async Task<ActionResult> Search(string searchString)
        {
            if (searchString == null)
            {
                return RedirectToAction("Index");
            }

            var username = ViewBag.LoggedInUser;
            if (username == "")
            {
                return RedirectToAction("Login", "Account");
            }

            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Game/Search/{searchString}");
            HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, $"api/User/{username}/Library");

            HttpResponseMessage response = await Client.SendAsync(request);
            HttpResponseMessage response2 = await Client.SendAsync(request2);

            if (!response.IsSuccessStatusCode || !response2.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized || response2.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Error", "Home");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            string responseBody2 = await response2.Content.ReadAsStringAsync();

            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(responseBody);
            List<UserGame> userGames = JsonConvert.DeserializeObject<List<UserGame>>(responseBody2);

            return View("Index", games.Select(g => new FullGame { Game = g, Selected = userGames.Any(a => a.Game.GameId == g.GameId) }).ToList());
        }

        // GET: UserGame by Filtered Results
        public async Task<ActionResult> Filter([FromForm]string lowPrice, [FromForm]string highPrice, [FromForm]string lowRating, [FromForm]string highRating, [FromForm]int[] devId, [FromForm]int[] tagId)
        {
            bool parseLP = int.TryParse(lowPrice, out int lowPriceInt);
            bool parseHP = int.TryParse(highPrice, out int highPriceInt);
            bool parseLR = int.TryParse(lowRating, out int lowRatingInt);
            bool parseHR = int.TryParse(highRating, out int highRatingInt);

            //if (parseLP == false || parseHP == false || parseLR == false || parseHR == false)
            //{
            //    return RedirectToAction("Index");
            //}
            //else // all TryParse methods worked
            {
                // building one object that can transport data to API
                int[] priceArray = new int[2];
                priceArray[0] = lowPriceInt;
                priceArray[1] = highPriceInt;

                int[] ratingArray = new int[2];
                ratingArray[0] = lowRatingInt;
                ratingArray[1] = highRatingInt;

                int[][] storageArray = new int[4][];
                storageArray[0] = priceArray;
                storageArray[1] = ratingArray;
                storageArray[2] = devId;
                storageArray[3] = tagId;

                // building request to send to API
                var username = ViewBag.LoggedInUser;
                if (username == "")
                {
                    return RedirectToAction("Login", "Account");
                }
                
                HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Game/Filter", storageArray);
                HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, $"api/User/{username}/Library");

                HttpResponseMessage response = await Client.SendAsync(request);
                HttpResponseMessage response2 = await Client.SendAsync(request2);

                if (!response.IsSuccessStatusCode || !response2.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized || response2.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return RedirectToAction("Error", "Home");
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                string responseBody2 = await response2.Content.ReadAsStringAsync();

                List<Game> games = JsonConvert.DeserializeObject<List<Game>>(responseBody);
                List<UserGame> userGames = JsonConvert.DeserializeObject<List<UserGame>>(responseBody2);

                return View("Index", games.Select(g => new FullGame { Game = g, Selected = userGames.Any(a => a.Game.GameId == g.GameId) }).ToList());
            }
        }



        // GET: UserGame/Details/5
        public async Task<ActionResult> Details(int id, bool selected)
        {

            var username = ViewBag.LoggedInUser;
            if (username == "")
            {
                return RedirectToAction("Login", "Account");
            }

            HttpRequestMessage request1 = CreateRequest(HttpMethod.Get, $"api/Game/{id}");
            HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, $"api/Dlc/Game/{id}");
            HttpRequestMessage request3 = CreateRequest(HttpMethod.Get, $"api/User/{username}/Library/Dlc");



            HttpResponseMessage response1 = await Client.SendAsync(request1);
            HttpResponseMessage response2 = await Client.SendAsync(request2);
            HttpResponseMessage response3 = await Client.SendAsync(request3);



            if (!response1.IsSuccessStatusCode || !response2.IsSuccessStatusCode)
            {
                if (response1.StatusCode == HttpStatusCode.Unauthorized || response2.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Error", "Home");
            }
            string responseBody1 = await response1.Content.ReadAsStringAsync();
            string responseBody2 = await response2.Content.ReadAsStringAsync();
            string responseBody3 = await response3.Content.ReadAsStringAsync();



            Game game = JsonConvert.DeserializeObject<Game>(responseBody1);
            List<Dlc> dlcs = JsonConvert.DeserializeObject<List<Dlc>>(responseBody2);
            List<UserDlc> userDlcs = JsonConvert.DeserializeObject<List<UserDlc>>(responseBody3);


            return View(new FullUserGame { Game = game, SelectDlcs = dlcs.Select(d => new FullDlc { Dlc = d,Selected=userDlcs.Any(a => a.Dlc.Dlcid == d.Dlcid)}).ToList(), Selected = selected });
        }

        // GET: UserGame/Create
        public async Task<ActionResult> Purchase(int id)
        {
            //var cookies = Request.Cookies.Keys;
            //var request = Request.Cookies["ApiAuth"];
            string username = ViewBag.LoggedInUser;
            if(username == "")
            {
                return RedirectToAction("Login", "Account");
            }
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Game/{id}");
            HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, $"api/User/{username}");

            HttpResponseMessage response = await Client.SendAsync(request);
            HttpResponseMessage response2 = await Client.SendAsync(request2);

            if (!response.IsSuccessStatusCode || !response2.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized || response2.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Error", "Home");

            }
            string resString = await response.Content.ReadAsStringAsync();
            string resString2 = await response2.Content.ReadAsStringAsync();

            return View(new UserGame { PurchaseDate = DateTime.Now,Game = JsonConvert.DeserializeObject<Game>(resString), User = JsonConvert.DeserializeObject<User>(resString2) });
        }

        // POST: UserGame/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Purchase(UserGame userGame)
        {
            int gameid = userGame.Game.GameId;
            DateTime purchaseDate = userGame.PurchaseDate;

            try
            {
                var username = ViewBag.LoggedInUser;

                HttpRequestMessage request = CreateRequest(HttpMethod.Post, $"api/User/{username}/Library/{gameid}", purchaseDate);
                HttpRequestMessage request2 = CreateRequest(HttpMethod.Patch, $"api/User/{username}/Wallet", userGame.User.Wallet);

                HttpResponseMessage response = await Client.SendAsync(request);
                HttpResponseMessage response2 = await Client.SendAsync(request2);


                if (!response.IsSuccessStatusCode || !response2.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return RedirectToAction("Error", "Home");

                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserGame/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserGame/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserGame/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserGame/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}