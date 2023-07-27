using FastBurger.Models;
using FastBurger.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        public async Task<IActionResult> Checkout(Pedido pedido)
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
            string token = "49E90CBC9A6B45F2B52880EC5FCD972B";
            var costumers = new Customer
            {
                Name = $"{pedido.Nome} {pedido.Sobrenome}",
                Email = pedido.Email,
                Tax_id = pedido.Documento
            };

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://sandbox.api.pagseguro.com/orders"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    request.Headers.TryAddWithoutValidation("accept", "application/json");

                    
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                }
            }

            //            Content = new StringContent("{\"customer\":{\"name\":\"Felipe Muniz\",\"email\":\"contatoninetec@gmail.com\",\"tax_id\":\"35918159851\"},\"items\":[{\"name\":\"Classico\",\"quantity\":1,\"unit_amount\":1290}],\"reference_id\":\"1\"}")
            //            {
            //                Headers =
            //    {
            //        ContentType = new MediaTypeHeaderValue("application/json")
            //    }
            //            }
            //        };
            //        using (var response = await client.SendAsync(request))
            //        {
            //            response.EnsureSuccessStatusCode();
            //            var body = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine(body);
            //}
            return new object();
        }
    }
}
