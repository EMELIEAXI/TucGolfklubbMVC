using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class SpelaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Greenfee()
        {
            return View();
        }

        public IActionResult Foretag() 
        {
            return View();
        }
    }
}
