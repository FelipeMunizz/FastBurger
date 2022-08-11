using FastBurger.Models;
using FastBurger.Repository.Interfaces;
using FastBurger.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FastBurger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferidos
            };
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}