namespace FastBurger.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? LancheNome { get; set; }
        public decimal Preco { get; set; }
        public string? DescricaoCurta { get; set; }
        public string? DescricaoDetalhada { get; set; }
        public string? ImagemUrl { get; set; }
        public string? ImagemThumbnailUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }

        //Relacionamento 1:n
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
