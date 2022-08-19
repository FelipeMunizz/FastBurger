using FastBurger.Models;

namespace FastBurger.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
