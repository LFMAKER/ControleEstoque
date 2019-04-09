using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ControleEstoque.Web.Controllers
{
    public class ProfileController : Controller
    {
        
        // GET: Profile
        public ActionResult Index()
        {

            var login = User.Identity.Name;
            var logado = UsuarioModel.RecuperarIdLogado(login);
            ViewBag.UserIdLogado = logado.Id;
            var IdPerfil = logado.IdPerfil;
            var Perfil = PerfilModel.RecuperarPeloId(IdPerfil);
            ViewBag.TagPerfil = Perfil.Nome;

            return View();
        }



    }
}