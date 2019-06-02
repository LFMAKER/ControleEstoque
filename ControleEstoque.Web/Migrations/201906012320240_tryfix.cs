namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryfix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.usuario", new[] { "Perfil_Id" });
            RenameColumn(table: "dbo.usuario", name: "Perfil_Id", newName: "id_perfil");
            AlterColumn("dbo.usuario", "id_perfil", c => c.Int(nullable: false));
            CreateIndex("dbo.usuario", "id_perfil");
        }
        
        public override void Down()
        {
            DropIndex("dbo.usuario", new[] { "id_perfil" });
            AlterColumn("dbo.usuario", "id_perfil", c => c.Int());
            RenameColumn(table: "dbo.usuario", name: "id_perfil", newName: "Perfil_Id");
            CreateIndex("dbo.usuario", "Perfil_Id");
        }
    }
}
