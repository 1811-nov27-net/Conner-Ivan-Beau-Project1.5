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
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Game");
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Error", "Home");
            }

            string responseBody = await response.Content.ReadAsStringAsync();


            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(responseBody);
            
            return View(games);
        }

        // GET: UserGame by Searched Name
        public async Task<ActionResult> Search(string searchString)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Game", searchString);
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Error", "Home");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(responseBody);

            return View(games);
        }

        // GET: UserGame/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserGame/Create
        public ActionResult Purchase(int id)
        {
            //var cookies = Request.Cookies.Keys;
            //var request = Request.Cookies["ApiAuth"];
            if(ViewBag.LoggedInUser == "")
            {
                return RedirectToAction("Login", "Account");
            }
            return View(new UserGame { Game = new Game { GameId = id} });
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
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
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