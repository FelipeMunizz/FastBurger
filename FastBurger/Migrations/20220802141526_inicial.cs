using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBurger.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Lanches",
                columns: table => new
                {
                    LancheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LancheNome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DescricaoCurta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescricaoDetalhada = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImagemThumbnailUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsLanchePreferido = table.Column<bool>(type: "bit", nullable: false),
                    EmEstoque = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanches", x => x.LancheId);
                    table.ForeignKey(
                        name: "FK_Lanches_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lanches_CategoriaId",
                table: "Lanches",
                column: "CategoriaId");

            migrationBuilder.Sql("INSERT INTO  Categorias(CategoriaNome, Descricao)" +
                "VALUES ('Hamburguer', 'Hamburguer de carne feito de forma artesanal')");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Classico',17.99,'Hamburger artesanal(90g), queijo, maionese, alface e tomate','Delicioso Hamburguer artesanal com 90 gramas de Alcatra, com queijo branco, alface Crespa e tomate Roma','classico.jpg','classico.jpg',0,1)");


            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                    "VALUES(1,'Burgerniaco',25.99,'Hamburguer artesanal(120g), cheddar, cebola crispy, picles e tomate','Delicioso Hamburguer artesanal com 120 gramas de Alcatra, no pão brioche, com cheddar cremoso da casa, salpicado com cebola crispy e tomate Roma','burgerniaco.jpg','burgerniaco.jpg',0,1)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Cheddar Onion Burguer',30.99,'Hamburger artesanal(120g), cheddar, barbecue, tomate, cebola, cebola caramelizada, cebola crispy, onion rings e cheddar','Hambúrguer artesanal 120g, Queijo Cheddar, Pão Brioche, Barbecue, Tomate, Cebola, Cebola Caramelizada, Cebola Crispy, Onion Rings e Cheddar Cremoso da casa','classico.jpg','classico.jpg',0,1)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Dublo Bacon',35.99,'Dois Hamburguer artesanal(120g), queijo, barbecue e bacon','Dois suculentos hambúrgueres artesanais de 120g com fatias de queijo prato, pão brioche, molho barbecue e bacon fatiado crocante','duplobacon.jpg','duplobacon.jpg',0,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lanches");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
