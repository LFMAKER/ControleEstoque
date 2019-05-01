using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;

namespace ControleEstoque.Web.Controllers
{
    public class OperSaidaProdutoController : OperEntradaSaidaProdutoController
    {
        protected override string SalvarPedido(EntradaSaidaProdutoViewModel dados)
        {
            return ProdutoDao.SalvarPedidoSaida(dados.Data, dados.Produtos);
        }
    }
}