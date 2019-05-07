using ControleEstoque.Web.Dal.Grafico;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public JsonResult GetDadosEntradas()
        {
            List<EntradaGraficos> entradasResult = new List<EntradaGraficos>();
            entradasResult = EntradaESaidaGraficoDao.GetEntradasGrafico();

            return Json(entradasResult);
        }



        [Authorize]
        public JsonResult GetDadosSaidas()
        {
            List<SaidaGraficos> saidasResult = new List<SaidaGraficos>();
            saidasResult = EntradaESaidaGraficoDao.GetSaidasGrafico();

            return Json(saidasResult);
        }




    }
}