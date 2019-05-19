using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //Definindo nome da tabela
            ToTable("usuario");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Login).HasColumnName("login").HasMaxLength(50).IsRequired();
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Senha).HasColumnName("senha").HasMaxLength(32).IsRequired();
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();



        }
    }
}