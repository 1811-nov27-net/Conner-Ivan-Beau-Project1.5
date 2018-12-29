using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VaporWebSite.App.Controllers
{
    public abstract class ARequestController : Controller
    {
        private static readonly Uri requestUri = new Uri("https://localhost:44360/");
        protected static readonly string cookieName = "ApiAuth";

        public HttpClient Client { get; set; }

        public ARequestController(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpRequestMessage CreateRequest(HttpMethod method, string uri, object body = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, new Uri(requestUri,uri));

            if(body != null)
            {
                string rawSerialized = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(rawSerialized, Encoding.UTF8, "application/json");
            }

            var cookieValue = Request.Cookies[cookieName];
            if(cookieValue != null)
            {
                request.Headers.Add("Cookie", new CookieHeaderValue(cookieName, cookieValue).ToString());
            }

            return request;
        }
    }
}
