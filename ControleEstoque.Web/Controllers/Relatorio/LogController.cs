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
            var UserLogado = User.Identity.Name;
            List<Log> logs = APIServicos.GoogleSheets.GoogleSheetsAPI.RequestLogsListar(UserLogado);
            return View(logs);
        }

        public ActionResult AutenticarGoogle()
        {
            var UserLogado = User.Identity.Name;
            bool status = APIServicos.GoogleSheets.GoogleSheetsAPI.AutenticarGoogle(UserLogado);
            return Json(new { Status = status });
        }

    }
}