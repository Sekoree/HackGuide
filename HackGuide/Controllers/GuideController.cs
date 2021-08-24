using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackGuide.Controllers
{
    public class GuideController : Controller
    {
        private readonly ILogger<GuideController> _logger;

        public GuideController(ILogger<GuideController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View("BuildYourVita");
        }

        [HttpPost]
        public IActionResult CreateVita()
        {
            return View("BuildYourVita");
        }
    }
}
