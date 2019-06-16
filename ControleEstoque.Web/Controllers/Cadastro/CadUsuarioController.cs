using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Cadastro
{

    public class CadUsuarioController : Controller
    {


        private const string _senhaPadrao = "{$127;$188}";
        private const int _quantMaxLinhasPorPagina = 5;
        [Authorize(Roles = "Gerente, Desenvolvedor")]
        public ActionResult Index()
        {


            ViewBag.SenhaPadrao = _senhaPadrao;

            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);


            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = UsuarioDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);

            var quant = UsuarioDao.RecuperarQuantidade();
            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;

            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;
            ViewBag.Perfis = PerfilDao.RecuperarLista(0, 0, "", true);
            //ViewBag.ddl_perfil = new SelectList(PerfilDao.RecuperarLista(), "Id", "Nome");

            return View(lista);
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Desenvolvedor")]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarUsuario(int id)
        {
            return Json(UsuarioDao.RecuperarPeloId(id));
        }
        [Authorize(Roles = "Gerente, Desenvolvedor, Analista")]
        public ActionResult RecuperarUsuarioComMd5(int id)
        {
            return Json(UsuarioDao.RecuperarPeloId(id));
        }



        [HttpPost]
        [Authorize(Roles = "Gerente, Desenvolvedor")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {
            string resultado = null;
            bool Ok = false;
            Usuario logData = UsuarioDao.RecuperarPeloId(id);

            Ok = UsuarioDao.ExcluirPeloId(id);

            if (Ok)
            {
                resultado = "OK";
            }
            else
            {
                resultado = "Não foi possível excluir esse Usuário.";
            }

            if (resultado.Equals("OK"))
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                    .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                        .MontarLog(User.Identity.Name.ToString(), "Excluir Usuário", "ALTA", logData),
                                        User.Identity.Name.ToString()
                                      );
            }
            else
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                                   .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                                       .MontarLog(User.Identity.Name.ToString(), "ERRO: Excluir Usuário", "EXTREMA", logData),
                                                       User.Identity.Name.ToString()
                                                     );
            }



            return Json(new { OK = Ok, Resultado = resultado });
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Desenvolvedor")]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(Usuario model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;
            model.Perfil = PerfilDao.RecuperarPeloId(model.IdPerfil);
            if (model.Perfil ==  null)
            {
                resultado = "O campo Perfil é obrigatório!";

            }
            else
            {

                if (!ModelState.IsValid)
                {
                    resultado = "AVISO";
                    mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    try
                    {
                        if ((!UsuarioDao.VerificarLogin(model) && !UsuarioDao.VerificarEmail(model)) 
                            || (UsuarioDao.VerificarLoginEId(model) || UsuarioDao.VerificarEmailEId(model)) 
                            || UsuarioDao.VerificarNomeEmailEId(model))
                        {

                            if (model.Senha == _senhaPadrao)
                            {
                                model.Senha = null;
                            }


                            var id = UsuarioDao.Salvar(model);
                            if (id > 0)
                            {
                                idSalvo = id.ToString();
                            }
                            else
                            {
                                resultado = "ERRO";
                            }
                        }else
                        {
                            resultado = "Não foi possível cadastrar esse usuário pois já existe outro usuário com o mesmo Login ou E-mail.";
                        }

                    }
                    catch (Exception ex)
                    {
                        resultado = "ERRO";
                    }
                }
            }

            if (resultado.Equals("OK"))
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                    .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                        .MontarLog(User.Identity.Name.ToString(), "Cadastrar Usuário", "BAIXA", model),
                                        User.Identity.Name.ToString()
                                      );
            }
            else
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                                   .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                                       .MontarLog(User.Identity.Name.ToString(), "ERRO: Cadastrar Usuário", "BAIXA", model),
                                                       User.Identity.Name.ToString()
                                                     );
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }



        [HttpPost]
        [Authorize(Roles = "Gerente, Desenvolvedor")]
        [ValidateAntiForgeryToken]
        public JsonResult UsuarioPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            var lista = UsuarioDao.RecuperarLista(pagina, tamPag, filtro);
            return Json(lista);
        }



    }
}