using LiveChat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.AspNet;
using System.Diagnostics;

namespace LiveChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationUserService _applicationUserService;
        public HomeController(ILogger<HomeController> logger, IApplicationUserService applicationUserService)
        {
            _logger = logger;
            _applicationUserService = applicationUserService;
        }

        public IActionResult Index()
        {
            var name = User.Identity.Name;
            var roleTitles = _applicationUserService.GetRole(name);
            bool isAdmin = false;
            if (roleTitles.Contains("Admin"))
            {
                isAdmin = true;
            }
            ViewData["IsAdmin"] = isAdmin;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
