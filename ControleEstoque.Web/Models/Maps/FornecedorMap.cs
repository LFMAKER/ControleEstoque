using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ControleEstoque.Web.Models
{
    public class FornecedorMap : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMap()
        {
            //Definindo Nome da Tabeça
            ToTable("fornecedor");

            HasKey(x => x.Id);
            //Definindo nome pk e auto increment
            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.RazaoSocial).HasColumnName("razao_social").HasMaxLength(200).IsOptional();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.NumDocumento).HasColumnName("num_documento").HasMaxLength(20).IsOptional();
            //Definindo Nome e Obrigatório
            Property(x => x.Tipo).HasColumnName("tipo").IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(20).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Contato).HasColumnName("contato").HasMaxLength(60).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Logradouro).HasColumnName("logradouro").HasMaxLength(200).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Numero).HasColumnName("numero").HasMaxLength(20).IsRequired();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(100).IsOptional();
            //Definindo Nome, tamanho e Opcional
            Property(x => x.Cep).HasColumnName("cep").HasMaxLength(10).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(50).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(50).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Estado).HasColumnName("estado").HasMaxLength(50).IsRequired();
            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Pais).HasColumnName("pais").HasMaxLength(50).IsRequired();




            //Definindo Nome, tamanho e Obrigatório
            Property(x => x.Ativo).HasColumnName("ativo").IsRequired();

      
        }
    }
}