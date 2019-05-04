using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Dal.Grafico
{
    public class EntradaESaidaGraficoDao
    {



        public static List<EntradaGraficos> GetEntradasGrafico()
        {
            List<EntradaGraficos> entradas = new List<EntradaGraficos>();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_custo*quant)) as total " +
                "from entrada_produto " +
                 "inner join produto on produto.id = entrada_produto.id_produto " +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                entradas = ctx.Database.Connection.Query<EntradaGraficos>(sql).ToList();

            }




            return entradas;
        }

        public static List<SaidaGraficos> GetSaidasGrafico()
        {
            List<SaidaGraficos> saidas = new List<SaidaGraficos>();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_venda*quant)) as total " +
                "from saida_produto " +
                 "inner join produto on produto.id = saida_produto.id_produto " +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                saidas = ctx.Database.Connection.Query<SaidaGraficos>(sql).ToList();
            }


            return saidas;
        }

    }
}