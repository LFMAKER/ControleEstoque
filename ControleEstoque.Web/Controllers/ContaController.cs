using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControleEstoque.Web.Models;
using System.Web.Security;
using ControleEstoque.Web.Dal.Cadastro;

namespace ControleEstoque.Web.Controllers
{
    public class ContaController : Controller
    {


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var usuario = UsuarioDao.ValidarUsuario(login.Usuario, login.Senha);





            if (usuario != null)
            {
                var userString = UsuarioDao.RecuperarStringNomePerfis(usuario);

                //FormsAuthentication.SetAuthCookie(login.Usuario, login.LembrarMe);
                var ticket = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(
                    1, login.Usuario, DateTime.Now, DateTime.Now.AddHours(12), login.LembrarMe, userString));

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
                Response.Cookies.Add(cookie);


                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    //Autenticando no google
                    if (APIServicos.GoogleSheets.GoogleSheetsAPI.AutenticarGoogle(User.Identity.Name))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login inválido.");
            }

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

}
