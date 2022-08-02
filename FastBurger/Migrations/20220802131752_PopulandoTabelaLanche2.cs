using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBurger.Migrations
{
    public partial class PopulandoTabelaLanche2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Burgerniaco',25.99,'Hamburguer artesanal(120g), cheddar, cebola crispy, picles e tomate','Delicioso Hamburguer artesanal com 120 gramas de Alcatra, no pão brioche, com cheddar cremoso da casa, salpicado com cebola crispy e tomate Roma','burgerniaco.jpg','burgerniaco.jpg',0,1)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Cheddar Onion Burguer',30.99,'Hamburger artesanal(120g), cheddar, barbecue, tomate, cebola, cebola caramelizada, cebola crispy, onion rings e cheddar','Hambúrguer artesanal 120g, Queijo Cheddar, Pão Brioche, Barbecue, Tomate, Cebola, Cebola Caramelizada, Cebola Crispy, Onion Rings e Cheddar Cremoso da casa','classico.jpg','classico.jpg',0,1)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Dublo Bacon',35.99,'Dois Hamburguer artesanal(120g), queijo, barbecue e bacon','Dois suculentos hambúrgueres artesanais de 120g com fatias de queijo prato, pão brioche, molho barbecue e bacon fatiado crocante','duplobacon.jpg','duplobacon.jpg',0,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE Lanches");
        }
    }
}
