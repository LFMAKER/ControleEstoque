using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Domain;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ControleEstoque.Web.Dal.Grafico
{
    public class EntradaESaidaGraficoDao
    {



        public static async Task<List<EntradaGraficos>> GetEntradasGrafico()
        {
            List<EntradaGraficos> entradas = new List<EntradaGraficos>();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_custo*quant)) as total " +
                "from entrada_produto " +
                 "inner join produto on produto.id = entrada_produto.id_produto " +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                entradas = (List<EntradaGraficos>)await ctx.Database.Connection.QueryAsync<EntradaGraficos>(sql);

            }




            return entradas;
        }


        public static async Task<EntradaGraficos> GetEntradaGastoMesAtual(DateTime dataAtual)
        {
            EntradaGraficos entradas = new EntradaGraficos();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_custo*quant)) as total " +
                "from entrada_produto " +
                 "inner join produto on produto.id = entrada_produto.id_produto " +
                 "WHERE MONTH(data) =" + dataAtual.Month + "and YEAR(data) =" + dataAtual.Year +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                var entradasBanco = await ctx.Database.Connection.QueryAsync<EntradaGraficos>(sql);
                entradas = entradasBanco.FirstOrDefault();

            }




            return entradas;
        }





        public static async Task<List<SaidaGraficos>> GetSaidasGrafico()
        {
            List<SaidaGraficos> saidas = new List<SaidaGraficos>();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_venda*quant)) as total " +
                "from saida_produto " +
                 "inner join produto on produto.id = saida_produto.id_produto " +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                saidas = (List<SaidaGraficos>)await ctx.Database.Connection.QueryAsync<SaidaGraficos>(sql);
            }


            return saidas;
        }


        public static async Task<SaidaGraficos> GetSaidaGanhoMesAtual(DateTime dataAtual)
        {
            SaidaGraficos saidas = new SaidaGraficos();


            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT (data, 'MM/yyyy') as data, SUM((preco_custo*quant)) as total " +
                "from saida_produto " +
                 "inner join produto on produto.id = saida_produto.id_produto " +
                 "WHERE MONTH(data) =" + dataAtual.Month + "and YEAR(data) =" + dataAtual.Year +
                  "GROUP BY FORMAT(data, 'MM/yyyy')";

                var saidaBanco = await ctx.Database.Connection.QueryAsync<SaidaGraficos>(sql);
                saidas = saidaBanco.FirstOrDefault();

            }




            return saidas;
        }


        public static async Task<Decimal> GetEntradaGastoAnoAtual(DateTime dataAtual)
        {


            string data = dataAtual.Year.ToString();
            decimal total = 0;
            using (var ctx = new Context())
            {

                //entradas = ctx.EntradasProdutos.Include("Produto").ToList();
                var sql = "SELECT FORMAT(data, 'MM/yyyy') as data, SUM((preco_custo * quant)) as total " +
               "from entrada_produto " +
                "inner join produto on produto.id = entrada_produto.id_produto " +
                "WHERE YEAR(data) =" + data +
                 " GROUP BY FORMAT(data, 'MM/yyyy') ";

                var entradasBanco = await ctx.Database.Connection.QueryAsync<EntradaGraficos>(sql);
                total = entradasBanco.Sum(x => Convert.ToDecimal(x.total));

            }




            return total;
        }


        public static async Task<Decimal> GetSaidaGanhoAnoAtual(DateTime dataAtual)
        {


            string data = dataAtual.Year.ToString();
            decimal total = 0;

            using (var ctx = new Context())
            {


                var sql = "SELECT FORMAT(data, 'MM/yyyy') as data, SUM((preco_custo * quant)) as total " +
                "from saida_produto " +
                 "inner join produto on produto.id = saida_produto.id_produto " +
                 "WHERE YEAR(data) =" + data +
                  " GROUP BY FORMAT(data, 'MM/yyyy')";



                var saidaBanco = await ctx.Database.Connection.QueryAsync<SaidaGraficos>(sql);
                total = saidaBanco.Sum(x => Convert.ToDecimal(x.total));



            }




            return total;
        }

    }
}