using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBurger.Models
{
    [Table("Lanches")]
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage ="Informe o nome do Lanche")]
        [Display(Name ="Nome do Lanche")]
        [StringLength(80, MinimumLength = 5, ErrorMessage ="O {0} deve ter no mínimo {1} caracteres e no máximo {2} caracteres.")]
        public string? LancheNome { get; set; }

        [Required(ErrorMessage ="Informe o preço do lanche.")]
        [Display(Name ="Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99, ErrorMessage ="O preço deve ser entre R$1,00 e R$999,99")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe uma descrição do lanche")]
        [Display(Name = "Descrição")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres e no máximo {2} caracteres.")]
        public string? DescricaoCurta { get; set; }

        [Required(ErrorMessage = "Informe uma descrição detalhada do lanche")]
        [Display(Name = "Descrição Detalhada")]
        [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres e no máximo {2} caracteres.")]
        public string? DescricaoDetalhada { get; set; }

        [Display(Name ="Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage ="O {0} deve ter no máximo {1}")]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1}")]
        public string? ImagemThumbnailUrl { get; set; }

        [Display(Name ="Preferido?")]
        public bool IsLanchePreferido { get; set; }
        
        [Display(Name ="Estoque")]
        public bool EmEstoque { get; set; }

        //Relacionamento 1:n
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }
    }
}
