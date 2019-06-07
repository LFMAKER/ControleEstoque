namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestePreco2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.produto", "preco_custo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.produto", "preco_venda", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.produto", "preco_venda", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.produto", "preco_custo", c => c.Decimal(nullable: false, precision: 10, scale: 2));
        }
    }
}
