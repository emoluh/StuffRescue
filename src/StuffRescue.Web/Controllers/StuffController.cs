using Microsoft.AspNetCore.Mvc;

namespace StuffRescue.Web.Controllers
{
    public class StuffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}