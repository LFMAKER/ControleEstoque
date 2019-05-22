namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocalArmazenamentoCapacidadeTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.local_armazenamento", "CapacidadeTotal", c => c.Int(nullable: false));
            AddColumn("dbo.local_armazenamento", "CapacidadeAtual", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.local_armazenamento", "CapacidadeAtual");
            DropColumn("dbo.local_armazenamento", "CapacidadeTotal");
        }
    }
}
