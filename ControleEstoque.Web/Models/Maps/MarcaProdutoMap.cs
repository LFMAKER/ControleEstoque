using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class MarcaProdutoMap : EntityTypeConfiguration<MarcaProduto>
    {
        public MarcaProdutoMap()
        {
            //Definindo o nome da tabela
            ToTable("marca_produto");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo o nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
            //Definindo o nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        }
    }
}