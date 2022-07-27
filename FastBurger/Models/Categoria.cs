namespace FastBurger.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string? CategoriaNome { get; set; }
        public string? Descricao { get; set; }

        //Relacionamento 1:n
        public List<Produto>? Produtos { get; set; }
    }
}
