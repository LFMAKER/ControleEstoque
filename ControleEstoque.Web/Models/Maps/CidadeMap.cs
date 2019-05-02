using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Web.Models
{
    public class CidadeMap : EntityTypeConfiguration<CidadeModel> 
    {
        /*EntityTypeConfiguration<CidadeModel>: Classe base de todos os
         * mapeamentos EF 
        */
        public CidadeMap()
        {
            //Nome da Tabela
            ToTable("cidade");
            //Chave Primária
            HasKey(x => x.Id);
            //Define nome da coluna e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Define nome da coluna, tamanho e obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(30).IsRequired();
            //Define nome da coluna e obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
            //Define o id do estado e obrigatório
            Property(x => x.IdEstado).HasColumnName("id_estado").IsRequired();
            //Associação da entidade cidade model com estado FK
            HasRequired(x => x.Estado).WithMany().HasForeignKey(x => x.IdEstado).WillCascadeOnDelete(false);
            


        }
    }
}