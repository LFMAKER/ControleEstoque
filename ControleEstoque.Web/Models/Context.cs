using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    //Classe de contexto do banco de dados
    public class Context : DbContext
    {

        //String de conexao com o BD
        public Context() : base("name=principal")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        //Mapeamento dos Models
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Chamandos os MAPS
            modelBuilder.Configurations.Add(new CidadeMap());
            modelBuilder.Configurations.Add(new EntradaProdutoMap());
            modelBuilder.Configurations.Add(new EstadoMap());
            modelBuilder.Configurations.Add(new FornecedorMap());
            modelBuilder.Configurations.Add(new GrupoProdutoMap());
            modelBuilder.Configurations.Add(new LocalArmazenamentoMap());
            modelBuilder.Configurations.Add(new MarcaProdutoMap());
            modelBuilder.Configurations.Add(new PaisMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new SaidaProdutoMap());
            modelBuilder.Configurations.Add(new UnidadeMedidaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
        }

        //Definindo o que o DB deve utilizar
        public DbSet<CidadeModel> Cidades { get; set; }
        public DbSet<EntradaProdutoModel> EntradasProdutos { get; set; }
        public DbSet<EstadoModel> Estados { get; set; }
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<GrupoProdutoModel> GruposProdutos { get; set; }
        public DbSet<LocalArmazenamentoModel> LocaisArmazenamentos { get; set; }
        public DbSet<MarcaProdutoModel> MarcasProdutos { get; set; }
        public DbSet<PaisModel> Paises { get; set; }
        public DbSet<PerfilModel> PerfisUsuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<SaidaProdutoModel> SaidasProdutos { get; set; }
        public DbSet<UnidadeMedidaModel> UnidadesMedida { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }



    }
}