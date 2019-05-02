using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Web.Models
{
    public class EntradaProdutoMap : EntityTypeConfiguration<EntradaProdutoModel>
    {

        public EntradaProdutoMap()
        {
            //Nome da tabela
            ToTable("entrada_produto");
            //Id Auto Increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Defininando nome da coluna, tamanho e obrigatório
            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10).IsRequired();
            //Defininando nome da coluna e obrigatório
            Property(x => x.Data).HasColumnName("data").IsRequired();
            //Defininando nome da coluna e obrigatório
            Property(x => x.Quantidade).HasColumnName("quant").IsRequired();
            //Defininando nome da coluna e obrigatório
            Property(x => x.IdProduto).HasColumnName("id_produto").IsRequired();
            //Definindo a FK
            HasRequired(x => x.Produto).WithMany().HasForeignKey(x => x.IdProduto).WillCascadeOnDelete(false);

        }

    }
}