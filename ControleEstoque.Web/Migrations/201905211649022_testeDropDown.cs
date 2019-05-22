namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testeDropDown : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.produto", "Fornecedor_Id", "dbo.fornecedor");
            DropForeignKey("dbo.produto", "GrupoProduto_Id", "dbo.grupo_produto");
            DropForeignKey("dbo.produto", "LocalArmazenamento_Id", "dbo.local_armazenamento");
            DropForeignKey("dbo.produto", "MarcaProduto_Id", "dbo.marca_produto");
            DropForeignKey("dbo.produto", "UnidadeMedida_Id", "dbo.unidade_medida");
            DropForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil");
            DropIndex("dbo.produto", new[] { "Fornecedor_Id" });
            DropIndex("dbo.produto", new[] { "GrupoProduto_Id" });
            DropIndex("dbo.produto", new[] { "LocalArmazenamento_Id" });
            DropIndex("dbo.produto", new[] { "MarcaProduto_Id" });
            DropIndex("dbo.produto", new[] { "UnidadeMedida_Id" });
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            AlterColumn("dbo.produto", "Fornecedor_Id", c => c.Int());
            AlterColumn("dbo.produto", "GrupoProduto_Id", c => c.Int());
            AlterColumn("dbo.produto", "LocalArmazenamento_Id", c => c.Int());
            AlterColumn("dbo.produto", "MarcaProduto_Id", c => c.Int());
            AlterColumn("dbo.produto", "UnidadeMedida_Id", c => c.Int());
            AlterColumn("dbo.usuario", "Perfil_Id", c => c.Int());
            CreateIndex("dbo.produto", "Fornecedor_Id");
            CreateIndex("dbo.produto", "GrupoProduto_Id");
            CreateIndex("dbo.produto", "LocalArmazenamento_Id");
            CreateIndex("dbo.produto", "MarcaProduto_Id");
            CreateIndex("dbo.produto", "UnidadeMedida_Id");
            CreateIndex("dbo.usuario", "Perfil_Id");
            AddForeignKey("dbo.produto", "Fornecedor_Id", "dbo.fornecedor", "id");
            AddForeignKey("dbo.produto", "GrupoProduto_Id", "dbo.grupo_produto", "id");
            AddForeignKey("dbo.produto", "LocalArmazenamento_Id", "dbo.local_armazenamento", "id");
            AddForeignKey("dbo.produto", "MarcaProduto_Id", "dbo.marca_produto", "id");
            AddForeignKey("dbo.produto", "UnidadeMedida_Id", "dbo.unidade_medida", "id");
            AddForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil");
            DropForeignKey("dbo.produto", "UnidadeMedida_Id", "dbo.unidade_medida");
            DropForeignKey("dbo.produto", "MarcaProduto_Id", "dbo.marca_produto");
            DropForeignKey("dbo.produto", "LocalArmazenamento_Id", "dbo.local_armazenamento");
            DropForeignKey("dbo.produto", "GrupoProduto_Id", "dbo.grupo_produto");
            DropForeignKey("dbo.produto", "Fornecedor_Id", "dbo.fornecedor");
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            DropIndex("dbo.produto", new[] { "UnidadeMedida_Id" });
            DropIndex("dbo.produto", new[] { "MarcaProduto_Id" });
            DropIndex("dbo.produto", new[] { "LocalArmazenamento_Id" });
            DropIndex("dbo.produto", new[] { "GrupoProduto_Id" });
            DropIndex("dbo.produto", new[] { "Fornecedor_Id" });
            AlterColumn("dbo.usuario", "Perfil_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "UnidadeMedida_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "MarcaProduto_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "LocalArmazenamento_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "GrupoProduto_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "Fornecedor_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.usuario", "Perfil_Id");
            CreateIndex("dbo.produto", "UnidadeMedida_Id");
            CreateIndex("dbo.produto", "MarcaProduto_Id");
            CreateIndex("dbo.produto", "LocalArmazenamento_Id");
            CreateIndex("dbo.produto", "GrupoProduto_Id");
            CreateIndex("dbo.produto", "Fornecedor_Id");
            AddForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "UnidadeMedida_Id", "dbo.unidade_medida", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "MarcaProduto_Id", "dbo.marca_produto", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "LocalArmazenamento_Id", "dbo.local_armazenamento", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "GrupoProduto_Id", "dbo.grupo_produto", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "Fornecedor_Id", "dbo.fornecedor", "id", cascadeDelete: true);
        }
    }
}
