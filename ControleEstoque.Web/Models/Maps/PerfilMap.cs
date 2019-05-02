using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class PerfilMap : EntityTypeConfiguration<PerfilModel>
    {
        public PerfilMap()
        {
            //Definindo o nome da tabela
            ToTable("perfil");
            //Definindo PK
            HasKey(x => x.Id);
            //Definindo o nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo o nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(20).IsRequired();
            //Definindo o nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();

            //Mapeando a tabela que surgirá com o relacionando N:N entre Perfil e Usuario
            HasMany(x => x.Usuarios).WithMany(x => x.Perfis)
                .Map(x =>
                {
                    //Definindo o nome da tabela N:N
                    x.ToTable("perfil_usuario");
                    x.MapLeftKey("id_perfil");
                    x.MapRightKey("id_usuario");
                });
        }
    }
}