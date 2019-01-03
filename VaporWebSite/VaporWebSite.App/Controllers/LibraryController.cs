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
    public class LibraryController : ARequestController
    {

        public LibraryController(HttpClient client) : base(client)
        {

        }

        // GET: Library
        public async Task<ActionResult> Index()
        {
            var username = ViewBag.LoggedInUser;
            if(username == "")
            {
                return RedirectToAction("Login", "Account");
            }

            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/User/{username}/Library");
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

            List<UserGame> userGames = new List<UserGame>();

            try
            {
                userGames = JsonConvert.DeserializeObject<List<UserGame>>(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(userGames);
        }

        // GET: Library/Details/5
        public async Task<ActionResult> Details(string username, int id)
        {
            if (username == "")
            {
                return RedirectToAction("Login", "Account");
            }
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Review/{username}/{id}");
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

            FullUserGame userGames = new FullUserGame();
            try
            {
                userGames = JsonConvert.DeserializeObject<FullUserGame>(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(userGames);
        }

        // GET: Library/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Library/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Library/Edit/5
        public async Task<ActionResult> Edit(string username, int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Review/{username}/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);
            string resString = await response.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<UserGame>(resString);
            return View(JsonConvert.DeserializeObject<UserGame>(resString));
        }

        // POST: Library/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserGame usergame)
        {
            try
            {
                string username = ViewBag.LoggedInUser;
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, $"api/Review/{username}/{usergame.Game.GameId}", new ScoreReview { Review=usergame.Review, Score=usergame.Score});
                HttpResponseMessage response = await Client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: Library/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Library/Delete/5
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