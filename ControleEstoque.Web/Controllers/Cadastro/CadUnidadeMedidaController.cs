using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Dal.Cadastro;

namespace ControleEstoque.Web.Controllers.Cadastro
{
    public class CadUnidadeMedidaController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = UnidadeMedidaDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            var quant = UnidadeMedidaDao.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult UnidadeMedidaPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            var lista = UnidadeMedidaDao.RecuperarLista(pagina, tamPag, filtro);

            return Json(lista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarUnidadeMedida(int id)
        {
            return Json(UnidadeMedidaDao.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirUnidadeMedida(int id)
        {
            string resultado = null;
            bool Ok = false;
            UnidadeMedida logData = UnidadeMedidaDao.RecuperarPeloId(id);

            Ok = UnidadeMedidaDao.ExcluirPeloId(id);

            if (Ok)
            {
                resultado = "OK";
            }
            else
            {
                resultado = "Não foi possível excluir essa Unidade de Medida.";
            }



            if (resultado.Equals("OK"))
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                    .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                        .MontarLog(User.Identity.Name.ToString(), "Excluir Unidade de Medida", "ALTA", logData),
                                        User.Identity.Name.ToString()
                                      );
            }
            else
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                                   .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                                       .MontarLog(User.Identity.Name.ToString(), "ERRO: Excluir Unidade de Medida", "EXTREMA", logData),
                                                       User.Identity.Name.ToString()
                                                     );
            }


            return Json(new { OK = Ok, Resultado = resultado });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarUnidadeMedida(UnidadeMedida model)
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
                  
                    if ((!UnidadeMedidaDao.VerificarNome(model) && !UnidadeMedidaDao.VerificarSigla(model)) 
                        || (UnidadeMedidaDao.VerificarNomeEId(model) || UnidadeMedidaDao.VerificarSiglaEId(model)) 
                        || UnidadeMedidaDao.VerificarNomeSiglaEId(model))
                    {
                        var id = UnidadeMedidaDao.Salvar(model);
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
                        resultado = "Não foi possível cadastrar essa unidade de medida pois já existe outra unidade de medida com o mesmo Nome ou Sigla.";
                    }

                }
                #pragma warning disable 0168
                catch (Exception ex)
                {
                    resultado = "ERRO: a sigla deve ter no máximo 3 caracteres.";
                }
            }


            if (resultado.Equals("OK"))
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                    .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                        .MontarLog(User.Identity.Name.ToString(), "Cadastrar Unidade de Medida", "BAIXA", model),
                                        User.Identity.Name.ToString()
                                      );
            }
            else
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                                   .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                                       .MontarLog(User.Identity.Name.ToString(), "ERRO: Cadastrar Unidade de Medida", "BAIXA", model),
                                                       User.Identity.Name.ToString()
                                                     );
            }



            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }
    }
}