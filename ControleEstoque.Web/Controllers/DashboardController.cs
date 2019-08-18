using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ControleEstoque.Web.Dal.Cadastro;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    [HandleError]
    public class DashboardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
       
            return View();
        }


        [Authorize]
        public ActionResult IBM()
        {
            return View();
        }


        [Authorize]
        public ActionResult APIDOC()
        {
            ViewBag.KeyAtual = UsuarioDao.KeyAtual(User.Identity.Name);
            return View();
        }

        [Authorize]
        public ActionResult APIResult()
        {
            string key = UsuarioDao.GerarESalvarKey(User.Identity.Name);
            return Json(new {apiKey = key });
        }


    }
}