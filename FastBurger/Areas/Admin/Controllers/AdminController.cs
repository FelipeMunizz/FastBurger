using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize("Admin")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
