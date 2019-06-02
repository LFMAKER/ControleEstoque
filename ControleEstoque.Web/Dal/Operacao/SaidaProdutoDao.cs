using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dal.Operacao
{
    public class SaidaProdutoDao
    {
        private static Context ctx = SingletonContext.GetInstance();


        public static List<SaidaProduto> RecuperarListaSaidaProdutos(int pagina = 0, int tamPagina = 0, string dataInicio = "", string dataFim = "")
        {
            var pos = (pagina - 1) * tamPagina;
            if (tamPagina != 0 && pagina != 0 && dataInicio == "" && dataFim == "" && (dataInicio != null && dataFim != null))
            {

                return ctx.SaidasProdutos.AsNoTracking().OrderByDescending(x => x.Data).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();

            }
            else if (tamPagina != 0 && pagina != 0 && ((dataInicio != "" && dataFim != "") && (dataInicio != null && dataFim != null)))
            {
                var dataInicioConvertida = Convert.ToDateTime(dataInicio);
                var dataFimConvertida = Convert.ToDateTime(dataFim);


                return ctx.SaidasProdutos.OrderByDescending(x => x.Data).Where(x => x.Data >= dataInicioConvertida && x.Data <= dataFimConvertida).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();
            }
            else if (tamPagina != 0 && pagina != 0 && ((dataInicio == "" && dataFim == "") || (dataInicio == null && dataFim == null)))
            {
                return ctx.SaidasProdutos.AsNoTracking().OrderByDescending(x => x.Data).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();
            }
            else
            {
                return ctx.SaidasProdutos.OrderByDescending(x => x.Data).Include("Produto").ToList();
            }

        }


        public static int RecuperarQuantidadeSaidaProdutos()
        {
            return ctx.SaidasProdutos.Count();
        }

        public static List<SaidaProduto> RecuperarSaidaPorNumero(string numero)
        {
            return ctx.SaidasProdutos.Where(x => x.Numero.Equals(numero)).ToList();
        }



    }
}