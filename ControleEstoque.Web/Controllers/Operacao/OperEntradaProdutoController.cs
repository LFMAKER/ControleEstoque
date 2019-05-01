﻿using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;

namespace ControleEstoque.Web.Controllers
{
    public class OperEntradaProdutoController : OperEntradaSaidaProdutoController
    {
        protected override string SalvarPedido(EntradaSaidaProdutoViewModel dados)
        {
            return ProdutoDao.SalvarPedidoEntrada(dados.Data, dados.Produtos);
        }
    }
}