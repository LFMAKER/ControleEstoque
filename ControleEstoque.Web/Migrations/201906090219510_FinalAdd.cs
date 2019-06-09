namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalAdd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.produto", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.fornecedor", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.fornecedor", "razao_social", c => c.String(maxLength: 200));
            AlterColumn("dbo.fornecedor", "logradouro", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.grupo_produto", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.local_armazenamento", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.marca_produto", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.unidade_medida", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.perfil", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.usuario", "senha", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.usuario", "nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.usuario", "email", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.usuario", "email", c => c.String(nullable: false));
            AlterColumn("dbo.usuario", "nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.usuario", "senha", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.perfil", "nome", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.unidade_medida", "nome", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.marca_produto", "nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.local_armazenamento", "nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.grupo_produto", "nome", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.fornecedor", "logradouro", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.fornecedor", "razao_social", c => c.String(maxLength: 100));
            AlterColumn("dbo.fornecedor", "nome", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.produto", "nome", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
