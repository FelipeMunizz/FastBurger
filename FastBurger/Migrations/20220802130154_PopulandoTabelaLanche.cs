using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBurger.Migrations
{
    public partial class PopulandoTabelaLanche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Classico',17.99,'Hamburger artesanal(90g), queijo, maionese, alface e tomate','Delicioso Hamburguer artesanal com 90 gramas de Alcatra, com queijo branco, alface Crespa e tomate Roma','classico.jpg','classico.jpg',0,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE Lanches");
        }
    }
}
