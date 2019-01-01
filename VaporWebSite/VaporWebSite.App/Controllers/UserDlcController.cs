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
    public class UserDlcController : ARequestController
    {

        public UserDlcController(HttpClient client) : base(client)
        {
            //nothing to do
        }


        // GET: UserDlc
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserDlc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserDlc/Create
        public async Task<ActionResult> Purchase(int id)
        {
            if (ViewBag.LoggedInUser == "")
            {
                return RedirectToAction("Login", "Account");
            }

            HttpRequestMessage request = CreateRequest(HttpMethod.Get, $"api/Dlc/{id}");
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


            Dlc dlc = JsonConvert.DeserializeObject<Dlc>(responseBody);

            return View(new UserDlc { Dlc = dlc });
        }

        // POST: UserDlc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Purchase(UserDlc userdlc)
        {
            int dlcid = userdlc.Dlc.Dlcid;

            try
            {
                var username = ViewBag.LoggedInUser;

                HttpRequestMessage request = CreateRequest(HttpMethod.Post, $"api/User/{username}/Library/Dlc",dlcid);
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return RedirectToAction("Error", "Home");

                }


                return RedirectToAction(nameof(Index), "UserGame");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserDlc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserDlc/Edit/5
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

        // GET: UserDlc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserDlc/Delete/5
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