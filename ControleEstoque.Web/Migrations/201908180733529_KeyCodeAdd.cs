namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyCodeAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KeyControles", "Ativada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KeyControles", "Ativada");
        }
    }
}
