using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ControleEstoque.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            var serverError = Server.GetLastError() as HttpException;

            if (ex is HttpRequestValidationException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.ContentType = "application/json";
                Response.Write("{ \"Resultado\":\"AVISO\",\"Mensagens\":[\"Somente texto sem caracteres especiais pode ser enviado.\"],\"IdSalvo\":\"\"}");
                Response.End();
            }
            else if (ex is HttpAntiForgeryException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.End();
                // gravar LOG
            }

            if (serverError != null)
            {
                if (serverError.GetHttpCode() == 404)
                {
                    Server.ClearError();
                    Response.Redirect("~/Error/PageNotFound");
                }
            }

        }

        //Usar o Cookie enviado no momento do login para preencher o GenericPrincipal
        
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if(cookie != null && cookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket;
                try
                {
                    ticket = FormsAuthentication.Decrypt(cookie.Value);
                }
                catch
                {

                    return;
                }


                var perfis = ticket.UserData.Split(';');

                if (Context.User != null)
                {
                    Context.User = new GenericPrincipal(Context.User.Identity, perfis);

                }
               


            }



        }



    }
}
