namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.usuario", new[] { "id_perfil" });
            RenameColumn(table: "dbo.usuario", name: "id_perfil", newName: "Perfil_Id");
            AlterColumn("dbo.usuario", "Perfil_Id", c => c.Int());
            CreateIndex("dbo.usuario", "Perfil_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            AlterColumn("dbo.usuario", "Perfil_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.usuario", name: "Perfil_Id", newName: "id_perfil");
            CreateIndex("dbo.usuario", "id_perfil");
        }
    }
}
