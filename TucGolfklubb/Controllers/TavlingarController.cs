using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class TavlingarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Anmalan()
        {
            return View();
        }
        public IActionResult Kalender() 
        {
            return View();
        }
        public IActionResult resultat()
        {
            return View();
        }
    }
}
