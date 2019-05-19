using ControleEstoque.Web.Dal.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.APIExport
{
    public class ApiExportController : Controller
    {
        // GET: ApiExport
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetFornecedores()
        {
            var lista = FornecedorDao.RecuperarLista();

            return this.Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}