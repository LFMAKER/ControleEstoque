using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class FornecedorMap : EntityTypeConfiguration<FornecedorModel>
    {
        public FornecedorMap()
        {
            //Definindo Nome da Tabeça
            ToTable("fornecedor");

            HasKey(x => x.Id);
            //Definindo nome pk e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(60).IsRequired();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.RazaoSocial).HasColumnName("razao_social").HasMaxLength(100).IsOptional();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.NumDocumento).HasColumnName("num_documento").HasMaxLength(20).IsOptional();
            //Definindo Nome e Obrigatório
            Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(20).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Contato).HasColumnName("contato").HasMaxLength(60).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(100).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(20).IsRequired();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(100).IsOptional();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.Cep).HasColumnName("cep").HasMaxLength(10).IsOptional();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();

            //Definindo PK Pais
            Property(x => x.IdPais).HasColumnName("id_pais").IsRequired();
            HasRequired(x => x.Pais).WithMany().HasForeignKey(x => x.IdPais).WillCascadeOnDelete(false);

            //Definindo PK Estado
            Property(x => x.IdEstado).HasColumnName("id_estado").IsRequired();
            HasRequired(x => x.Estado).WithMany().HasForeignKey(x => x.IdPais).WillCascadeOnDelete(false);

            //Definindo PK Cidade
            Property(x => x.IdCidade).HasColumnName("id_cidade").IsRequired();
            HasRequired(x => x.Cidade).WithMany().HasForeignKey(x => x.IdPais).WillCascadeOnDelete(false);
        }
    }
}