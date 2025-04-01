using Microsoft.AspNetCore.Mvc;

namespace TucGolfklubb.Controllers
{
    public class KontaktController : Controller
    {
        public IActionResult Kontakt()
        {
            return View();
        }
    }
}
