using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBurger.Migrations
{
    public partial class PopulandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO  Categorias(CategoriaNome, Descricao)" +
                "VALUES ('Hamburguer', 'Hamburguer de carne feito de forma artesanal')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
