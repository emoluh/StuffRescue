using Microsoft.AspNetCore.Mvc;

namespace StuffRescue.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Ping")]
    public class PingController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "Pong" ;
        }
    }
}