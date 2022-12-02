using FastBurger.Data;
using FastBurger.Models;
using Microsoft.EntityFrameworkCore;

namespace FastBurger.Areas.Admin.Services;

public class RelatorioVendaService
{
    private readonly AppDbContext _context;

    public RelatorioVendaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pedido>> FindByDateAsync(DateTime? startDate, DateTime? endDate)
    {
        var result = from obj in _context.Pedidos select obj;

        if (startDate.HasValue)
        {
            result = result.Where(x => x.PedidoEnviado >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            result = result.Where(x => x.PedidoEnviado <= endDate.Value);
        }

        return await result
                        .Include(l => l.PedidoItens)
                        .ThenInclude(l => l.Lanche)
                        .OrderByDescending(x => x.PedidoEnviado)
                        .ToListAsync();
    }
}
