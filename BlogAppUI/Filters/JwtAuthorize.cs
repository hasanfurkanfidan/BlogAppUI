using BlogAppUI.Extentions;
using BlogAppUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlogAppUI.Filters
{
    public class JwtAuthorize:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token =  context.HttpContext.Session.GetString("token");

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new RedirectToActionResult("SignIn","Account", new { @area = "" });

            }
            else
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",token);
                var response = httpClient.GetAsync("http://localhost:59229/api/Auth/ActiveUser").Result;
                if (response.IsSuccessStatusCode)
                {
                    var user = JsonConvert.DeserializeObject<AppUserViewModel>(response.Content.ReadAsStringAsync().Result);
                    context.HttpContext.Session.SetObject(user, "activeUser");
                }
                else
                {
                    context.Result = new RedirectToActionResult("SignIn", "Account", new {@area="" });
                }

            }
            base.OnActionExecuting(context);
        }
    }
}
