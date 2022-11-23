using Microsoft.AspNetCore.Mvc;

namespace RateLimit.Controllers
{
    public class RateLimitOptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
