namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil");
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            DropColumn("dbo.usuario", "Perfil_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.usuario", "Perfil_Id", c => c.Int());
            CreateIndex("dbo.usuario", "Perfil_Id");
            AddForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil", "id");
        }
    }
}
