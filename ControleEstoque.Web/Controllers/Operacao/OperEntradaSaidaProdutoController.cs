using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public abstract class OperEntradaSaidaProdutoController : Controller
    {

        private const int _quantMaxLinhasPorPagina = 5;

        public ActionResult Index()
        {
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;


            ViewBag.Produtos = ProdutoDao.RecuperarLista(somenteAtivos: true);
            ViewBag.Entradas = ProdutoDao.RecuperarListaEntradaProdutos(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            ViewBag.Saidas = ProdutoDao.RecuperarListaSaidaProdutos(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);

            //Paginação Entrada
            var quantEntradas = ProdutoDao.RecuperarQuantidadeEntradaProdutos();
            var difQuantPaginasEntradas = (quantEntradas % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;

            ViewBag.QuantPaginasEntradas = (quantEntradas / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginasEntradas;



            //Paginação Saídas
            var quantSaidas = ProdutoDao.RecuperarQuantidadeSaidaProdutos();
            var difQuantPaginasSaidas = (quantSaidas % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;

            ViewBag.QuantPaginasSaidas = (quantSaidas / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginasSaidas;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EntradaSaidaPagina(int pagina, int tamPag, string dataInicio, string dataFim, string tipo)
        {
            List<object> lista = new List<object>();


            if (tipo.Equals("entrada"))
            {
                lista.Add(ProdutoDao.RecuperarListaEntradaProdutos(pagina, tamPag, dataInicio, dataFim));
            }else if (tipo.Equals("saida"))
            {
                lista.Add(ProdutoDao.RecuperarListaSaidaProdutos(pagina, tamPag, dataInicio, dataFim));
            }


            return Json(lista);
        }
       

        [HttpPost]
        [Authorize]
        public JsonResult RemoverEntradaSaidaProduto(int? id, string tipo)
        {
            var ok = false;
            var mensagem = "";


            var result = ProdutoDao.RemoverEntradaSaidaProduto(id, tipo);
            if (result)
            {
                ok = true;
                mensagem = "Exclusão realizada com sucesso!";
            }else
            {
                ok = false;
                mensagem = "Ocorreu um erro no processo de exclusão";
            }

            return Json(new {OK = ok, Mensagem = mensagem });
        }


        protected abstract string SalvarPedido(EntradaSaidaProdutoViewModel dados);

     
    }
}