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
    public class DlcController : ARequestController
    {

        public DlcController(HttpClient client) : base (client)
        {

        }

        // GET: Dlc
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Dlc");
            HttpRequestMessage request2 = CreateRequest(HttpMethod.Get, "api/Game");

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


            List<Dlc> dlcs = JsonConvert.DeserializeObject<List<Dlc>>(responseBody);

            List<Game> rawGames = JsonConvert.DeserializeObject<List<Game>>(responseBody2).OrderBy(a => a.GameId).ToList();

            List<FullDlc> fullDlcs = new List<FullDlc>();
            foreach(var dlc in dlcs)
            {
                Game temp = rawGames.First(a => a.GameId == dlc.GameId);
                fullDlcs.Add(new FullDlc { Dlc = dlc, Game = temp });
            }


            return View(fullDlcs);
        }

        // GET: Dlc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dlc/Create
        public async Task<ActionResult> Create()
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


            return View(new FullDlc { Game = new Game(), Dlc = new Dlc(), AllGames = games});
        }

        // POST: Dlc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FullDlc fulldlc)
        {
            Dlc dlc = fulldlc.Dlc;
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Dlc", dlc);
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

        // GET: Dlc/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Dlc/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error");
            }
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Dlc>(resString));
        }

        // POST: Dlc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Dlc dlc)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Put, $"api/Dlc/{id}", dlc);
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

        // GET: Dlc/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Dlc/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error");
            }
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Dlc>(resString));
        }

        // POST: Dlc/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Delete, $"api/Dlc/{id}");
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