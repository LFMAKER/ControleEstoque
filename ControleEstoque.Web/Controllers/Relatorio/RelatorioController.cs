using ControleEstoque.Web.Dal.Grafico;
using ControleEstoque.Web.Models.Domain;
using System;
using Rotativa;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Relatorio
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        /*
         @todo: Export PDF "Relatorio"
         @body: Create a controller to return a report in pdf
             */


        public ActionResult RelatorioEntradasIndex()
        {
            return View();
        }


        //GetEntradasGraficoFiltro
        [HttpPost]
        public ActionResult RelatorioEntradasFiltro(DateTime txt_data_inicio, DateTime txt_data_fim, int cbx_tipo)
        {

            if(cbx_tipo == 1)
            {
                var relatorioPDF = new ViewAsPdf
                {
                    ViewName = "RelatorioEntradasPDF",
                    IsGrayScale = false,
                    FileName = "RelatorioEntradasPDF.pdf",
                    Model = EntradaESaidaGraficoDao.GetEntradasGraficoFiltro(txt_data_inicio, txt_data_fim)
                };
                return relatorioPDF;
            }
            return null;
        }


        public async Task<ActionResult> RelatorioEntradas()
        {

            List<EntradaGraficos> entradas = await EntradaESaidaGraficoDao.GetEntradasGrafico();
            return View(entradas);
        }





    }
}