using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.APIExport
{
    public class ApiExportController : Controller
    {
        [HttpGet]
        public JsonResult GetFornecedores()
        {
            var lista = FornecedorDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUnidadesMedida()
        {
            var lista = UnidadeMedidaDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGruposProduto()
        {
            var lista = GrupoProdutoDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLocaisArmazenamento()
        {
            var lista = LocalArmazenamentoDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMarcasProduto()
        {
            var lista = MarcaProdutoDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPerfils()
        {
            var lista = PerfilDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProdutos()
        {
            var lista = ProdutoDao.RecuperarLista();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUsuarios(string login, string senha)
        {

            if (login != null && senha != null)
            {
                var user = UsuarioDao.ValidarUsuario(login, senha);
                if (user != null)
                {
                    var lista = ProdutoDao.RecuperarLista();

                    return Json(lista, JsonRequestBehavior.AllowGet);
                }

                return Json(new { ERRO = "A Autenticação falhou! Tente novamente com um usuário válido." }, JsonRequestBehavior.AllowGet);
            }else
            {
                return Json(new { ERRO = "Para obter essa informação, você deve autenticar com um login e senha passando como parâmetro  na url." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}