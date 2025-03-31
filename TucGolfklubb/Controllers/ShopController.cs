using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductShop()
        {
            return View();
        }
    }
}
