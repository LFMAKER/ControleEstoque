using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class EstadoMap : EntityTypeConfiguration<EstadoModel>
    {
         public EstadoMap()
        {
            //Nome da tabela
            ToTable("estado");
            HasKey(x => x.Id);
            //Id Auto Increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Defininando nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(30).IsRequired();
            //Defininando nome da coluna, tamanho e obrigatório
            Property(x => x.UF).HasColumnName("uf").HasMaxLength(2).IsRequired();
            //Defininando nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
            //Defininando nome da coluna e obrigatório
            Property(x => x.IdPais).HasColumnName("id_pais").IsRequired();
            //Definindo a FK
            HasRequired(x => x.Pais).WithMany().HasForeignKey(x => x.IdPais).WillCascadeOnDelete(false);



        }

    }
}