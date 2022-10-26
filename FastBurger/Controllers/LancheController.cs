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

        #region Lista todos os Lanches ou Produtos
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
        #endregion

        #region Detalhes dos Lanches ou Produtos
        public IActionResult Details(int lancheId)
        {
            var lanche = _repository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            return View(lanche);
        }
        #endregion

        #region Busca Lanches ou Produtos
        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty("searchString"))
            {
                lanches = _repository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _repository.Lanches
                    .Where(p => p.LancheNome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                    categoriaAtual = "Lanches";
                else
                    categoriaAtual = "Nenhum Lanche Encontrado";
            }
            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }
        #endregion
    }
}