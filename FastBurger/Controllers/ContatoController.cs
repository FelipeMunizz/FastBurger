using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
