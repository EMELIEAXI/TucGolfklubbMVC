using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class NyheterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
