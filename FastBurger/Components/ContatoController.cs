using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Components
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
