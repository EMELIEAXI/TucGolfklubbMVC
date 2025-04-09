using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class RestaurangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
