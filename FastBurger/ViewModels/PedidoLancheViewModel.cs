using FastBurger.Models;

namespace FastBurger.ViewModels;

public class PedidoLancheViewModel
{
    public Pedido Pedido { get; set; }
    public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set;}
}
