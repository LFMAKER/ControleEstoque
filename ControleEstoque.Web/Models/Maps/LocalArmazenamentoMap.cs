using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models.Maps
{
    public class LocalArmazenamentoMap : EntityTypeConfiguration<LocalArmazenamento>
    {
        public LocalArmazenamentoMap()
        {

            //Definindo nome da tabela
            ToTable("local_armazenamento");
            
            //Definindo PK
            HasKey(x => x.Id);

            //Definindo nome da tabela e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Definindo nome da tabela, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
            //Definindo nome da tabela e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();

        }

    }
}