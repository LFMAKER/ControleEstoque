using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Operacao
{
    public class OperEncomendaController : Controller
    {
        // GET: OperEncomenda
        public ActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public JsonResult RastreioEncomenda()
        {
            string result = API.Correios.CorreiosAPI.Rastreio();
            string json = JsonConvert.SerializeObject(result);

            return Json(result);
        }

    }
}