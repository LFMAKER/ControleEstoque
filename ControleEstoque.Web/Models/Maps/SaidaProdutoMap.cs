using ControleEstoque.Web.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class SaidaProdutoMap : EntityTypeConfiguration<SaidaProdutoModel>
    {
        public SaidaProdutoMap()
        {
            //Definindo nome da tabela
            ToTable("saida_produto");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.Data).HasColumnName("data").IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.Quantidade).HasColumnName("quant").IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.IdProduto).HasColumnName("id_produto").IsRequired();
            //Definindo FK Produto
            HasRequired(x => x.Produto).WithMany().HasForeignKey(x => x.IdProduto).WillCascadeOnDelete(false);
        }
    }
}