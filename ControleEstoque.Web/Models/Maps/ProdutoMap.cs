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
            Property(x => x.PrecoCusto).HasColumnName("preco_custo").HasPrecision(7, 2).IsRequired();
            //Definindo nome da coluna, precisão e obrigatório
            Property(x => x.PrecoVenda).HasColumnName("preco_venda").HasPrecision(7, 2).IsRequired();
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.QuantEstoque).HasColumnName("quant_estoque").IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
            
        }
    }
}