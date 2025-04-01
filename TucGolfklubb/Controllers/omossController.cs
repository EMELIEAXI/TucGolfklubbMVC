using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class omossController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Faciliteter()
        {
            return View();
        }

        public IActionResult Banorna()
        {
            return View();
        }
        public IActionResult Historia()
        {
            return View();
        }
    }
}
