using FastBurger.Models;
using FastBurger.Repository.Interfaces;
using FastBurger.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _repository;
        public LancheController(ILancheRepository repository)
        {
            _repository = repository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _repository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _repository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(l => l.LancheNome);
                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }
    }
}
