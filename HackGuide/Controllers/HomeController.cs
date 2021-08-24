using HackGuide.Models;
using HackGuide.PsnUtil;
using HackGuide.PsnUtil.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HackGuide.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PsnClient _psnClient;

        public HomeController(ILogger<HomeController> logger, PsnClient psnClient)
        {
            _logger = logger;
            this._psnClient = psnClient;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("QuickLinks")]
        public IActionResult QuickLinks()
        {
            return View();
        }

        [HttpPost]
        [Route("QuickLinks")]
        public async Task<IActionResult> QuickLinks(string codeURL)
        {
            try
            {
                if (codeURL.StartsWith("https"))
                {
                    var token = await this._psnClient.GetTokenResponse(codeURL);
                    var info = await this._psnClient.GetUserInfoResponse(token);
                    this.ViewData["AID"] = await this._psnClient.GetAID(info);
                    this.ViewData["UserInfo"] = info;
                }
                else
                {
                    this.ViewData["AID"] = codeURL;
                    this.ViewData["UserInfo"] = new UserInfoResponse
                    {
                        OnlineId = "Not logged in"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex);
            }
            return View();
        }

        [HttpPost]
        public IActionResult ApiRedirect(string aid, string pack)
        {
            return RedirectPermanent($"https://{this.Request.Host.Value}/api/hb/get{pack}/{aid}");
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
