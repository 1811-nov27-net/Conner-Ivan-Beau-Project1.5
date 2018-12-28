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
    public class DeveloperController : ARequestController
    {

        public DeveloperController(HttpClient client) : base(client)
        {
        }

        // GET: Developer
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Developer");
            HttpResponseMessage response = await Client.SendAsync(request);

            if(!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            List<Developer> developers = JsonConvert.DeserializeObject<List<Developer>>(responseBody);

            return View(developers);
        }

        // GET: Developer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Developer/Create
        public ActionResult Create()
        {
            //might want some logic for security here
            return View(new Developer());
        }

        // POST: Developer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Developer developer)
        {
            try
            {
                // TODO: Add insert logic here
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Developer", developer);
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

        // GET: Developer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //Client.GetStringAsync()
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Developer/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Developer>(resString));
        }

        // POST: Developer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Developer developer)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Put, $"api/Developer/{id}", developer);
                HttpResponseMessage response = await Client.SendAsync(request);
                if (response.IsSuccessStatusCode)
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

        // GET: Developer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Developer/Delete/5
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