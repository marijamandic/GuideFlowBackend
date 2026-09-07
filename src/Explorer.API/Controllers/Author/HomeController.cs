using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
