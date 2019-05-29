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
            return Json(UsuarioDao.ExcluirPeloId(id));
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Desenvolvedor")]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(Usuario model, int? IdPerfil)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;
            model.Perfil = PerfilDao.RecuperarPeloId(IdPerfil);
            if (model.Perfil == null)
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
                        if ((!UsuarioDao.VerificarLogin(model) && !UsuarioDao.VerificarEmail(model)) || UsuarioDao.VerificarNomeEmailEId(model))
                        {

                            if (model.Senha == _senhaPadrao)
                            {
                                model.Senha = null;
                            }


                            var id = UsuarioDao.Salvar(model, IdPerfil);
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
                            resultado = "Não é possível cadastrar um usuário com o mesmo login ou e-mail.";
                        }

                    }
                    catch (Exception ex)
                    {
                        resultado = "ERRO";
                    }
                }
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