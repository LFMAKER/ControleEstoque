using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class UnidadeMedidaMap : EntityTypeConfiguration<UnidadeMedida>
    {
        public UnidadeMedidaMap()
        {
            //Definindo nome da tabela
            ToTable("unidade_medida");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(30).IsRequired();
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Sigla).HasColumnName("sigla").HasMaxLength(3).IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        }
    }
}