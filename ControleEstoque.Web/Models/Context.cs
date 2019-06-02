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
        public Context() : base("InventoryAnalytics")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        //Mapeamento dos Models
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Chamandos os MAPS
            modelBuilder.Configurations.Add(new EntradaProdutoMap());
            modelBuilder.Configurations.Add(new FornecedorMap());
            modelBuilder.Configurations.Add(new GrupoProdutoMap());
            modelBuilder.Configurations.Add(new LocalArmazenamentoMap());
            modelBuilder.Configurations.Add(new MarcaProdutoMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new SaidaProdutoMap());
            modelBuilder.Configurations.Add(new UnidadeMedidaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());



        }

        //Definindo o que o DB deve utilizar
        public DbSet<EntradaProduto> EntradasProdutos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<GrupoProduto> GruposProdutos { get; set; }
        public DbSet<LocalArmazenamento> LocaisArmazenamentos { get; set; }
        public DbSet<MarcaProduto> MarcasProdutos { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<SaidaProduto> SaidasProdutos { get; set; }
        public DbSet<UnidadeMedida> UnidadesMedida { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }



    }
}