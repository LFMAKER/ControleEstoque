
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Dal.Relatorio
{
    public class EntradaESaidaRelatorioDao
    {

        public static List<EntradaGraficos> GetEntradasRelatorioFiltro(DateTime dataInicio, DateTime dataFim)
        {
            List<EntradaGraficos> entradas = new List<EntradaGraficos>();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_custo*quant)) as total " +
                "from entrada_produto " +
                 "inner join produto on produto.id = entrada_produto.id_produto " +
                 "WHERE(YEAR(data) BETWEEN " + dataInicio.Year + " and " + dataFim.Year + ") and" +
                 "(MONTH(data) BETWEEN " + dataInicio.Month + " and " + dataFim.Month + ")" +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                entradas = ctx.Database.Connection.Query<EntradaGraficos>(sql).ToList();
                entradas = entradas.OrderBy(x => x.Data).ToList();

            }
            return entradas;
        }


        public static List<SaidaGraficos> GetSaidasRelatorioFiltro(DateTime dataInicio, DateTime dataFim)
        {
            List<SaidaGraficos> saidas = new List<SaidaGraficos>();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_custo*quant)) as total " +
                "from saida_produto " +
                 "inner join produto on produto.id = saida_produto.id_produto " +
                 "WHERE(YEAR(data) BETWEEN " + dataInicio.Year + " and " + dataFim.Year + ") and" +
                 "(MONTH(data) BETWEEN " + dataInicio.Month + " and " + dataFim.Month + ")" +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                saidas = ctx.Database.Connection.Query<SaidaGraficos>(sql).ToList();
                saidas = saidas.OrderBy(x => x.Data).ToList();

            }
            return saidas;
        }



    }
}