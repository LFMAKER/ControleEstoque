using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Cadastro
{
    [Authorize(Roles = "Gerente, Desenvolvedor")]
    public class CadPerfilController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;


        public ActionResult Index()
        {

            ViewBag.ListaUsuario = UsuarioDao.RecuperarLista();

            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = PerfilDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);

            var quant = PerfilDao.RecuperarQuantidade();
            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;

            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;
            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult PerfilPagina(int pagina, int tamPag, string filtro)
        {
            var lista = PerfilDao.RecuperarLista(pagina, tamPag, filtro);
            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarPerfil(int id)
        {
            
            var ret = PerfilDao.RecuperarPeloId(id);

            return Json(ret);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirPerfil(int id)
        {
            return Json(PerfilDao.ExcluirPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarPerfil(Perfil model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                

                try
                {
                    if (!PerfilDao.VerificarNome(model) || PerfilDao.VerificarNomeEId(model))
                    {

                    
                    var id = PerfilDao.Salvar(model);
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
                        resultado = "Não é possível cadastrar um perfil com o mesmo nome!";
                    }

                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }


    }
}