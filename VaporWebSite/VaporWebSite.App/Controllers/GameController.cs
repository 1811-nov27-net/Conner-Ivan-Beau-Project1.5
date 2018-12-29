using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VaporWebSite.App.Models;

namespace VaporWebSite.App.Controllers
{
    public class GameController : ARequestController
    {

        public GameController(HttpClient client) : base(client)
        {
            //nothing to do
        }

        // GET: Game
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Game");
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(responseBody);

            return View(games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            //potentail logic for security
            return View(new Game());
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Game game)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Game", game);
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Game/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Game>(resString));
        }

        // POST: Game/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game game)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Put, $"api/Game/{id}", game);
                HttpResponseMessage response = await Client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Game/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Game>(resString));
        }

        // POST: Game/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Delete, $"api/Game/{id}");
                HttpResponseMessage response = await Client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}