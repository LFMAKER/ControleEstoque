using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Relatorio
{
    public class LogController : Controller
    {

        [Authorize(Roles = "Gerente,Administrativo")]
        public ActionResult Index()
        {
            List<Log> logs = APIServicos.GoogleSheets.GoogleSheetsAPI.RequestLogsListar();
            return View(logs);
        }
    }
}