namespace ControleEstoque.Web.Migrations
{
    using Dados;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ControleEstoque.Web.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ControleEstoque.Web.Models.Context context)
        {
            PopularBanco.Inserir(context);
        }
    }
}
