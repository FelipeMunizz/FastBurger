using FastBurger.Models;
using FastBurger.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FastBurger.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private string _token = "49E90CBC9A6B45F2B52880EC5FCD972B";

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
        public IActionResult Pagamento()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0;

            string publicKey = await CriarPublicKey();

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
                var pedidoMercadoPago = await RequestPedidoMercadoPago(pedido);
                if (pedidoMercadoPago.id == "-15000")
                    return View(pedido);

                //define as mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();
                ViewBag.id = pedidoMercadoPago.id;
                ViewBag.publicKey = publicKey;

                //exibe a view com dados do cliente e do pedido
                return RedirectToAction("Pagamento", pedido);
            }
            return View(pedido);
            #endregion
        }

        private async Task<ResponsePedidoPagSeguro> RequestPedidoMercadoPago(Pedido pedido)
        {
            Customer costumers = new Customer
            {
                Name = $"{pedido.Nome} {pedido.Sobrenome}",
                Email = pedido.Email,
                Tax_id = RemoveCaracteresEspeciais(pedido.Documento)
            };

            List<Item> items = new List<Item>();
            foreach (var itens in pedido.PedidoItens)
            {
                Item item = new Item
                {
                    Name = itens.Lanche.LancheNome,
                    Quantity = itens.Quantidade,
                    Unit_amount = Convert.ToInt32(RemoveCaracteresEspeciais(itens.Lanche.Preco.ToString()))
                };

                items.Add(item);
            }
            Item[] itemsArray = items.ToArray();

            PagueSeguro pedidoPagSeguro = new PagueSeguro
            {
                Customer = costumers,
                Items = itemsArray,
                Reference_id = pedido.PedidoId
            };

            string JsonData = JsonSerializer.Serialize(pedidoPagSeguro).ToLower();
            Uri url = new Uri("https://sandbox.api.pagseguro.com/orders");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            client.DefaultRequestHeaders.Add("accept", "application/json");
            HttpContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(url, content);
                string textResponse = await response.Content.ReadAsStringAsync();
                ResponsePedidoPagSeguro responsePedidoPagSeguro = JsonSerializer.Deserialize<ResponsePedidoPagSeguro>(textResponse);
                return responsePedidoPagSeguro;
            }
            catch (Exception)
            {
                return new ResponsePedidoPagSeguro
                {
                    id = "-15000"
                };
            }
        }

        private async Task<string> CriarPublicKey()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "type", "card" }
            };
            string JsonData = JsonSerializer.Serialize(data);
            Uri url = new Uri("https://sandbox.api.pagseguro.com/public-keys");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            client.DefaultRequestHeaders.Add("accept", "application/json");
            HttpContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(url, content);
                string textResponse = await response.Content.ReadAsStringAsync();
                ResponsePedidoPagSeguro responsePedidoPagSeguro = JsonSerializer.Deserialize<ResponsePedidoPagSeguro>(textResponse);
                return responsePedidoPagSeguro.public_key;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        static string RemoveCaracteresEspeciais(string input)
        {
            string pattern = "[^a-zA-Z0-9]";
            string replacement = "";
            Regex regex = new Regex(pattern);

            string result = regex.Replace(input, replacement);
            return result;
        }
    }
}
