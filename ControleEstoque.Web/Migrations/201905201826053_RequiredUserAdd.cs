namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredUserAdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil");
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            AlterColumn("dbo.usuario", "Perfil_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.usuario", "Perfil_Id");
            AddForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil");
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            AlterColumn("dbo.usuario", "Perfil_Id", c => c.Int());
            CreateIndex("dbo.usuario", "Perfil_Id");
            AddForeignKey("dbo.usuario", "Perfil_Id", "dbo.perfil", "id");
        }
    }
}
