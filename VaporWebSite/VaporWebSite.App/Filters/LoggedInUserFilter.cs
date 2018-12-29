using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VaporWebSite.App.Controllers;

namespace VaporWebSite.App.Filters
{
    public class LoggedInUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as ARequestController;
            if(controller != null)
            {
                HttpRequestMessage request = controller.CreateRequest(HttpMethod.Get, "api/account/loggedinuser");
                HttpResponseMessage response = await controller.Client.SendAsync(request);

                if(!response.IsSuccessStatusCode)
                {
                    controller.ViewBag.LoggedInUser = "";
                }
                controller.ViewBag.LoggedInUser = await response.Content.ReadAsStringAsync();
            }
            var resultContext = await next();
        }
    }
}
