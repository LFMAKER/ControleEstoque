using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    public class OperSaidaProdutoController : OperEntradaSaidaProdutoController
    {
        protected override string SalvarPedido(EntradaSaidaProdutoViewModel dados)
        {
            return ProdutoDao.SalvarPedidoSaida(dados.Data, dados.Produtos);
        }



        public JsonResult Salvar([ModelBinder(typeof(EntradaSaidaProdutoViewModelModelBinder))]EntradaSaidaProdutoViewModel dados)
        {

            //TODO: Verificar o estoque dos produtos caso seja o mesmo local de armazenamento

            var numPedido = "";
            var Mensagem = "";
            List<object> pedidosSalvos = new List<object>();
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
                pedidosSalvos.Add(ProdutoDao.RecuperarSaidaPorNumero(numPedido));
                Mensagem = "Saída realizada com sucesso!";
                ok = (numPedido != "");

            }
            else
            {
                Mensagem = "A Saída não foi realizada pois você informou duas ou mais saídas de um mesmo produto, " +
                    "isso causa inconsistência no estoque atual desse produto e por esse motivo sua saída foi cancelada";
                ok = false;
            }



            //var numPedidoOuMensagem = SalvarPedido(dados);
            //var ok = (numPedido != "");

            return Json(new { OK = ok, Numero = numPedido, Mensagem = Mensagem, Pedidos = pedidosSalvos });
        }



    }
}