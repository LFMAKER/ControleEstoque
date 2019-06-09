using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadMarcaProdutoController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = MarcaProdutoDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            var quant = MarcaProdutoDao.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult MarcaProdutoPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            var lista = MarcaProdutoDao.RecuperarLista(pagina, tamPag, filtro);

            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarMarcaProduto(int id)
        {
            //return Json(false);
            return Json(MarcaProdutoDao.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirMarcaProduto(int id)
        {
            string resultado = null;
            bool Ok = false;


            Ok = MarcaProdutoDao.ExcluirPeloId(id);

            if (Ok)
            {
                resultado = "OK";
            }
            else
            {
                resultado = "Não foi possível excluir essa Marca de Produto.";
            }
            return Json(new { OK = Ok, Resultado = resultado });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarMarcaProduto(MarcaProduto model)
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
                    if (!MarcaProdutoDao.VerificarNome(model) || MarcaProdutoDao.VerificarNomeEId(model))
                    {
                        var id = MarcaProdutoDao.Salvar(model);
                        if (id > 0)
                        {
                            idSalvo = id.ToString();
                        }
                        else
                        {
                            resultado = "ERRO";
                        }

                    }
                    else
                    {
                        resultado = "Não foi possível cadastrar essa marca de produtos pois já existe outra marca de produtos com o mesmo Nome.";
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