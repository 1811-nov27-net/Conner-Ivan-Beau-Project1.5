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
    public class TagController : ARequestController
    {

        public TagController(HttpClient client) : base(client)
        {

        }

        // GET: Tag
        public async Task<ActionResult> Index()
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Tag");
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

            List<Tag> tags = JsonConvert.DeserializeObject<List<Tag>>(responseBody);

            return View(tags);
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tag/Create
        public async Task<ActionResult> Create()
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, "api/Tag");
            HttpResponseMessage response = await Client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error");
            }
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tag tag)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Tag", tag);
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

        // GET: Tag/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Tag/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Tag>(resString));
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Tag tag)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Put, $"api/Tag/{id}", tag);
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

        // GET: Tag/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Tag/{id}");
            HttpResponseMessage response = await Client.SendAsync(request);
            string resString = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<Tag>(resString));
        }

        // POST: Tag/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpRequestMessage request = CreateRequest(HttpMethod.Delete, $"api/Tag/{id}");
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