using Microsoft.EntityFrameworkCore.Migrations;

namespace APICatalogo.Migrations
{
    public partial class Populadb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome,ImagemUrl) Values('Bebidas','http://wwww.imagemcategorias.com.br/img/1')");   
            mb.Sql("Insert into Categorias(Nome,ImagemUrl) Values('Lanches','http://wwww.imagemcategorias.com.br/img/2')"); 

            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) Values('Coca-Cola 200ml','A base de cola',5.99,'http://www.imagemproduto.com.br/img/1',50,now(),(Select CategoriaId from Categorias where Nome='Bebidas'))");
            mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) Values('Sandwich','Beicom + salada',10.99,'http://www.imagemproduto.com.br/img/2',50,now(),(Select CategoriaId from Categorias where Nome='Lanches'))");


        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
            mb.Sql("Delete from Produtos");
        }
    }
}
