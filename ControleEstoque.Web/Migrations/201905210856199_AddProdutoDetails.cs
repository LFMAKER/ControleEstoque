namespace ControleEstoque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProdutoDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.produto", "id_fornecedor", "dbo.fornecedor");
            DropForeignKey("dbo.produto", "id_grupo", "dbo.grupo_produto");
            DropForeignKey("dbo.produto", "id_local_armazenamento", "dbo.local_armazenamento");
            DropForeignKey("dbo.produto", "id_marca", "dbo.marca_produto");
            DropForeignKey("dbo.produto", "id_unidade_medida", "dbo.unidade_medida");
            RenameColumn(table: "dbo.produto", name: "id_fornecedor", newName: "Fornecedor_Id");
            RenameColumn(table: "dbo.produto", name: "id_grupo", newName: "GrupoProduto_Id");
            RenameColumn(table: "dbo.produto", name: "id_local_armazenamento", newName: "LocalArmazenamento_Id");
            RenameColumn(table: "dbo.produto", name: "id_marca", newName: "MarcaProduto_Id");
            RenameColumn(table: "dbo.produto", name: "id_unidade_medida", newName: "UnidadeMedida_Id");
            RenameIndex(table: "dbo.produto", name: "IX_id_fornecedor", newName: "IX_Fornecedor_Id");
            RenameIndex(table: "dbo.produto", name: "IX_id_grupo", newName: "IX_GrupoProduto_Id");
            RenameIndex(table: "dbo.produto", name: "IX_id_local_armazenamento", newName: "IX_LocalArmazenamento_Id");
            RenameIndex(table: "dbo.produto", name: "IX_id_marca", newName: "IX_MarcaProduto_Id");
            RenameIndex(table: "dbo.produto", name: "IX_id_unidade_medida", newName: "IX_UnidadeMedida_Id");
            AddForeignKey("dbo.produto", "Fornecedor_Id", "dbo.fornecedor", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "GrupoProduto_Id", "dbo.grupo_produto", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "LocalArmazenamento_Id", "dbo.local_armazenamento", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "MarcaProduto_Id", "dbo.marca_produto", "id", cascadeDelete: true);
            AddForeignKey("dbo.produto", "UnidadeMedida_Id", "dbo.unidade_medida", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.produto", "UnidadeMedida_Id", "dbo.unidade_medida");
            DropForeignKey("dbo.produto", "MarcaProduto_Id", "dbo.marca_produto");
            DropForeignKey("dbo.produto", "LocalArmazenamento_Id", "dbo.local_armazenamento");
            DropForeignKey("dbo.produto", "GrupoProduto_Id", "dbo.grupo_produto");
            DropForeignKey("dbo.produto", "Fornecedor_Id", "dbo.fornecedor");
            RenameIndex(table: "dbo.produto", name: "IX_UnidadeMedida_Id", newName: "IX_id_unidade_medida");
            RenameIndex(table: "dbo.produto", name: "IX_MarcaProduto_Id", newName: "IX_id_marca");
            RenameIndex(table: "dbo.produto", name: "IX_LocalArmazenamento_Id", newName: "IX_id_local_armazenamento");
            RenameIndex(table: "dbo.produto", name: "IX_GrupoProduto_Id", newName: "IX_id_grupo");
            RenameIndex(table: "dbo.produto", name: "IX_Fornecedor_Id", newName: "IX_id_fornecedor");
            RenameColumn(table: "dbo.produto", name: "UnidadeMedida_Id", newName: "id_unidade_medida");
            RenameColumn(table: "dbo.produto", name: "MarcaProduto_Id", newName: "id_marca");
            RenameColumn(table: "dbo.produto", name: "LocalArmazenamento_Id", newName: "id_local_armazenamento");
            RenameColumn(table: "dbo.produto", name: "GrupoProduto_Id", newName: "id_grupo");
            RenameColumn(table: "dbo.produto", name: "Fornecedor_Id", newName: "id_fornecedor");
            AddForeignKey("dbo.produto", "id_unidade_medida", "dbo.unidade_medida", "id");
            AddForeignKey("dbo.produto", "id_marca", "dbo.marca_produto", "id");
            AddForeignKey("dbo.produto", "id_local_armazenamento", "dbo.local_armazenamento", "id");
            AddForeignKey("dbo.produto", "id_grupo", "dbo.grupo_produto", "id");
            AddForeignKey("dbo.produto", "id_fornecedor", "dbo.fornecedor", "id");
        }
    }
}
