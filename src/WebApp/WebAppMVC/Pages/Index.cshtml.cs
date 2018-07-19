using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace WebAppMVC.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {
            var tokenClient = new TokenClient("http://localhost:5000/connect/token", "mvc", "secret");
            var tokenResponse =  tokenClient.RequestClientCredentialsAsync("api1");
            ViewData["token"] = tokenResponse.Result.AccessToken;
        }
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
        //public async Task<IActionResult> CallApiUsingClientCredentials()
        //{
        //    var tokenClient = new TokenClient("http://localhost:5000/connect/token", "mvc", "secret");
        //    var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

        //    var client = new HttpClient();
        //    client.SetBearerToken(tokenResponse.AccessToken);
        //    var content = await client.GetStringAsync("http://localhost:5001/identity");

        //    ViewBag.Json = JArray.Parse(content).ToString();
        //    return View("Json");
        //}

        //public async Task<IActionResult> CallApiUsingUserAccessToken()
        //{
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");

        //    var client = new HttpClient();
        //    client.SetBearerToken(accessToken);
        //    var content = await client.GetStringAsync("http://localhost:5001/identity");

        //    ViewBag.Json = JArray.Parse(content).ToString();
        //    return View("Json");
        //}
    }
}
