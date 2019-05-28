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

            //Paginação Entrada
            var quantEntradas = ProdutoDao.RecuperarQuantidadeEntradaProdutos();
            var difQuantPaginasEntradas = (quantEntradas % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;

            ViewBag.QuantPaginasEntradas = (quantEntradas / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginasEntradas;



            //Paginação Saídas


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

        public JsonResult Salvar([ModelBinder(typeof(EntradaSaidaProdutoViewModelModelBinder))]EntradaSaidaProdutoViewModel dados)
        {

            //TODO: Verificar o estoque dos produtos caso seja o mesmo local de armazenamento

            var numPedido = "";
            var Mensagem = "";
            List <object> pedidosSalvos = new List<object>();
            var ok = false;
           
            /*Na EntradaSaidaProdutoViewModelModelBinder é realizado a validação das entradas,
             *não é possível realizar várias entradas de um mesmo produtos, apenas será possível
             *realizar entradas de produtos diferentes.
             * Caso exista algum igual, a classe EntradaSaidaProdutoViewModelModelBinder
             * definirá a dictionary de produtos como null.
             * */
            if (dados.Produtos != null)
            {
                numPedido = SalvarPedido(dados);
                pedidosSalvos.Add(ProdutoDao.RecuperarEntradaPorNumero(numPedido));
                Mensagem = "Entrada realizada com sucesso!";
                ok = (numPedido != "");

            }
            else
            {
                Mensagem = "A Entrada não foi realizada pois você informou duas ou mais entradas de um mesmo produto, " +
                    "isso causa inconsistência no armazenamento e por esse motivo sua entrada foi cancelada";
                ok = false;
            }



            //var numPedidoOuMensagem = SalvarPedido(dados);
            //var ok = (numPedido != "");

            return Json(new { OK = ok, Numero = numPedido, Mensagem = Mensagem, Pedidos = pedidosSalvos });
        }
    }
}