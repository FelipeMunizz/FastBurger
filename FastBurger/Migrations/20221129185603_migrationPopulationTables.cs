﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBurger.Migrations
{
    /// <inheritdoc />
    public partial class migrationPopulationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO  Categorias(CategoriaNome, Descricao)" +
                "VALUES ('Hamburguer', 'Hamburguer de carne feito de forma artesanal')");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Classico',17.99,'Hamburger artesanal(90g), queijo, maionese, alface e tomate','Delicioso Hamburguer artesanal com 90 gramas de Alcatra, com queijo branco, alface Crespa e tomate Roma','classico.png','classico.jpg',0,1)");


            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                    "VALUES(1,'Burguerniaco',25.99,'Hamburguer artesanal(120g), cheddar, cebola crispy, picles e tomate','Delicioso Hamburguer artesanal com 120 gramas de Alcatra, no pão brioche, com cheddar cremoso da casa, salpicado com cebola crispy e tomate Roma','burguerniaco.png','burgerniaco.jpg',0,1)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Cheddar Onion Burguer',30.99,'Hamburger artesanal(120g), cheddar, barbecue, tomate, cebola, cebola caramelizada, cebola crispy, onion rings e cheddar','Hambúrguer artesanal 120g, Queijo Cheddar, Pão Brioche, Barbecue, Tomate, Cebola, Cebola Caramelizada, Cebola Crispy, Onion Rings e Cheddar Cremoso da casa','cheddar.png','classico.jpg',0,1)");

            migrationBuilder.Sql("INSERT INTO Lanches(CategoriaId,LancheNome,Preco,DescricaoCurta,DescricaoDetalhada, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque)" +
                "VALUES(1,'Dublo Bacon',35.99,'Dois Hamburguer artesanal(120g), queijo, barbecue e bacon','Dois suculentos hambúrgueres artesanais de 120g com fatias de queijo prato, pão brioche, molho barbecue e bacon fatiado crocante','duplobacon.jpg','duplobacon.png',0,1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
