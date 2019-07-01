using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

    }
}