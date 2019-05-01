using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public abstract class OperEntradaSaidaProdutoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Produtos = ProdutoDao.RecuperarLista(somenteAtivos: true);

            return View();
        }

        protected abstract string SalvarPedido(EntradaSaidaProdutoViewModel dados);

        public JsonResult Salvar([ModelBinder(typeof(EntradaSaidaProdutoViewModelModelBinder))]EntradaSaidaProdutoViewModel dados)
        {
            var numPedido = SalvarPedido(dados);
            var ok = (numPedido != "");

            return Json(new { OK = ok, Numero = numPedido });
        }
    }
}