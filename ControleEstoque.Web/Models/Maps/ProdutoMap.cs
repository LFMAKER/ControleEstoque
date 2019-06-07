using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            //Definindo o nome da tabela
            ToTable("produto");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Codigo).HasColumnName("codigo").HasMaxLength(10).IsRequired();
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
            //Definindo nome da coluna, precisão e obrigatório
            Property(x => x.PrecoCusto).HasColumnName("preco_custo").IsRequired();
            //Definindo nome da coluna, precisão e obrigatório
            Property(x => x.PrecoVenda).HasColumnName("preco_venda").IsRequired();
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.QuantEstoque).HasColumnName("quant_estoque").IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();



            //Definindo a FK
            HasRequired(x => x.Fornecedor).WithMany().HasForeignKey(x => x.IdFornecedor).WillCascadeOnDelete(false);
            HasRequired(x => x.GrupoProduto).WithMany().HasForeignKey(x => x.IdGrupo).WillCascadeOnDelete(false);
            HasRequired(x => x.LocalArmazenamento).WithMany().HasForeignKey(x => x.IdLocalArmazenamento).WillCascadeOnDelete(false);
            HasRequired(x => x.UnidadeMedida).WithMany().HasForeignKey(x => x.IdUnidadeMedida).WillCascadeOnDelete(false);
            HasRequired(x => x.MarcaProduto).WithMany().HasForeignKey(x => x.IdMarca).WillCascadeOnDelete(false);



        }
    }
}