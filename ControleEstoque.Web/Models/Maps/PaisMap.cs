using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class PaisMap : EntityTypeConfiguration<PaisModel>
    {
        public PaisMap()
        {
            //Definindo o nome da tabela
            ToTable("pais");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo o nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo o nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(60).IsRequired();
            //Definindo o nome da coluna, tamanho e obrigatório
            Property(x => x.NomePt).HasColumnName("nome_pt").HasMaxLength(60).IsRequired();
            //Definindo o nome da coluna, tamanho e obrigatório
            Property(x => x.Sigla).HasColumnName("sigla").HasMaxLength(2).IsRequired();
            //Definindo o nome da coluna, tamanho e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();


        }
    }
}