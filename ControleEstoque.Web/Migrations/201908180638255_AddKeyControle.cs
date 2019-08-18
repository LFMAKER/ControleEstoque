namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeyControle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyControles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        CriadaEm = c.DateTime(nullable: false),
                        UltimoUso = c.DateTime(nullable: false),
                        QuantidadeDeChamadas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.usuario", "Key_Id", c => c.Int());
            CreateIndex("dbo.usuario", "Key_Id");
            AddForeignKey("dbo.usuario", "Key_Id", "dbo.KeyControles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.usuario", "Key_Id", "dbo.KeyControles");
            DropIndex("dbo.usuario", new[] { "Key_Id" });
            DropColumn("dbo.usuario", "Key_Id");
            DropTable("dbo.KeyControles");
        }
    }
}
