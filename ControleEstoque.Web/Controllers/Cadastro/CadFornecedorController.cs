using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{

    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadFornecedorController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = FornecedorDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            var quant = FornecedorDao.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;


            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult FornecedorPagina(int pagina, int tamPag, string filtro)
        {
            var lista = FornecedorDao.RecuperarLista(pagina, tamPag, filtro);

            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarFornecedor(int id)
        {
            return Json(FornecedorDao.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirFornecedor(int id)
        {
            string resultado = null;
            bool Ok = false;

            Fornecedor logData = FornecedorDao.RecuperarPeloId(id);
            Ok = FornecedorDao.ExcluirPeloId(id);

            if (Ok)
            {
                resultado = "OK";
            }
            else
            {
                resultado = "Não foi possível excluir esse Fornecedor.";
            }

            if (resultado.Equals("OK"))
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                    .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                        .MontarLog(User.Identity.Name.ToString(), "Excluir Fornecedor", "ALTA", logData),
                                        User.Identity.Name.ToString()
                                      );
            }
            else
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                                   .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                                       .MontarLog(User.Identity.Name.ToString(), "ERRO: Excluir Fornecedor", "EXTREMA", logData),
                                                       User.Identity.Name.ToString()
                                                     );
            }


            return Json(new { OK = Ok, Resultado = resultado });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarFornecedor(Fornecedor model)
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

                    if (!FornecedorDao.VerificarNumDocumento(model) || FornecedorDao.VerificarNumDocumentoEId(model))
                    {
                        var id = FornecedorDao.Salvar(model);
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
                        resultado = "Não foi possível cadastrar esse fornecedor pois já existe outro fornecedor com o mesmo CPF ou CNPJ.";
                    }
                }
                #pragma warning disable 0168
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            if (resultado.Equals("OK"))
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                    .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                        .MontarLog(User.Identity.Name.ToString(), "Cadastrar Fornecedor", "BAIXA", model),
                                        User.Identity.Name.ToString()
                                      );
            }
            else
            {
                APIServicos.GoogleSheets.GoogleSheetsAPI
                                   .RequestLogsGravar(APIServicos.GoogleSheets.GoogleSheetsAPI
                                                       .MontarLog(User.Identity.Name.ToString(), "ERRO: Cadastrar Fornecedor", "BAIXA", model),
                                                       User.Identity.Name.ToString()
                                                     );
            }



            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }
    }
}