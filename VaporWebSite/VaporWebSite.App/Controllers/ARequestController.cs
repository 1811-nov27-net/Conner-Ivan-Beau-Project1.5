using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VaporWebSite.App.Controllers
{
    public abstract class ARequestController : Controller
    {
        private static readonly Uri requestUri = new Uri("");

        public HttpClient Client { get; set; }

        public ARequestController(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpRequestMessage CreateRequest(HttpMethod method, string uri, object body = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, uri);

            if(body != null)
            {
                string rawSerialized = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(rawSerialized, Encoding.UTF8, "application/json");
            }

            return request;
        }
    }
}
