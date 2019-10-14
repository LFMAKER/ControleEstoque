namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.entrada_produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        numero = c.String(nullable: false, maxLength: 10),
                        data = c.DateTime(nullable: false),
                        quant = c.Int(nullable: false),
                        id_produto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.produto", t => t.id_produto)
                .Index(t => t.id_produto);
            
            CreateTable(
                "dbo.produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        codigo = c.String(nullable: false, maxLength: 10),
                        nome = c.String(nullable: false, maxLength: 200),
                        preco_custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        preco_venda = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quant_estoque = c.Int(nullable: false),
                        IdUnidadeMedida = c.Int(nullable: false),
                        IdGrupo = c.Int(nullable: false),
                        IdMarca = c.Int(nullable: false),
                        IdFornecedor = c.Int(nullable: false),
                        IdLocalArmazenamento = c.Int(nullable: false),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.fornecedor", t => t.IdFornecedor)
                .ForeignKey("dbo.grupo_produto", t => t.IdGrupo)
                .ForeignKey("dbo.local_armazenamento", t => t.IdLocalArmazenamento)
                .ForeignKey("dbo.marca_produto", t => t.IdMarca)
                .ForeignKey("dbo.unidade_medida", t => t.IdUnidadeMedida)
                .Index(t => t.IdUnidadeMedida)
                .Index(t => t.IdGrupo)
                .Index(t => t.IdMarca)
                .Index(t => t.IdFornecedor)
                .Index(t => t.IdLocalArmazenamento);
            
            CreateTable(
                "dbo.fornecedor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200),
                        razao_social = c.String(maxLength: 200),
                        num_documento = c.String(maxLength: 20),
                        tipo = c.Int(nullable: false),
                        telefone = c.String(nullable: false, maxLength: 20),
                        contato = c.String(nullable: false, maxLength: 60),
                        logradouro = c.String(nullable: false, maxLength: 200),
                        numero = c.String(nullable: false, maxLength: 20),
                        complemento = c.String(maxLength: 100),
                        cep = c.String(nullable: false, maxLength: 10),
                        bairro = c.String(nullable: false, maxLength: 50),
                        cidade = c.String(nullable: false, maxLength: 50),
                        estado = c.String(nullable: false, maxLength: 50),
                        pais = c.String(nullable: false, maxLength: 50),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.grupo_produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.local_armazenamento",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200),
                        CapacidadeTotal = c.Int(nullable: false),
                        CapacidadeAtual = c.Int(nullable: false),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.marca_produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.unidade_medida",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200),
                        sigla = c.String(nullable: false, maxLength: 3),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.KeyControles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        CriadaEm = c.DateTime(nullable: false),
                        UltimoUso = c.DateTime(nullable: false),
                        QuantidadeDeChamadas = c.Int(nullable: false),
                        Ativada = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.perfil",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 200),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.saida_produto",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        numero = c.String(nullable: false, maxLength: 10),
                        data = c.DateTime(nullable: false),
                        quant = c.Int(nullable: false),
                        id_produto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.produto", t => t.id_produto)
                .Index(t => t.id_produto);
            
            CreateTable(
                "dbo.usuario",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 50),
                        senha = c.String(nullable: false, maxLength: 50),
                        nome = c.String(nullable: false, maxLength: 200),
                        email = c.String(nullable: false, maxLength: 200),
                        id_perfil = c.Int(nullable: false),
                        KeyC_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.KeyControles", t => t.KeyC_Id)
                .ForeignKey("dbo.perfil", t => t.id_perfil)
                .Index(t => t.id_perfil)
                .Index(t => t.KeyC_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.usuario", "id_perfil", "dbo.perfil");
            DropForeignKey("dbo.usuario", "KeyC_Id", "dbo.KeyControles");
            DropForeignKey("dbo.saida_produto", "id_produto", "dbo.produto");
            DropForeignKey("dbo.entrada_produto", "id_produto", "dbo.produto");
            DropForeignKey("dbo.produto", "IdUnidadeMedida", "dbo.unidade_medida");
            DropForeignKey("dbo.produto", "IdMarca", "dbo.marca_produto");
            DropForeignKey("dbo.produto", "IdLocalArmazenamento", "dbo.local_armazenamento");
            DropForeignKey("dbo.produto", "IdGrupo", "dbo.grupo_produto");
            DropForeignKey("dbo.produto", "IdFornecedor", "dbo.fornecedor");
            DropIndex("dbo.usuario", new[] { "KeyC_Id" });
            DropIndex("dbo.usuario", new[] { "id_perfil" });
            DropIndex("dbo.saida_produto", new[] { "id_produto" });
            DropIndex("dbo.produto", new[] { "IdLocalArmazenamento" });
            DropIndex("dbo.produto", new[] { "IdFornecedor" });
            DropIndex("dbo.produto", new[] { "IdMarca" });
            DropIndex("dbo.produto", new[] { "IdGrupo" });
            DropIndex("dbo.produto", new[] { "IdUnidadeMedida" });
            DropIndex("dbo.entrada_produto", new[] { "id_produto" });
            DropTable("dbo.usuario");
            DropTable("dbo.saida_produto");
            DropTable("dbo.perfil");
            DropTable("dbo.KeyControles");
            DropTable("dbo.unidade_medida");
            DropTable("dbo.marca_produto");
            DropTable("dbo.local_armazenamento");
            DropTable("dbo.grupo_produto");
            DropTable("dbo.fornecedor");
            DropTable("dbo.produto");
            DropTable("dbo.entrada_produto");
        }
    }
}
