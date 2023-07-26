using FastBurger.Models;
using FastBurger.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0;

            #region Obtem os itens do carrinho de compra do cliente

            //obtem os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            //verifica se existem itens de pedido
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Carrinho Vazio");
            }

            //calcula o total de intens e o total do pedido
            foreach (var item in itens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += item.Lanche.Preco * item.Quantidade;
            }

            //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;
            #endregion

            #region Criação do Pedido
            //valida os dados do pedido
            if (ModelState.IsValid)
            {
                //cria o pedido e os detalhes
                _pedidoRepository.CriarPedido(pedido);

                //define as mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                var pedidoMercadoPago = await RequestPedidoMercadoPago(pedido);

                //limpa o carrinho do cliente
                _carrinhoCompra.LimparCarrinho();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
            #endregion
        }

        private async Task<object> RequestPedidoMercadoPago(Pedido pedido)
        {
            using System.Net.Http.Headers;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://sandbox.api.pagseguro.com/orders"),
                Headers =
    {
        { "accept", "application/json" },
        { "Authorization", "sha512-txa/QoWhm5hKGQssT12xSHQchU2ZPttPsCPdYJbRfpCrJ7BUmaCzpDEOj5xc4iBd0QKWF1yxTm1WvEyU/r21nQ==?972B" },
    },
                Content = new StringContent("{\"customer\":{\"name\":\"Felipe Muniz\",\"email\":\"contatoninetec@gmail.com\",\"tax_id\":\"35918159851\"},\"items\":[{\"name\":\"Classico\",\"quantity\":1,\"unit_amount\":1290}],\"reference_id\":\"1\"}")
                {
                    Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            return new object();
        }
    }
}
