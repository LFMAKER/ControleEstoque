namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryfixProduto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.produto", "IdUnidadeMedida", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdGrupo", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdMarca", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdFornecedor", c => c.Int(nullable: false));
            AddColumn("dbo.produto", "IdLocalArmazenamento", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.produto", "IdLocalArmazenamento");
            DropColumn("dbo.produto", "IdFornecedor");
            DropColumn("dbo.produto", "IdMarca");
            DropColumn("dbo.produto", "IdGrupo");
            DropColumn("dbo.produto", "IdUnidadeMedida");
        }
    }
}
