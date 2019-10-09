namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAttributeKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.usuario", name: "Key_Id", newName: "KeyC_Id");
            RenameIndex(table: "dbo.usuario", name: "IX_Key_Id", newName: "IX_KeyC_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.usuario", name: "IX_KeyC_Id", newName: "IX_Key_Id");
            RenameColumn(table: "dbo.usuario", name: "KeyC_Id", newName: "Key_Id");
        }
    }
}
