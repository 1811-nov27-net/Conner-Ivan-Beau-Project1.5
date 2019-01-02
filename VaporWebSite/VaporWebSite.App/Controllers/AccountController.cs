using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VaporWebSite.App.Models;

namespace VaporWebSite.App.Controllers
{
    public class AccountController : ARequestController
    {

        public AccountController(HttpClient client) : base(client)
        {
            //nothing else to do
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Account account)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View();
                }
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Account/Login", account);
                HttpResponseMessage response = await Client.SendAsync(request);

                if(!response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        ModelState.AddModelError("Password", "Incorrect username or password");
                    }
                    return View();
                }

                var success = PassCookiesToClient(response);
                if(!success)
                {
                    return View("Error");
                }
                //succeeded
                return RedirectToAction("Index", "UserGame");

            } catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> RegisterInput(Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Account/Register", account);
                HttpResponseMessage response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError("Password", "User already exists or invalid character");
                    }
                    return View("Register");
                }

                var success = PassCookiesToClient(response);
                if (!success)
                {
                    return View("Error");
                }
                //succeeded
                return RedirectToAction("Index", "UserGame");

            }
            catch (Exception)
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                HttpRequestMessage request = CreateRequest(HttpMethod.Post, "api/Account/Logout");
                HttpResponseMessage response = await Client.SendAsync(request);

                var success = PassCookiesToClient(response);
                if (!success)
                {
                    return View("Error");
                }
                //succeeded
                return RedirectToAction("Login");

            }
            catch (Exception)
            {
                return View();
            }
        }

        private bool PassCookiesToClient(HttpResponseMessage response)
        {
            if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values))
            {
                var authValue = values.FirstOrDefault(x => x.StartsWith(cookieName));
                if(authValue != null)
                {
                    Response.Headers.Add("Set-Cookie", authValue);
                    return true;
                }
            }
            return false;
        }
    }
}