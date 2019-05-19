using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class GrupoProdutoMap : EntityTypeConfiguration<GrupoProduto>
    {
        public GrupoProdutoMap()
        {
            //Definindo o nome da tabela
            ToTable("grupo_produto");

            //Definindo PK
            HasKey(x => x.Id);
            //Definindo nome da PK e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Definindo nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
            //Definindo nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
        }

    }
}