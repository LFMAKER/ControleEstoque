namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.usuario", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.usuario", "Email");
        }
    }
}
