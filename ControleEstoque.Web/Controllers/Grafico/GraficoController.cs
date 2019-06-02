using ControleEstoque.Web.Dal.Grafico;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class GraficoController : Controller
    {
        [Authorize]
        public ActionResult PerdaMes()
        {
            return View();
        }
        [Authorize]
        public ActionResult EntradaSaidaMesa()
        {


            return View();
        }

        [Authorize]
        public async Task<JsonResult> GetDadosEntradas()
        {
            List<EntradaGraficos> entradasResult = new List<EntradaGraficos>();
            entradasResult = await EntradaESaidaGraficoDao.GetEntradasGrafico();

            return Json(entradasResult);
        }



        [Authorize]
        public async Task<JsonResult> GetDadosSaidas()
        {
            List<SaidaGraficos> saidasResult = new List<SaidaGraficos>();
            saidasResult = await EntradaESaidaGraficoDao.GetSaidasGrafico();

            return Json(saidasResult);
        }


        [Authorize]
        public async Task<JsonResult> GetGanhosMesAtualEAnoAtual()
        {
            DateTime dataAtual = new DateTime();
            dataAtual = DateTime.Now;

            EntradaGraficos entradas = new EntradaGraficos();
            SaidaGraficos saidas = new SaidaGraficos();
            decimal resultadoMes;

            entradas = await EntradaESaidaGraficoDao.GetEntradaGastoMesAtual(dataAtual);
            saidas = await EntradaESaidaGraficoDao.GetSaidaGanhoMesAtual(dataAtual);

            if(saidas != null && entradas != null)
            {
                resultadoMes = saidas.total - entradas.total;

            }else
            {
                resultadoMes = 0;
            }

            //Calculando Ano
            decimal resultadoAno;

            decimal totalEntradas = await EntradaESaidaGraficoDao.GetEntradaGastoAnoAtual(dataAtual);
            decimal totalSaidas = await EntradaESaidaGraficoDao.GetSaidaGanhoAnoAtual(dataAtual);

            resultadoAno = totalSaidas - totalEntradas;



            return Json( new {ResultadoMes = resultadoMes, ResultadoAno = resultadoAno });
        }


     

    }
}