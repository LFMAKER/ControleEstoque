namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryfixProduto2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.produto", new[] { "Fornecedor_Id" });
            DropIndex("dbo.produto", new[] { "GrupoProduto_Id" });
            DropIndex("dbo.produto", new[] { "LocalArmazenamento_Id" });
            DropIndex("dbo.produto", new[] { "MarcaProduto_Id" });
            DropIndex("dbo.produto", new[] { "UnidadeMedida_Id" });
            DropColumn("dbo.produto", "IdFornecedor");
            DropColumn("dbo.produto", "IdGrupo");
            DropColumn("dbo.produto", "IdLocalArmazenamento");
            DropColumn("dbo.produto", "IdMarca");
            DropColumn("dbo.produto", "IdUnidadeMedida");
            RenameColumn(table: "dbo.produto", name: "Fornecedor_Id", newName: "IdFornecedor");
            RenameColumn(table: "dbo.produto", name: "GrupoProduto_Id", newName: "IdGrupo");
            RenameColumn(table: "dbo.produto", name: "LocalArmazenamento_Id", newName: "IdLocalArmazenamento");
            RenameColumn(table: "dbo.produto", name: "MarcaProduto_Id", newName: "IdMarca");
            RenameColumn(table: "dbo.produto", name: "UnidadeMedida_Id", newName: "IdUnidadeMedida");
            AlterColumn("dbo.produto", "IdFornecedor", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "IdGrupo", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "IdLocalArmazenamento", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "IdMarca", c => c.Int(nullable: false));
            AlterColumn("dbo.produto", "IdUnidadeMedida", c => c.Int(nullable: false));
            CreateIndex("dbo.produto", "IdUnidadeMedida");
            CreateIndex("dbo.produto", "IdGrupo");
            CreateIndex("dbo.produto", "IdMarca");
            CreateIndex("dbo.produto", "IdFornecedor");
            CreateIndex("dbo.produto", "IdLocalArmazenamento");
        }
        
        public override void Down()
        {
            DropIndex("dbo.produto", new[] { "IdLocalArmazenamento" });
            DropIndex("dbo.produto", new[] { "IdFornecedor" });
            DropIndex("dbo.produto", new[] { "IdMarca" });
            DropIndex("dbo.produto", new[] { "IdGrupo" });
            DropIndex("dbo.produto", new[] { "IdUnidadeMedida" });
            AlterColumn("dbo.produto", "IdUnidadeMedida", c => c.Int());
            AlterColumn("dbo.produto", "IdMarca", c => c.Int());
            AlterColumn("dbo.produto", "IdLocalArmazenamento", c => c.Int());
            AlterColumn("dbo.produto", "IdGrupo", c => c.Int());
            AlterColumn("dbo.produto", "IdFornecedor", c => c.Int());
            RenameColumn(table: "dbo.produto", name: "IdUnidadeMedida", newName: "UnidadeMedida_Id");
            RenameColumn(table: "dbo.produto", name: "IdMarca", newName: "MarcaProduto_Id");
            RenameColumn(table: "dbo.produto", name: "IdLocalArmazenamento", newName: "LocalArmazenamento_Id");
            RenameColumn(table: "dbo.produto", name: "IdGrupo", newName: "GrupoProduto_Id");
            RenameColumn(table: "dbo.produto", name: "IdFornecedor", newName: "Fornecedor_Id");
            AddColumn("dbo.produto", "IdUnidadeMedida", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdMarca", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdLocalArmazenamento", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdGrupo", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdFornecedor", c => c.Int(nullable: false));
            CreateIndex("dbo.produto", "UnidadeMedida_Id");
            CreateIndex("dbo.produto", "MarcaProduto_Id");
            CreateIndex("dbo.produto", "LocalArmazenamento_Id");
            CreateIndex("dbo.produto", "GrupoProduto_Id");
            CreateIndex("dbo.produto", "Fornecedor_Id");
        }
    }
}
