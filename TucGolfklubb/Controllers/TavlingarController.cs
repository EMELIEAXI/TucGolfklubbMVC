using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class TavlingarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
