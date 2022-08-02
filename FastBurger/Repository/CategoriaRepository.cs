using FastBurger.Data;
using FastBurger.Models;
using FastBurger.Repository.Interfaces;

namespace FastBurger.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
