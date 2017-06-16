using Microsoft.AspNetCore.Mvc;

namespace StuffRescue.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string callbackUrl = HttpContext.Request.Query["callbackUrl"].ToString();

            if (!string.IsNullOrEmpty(callbackUrl))
            {
                ViewData["Message"] = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
