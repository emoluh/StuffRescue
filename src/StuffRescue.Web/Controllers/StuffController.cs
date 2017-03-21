using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StuffRescue.Web.Models.StuffViewModels;
using System.IO;
using System.Threading.Tasks;

namespace StuffRescue.Web.Controllers
{
    public class StuffController : Controller
    {
        private IHostingEnvironment _environment;
        public StuffController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ItemViewModel data)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "Photos");
            foreach (var file in data.Photos)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return View();
        }
    }
}