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

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Game/Create
        public async Task<ActionResult> Create()
        {
            //potential logic for security

            //need to send over all the tags and developers to choose from

            
            HttpRequestMessage request1 = CreateRequest(HttpMethod.Get, "api/Developer");
            HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, "api/Tag");
            HttpResponseMessage response1 = await Client.SendAsync(request1);
            HttpResponseMessage response2 = await Client.SendAsync(request2);


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


            List<Developer> developers = JsonConvert.DeserializeObject<List<Developer>>(responseBody1);
            List<Tag> tags = JsonConvert.DeserializeObject<List<Tag>>(responseBody2);


            return View(new FullGame { Game = new Game(),Developers = developers, Tags = tags.Select(t => new FilterTag { Tag = t, Selected = false}).ToList()});
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FullGame rawgame)
        {
            Game game = rawgame.Game;
            try
            {
                if (ModelState.IsValid)
                {
                    List<Tag> addedTags = new List<Tag>();
                    foreach(var t in rawgame.Tags)
                    {
                        if(t.Selected)
                        {
                            addedTags.Add(t.Tag);
                        }
                    }
                    game.Tags = addedTags;
                    HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Game", game);
                    HttpResponseMessage response = await Client.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                        return RedirectToAction("Error", "Home");
                    }
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
    }
}