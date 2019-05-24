using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ControleEstoque.Web.Models
{
    public class EntradaSaidaProdutoViewModelModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valores = controllerContext.HttpContext.Request.Form;

            var ret = new EntradaSaidaProdutoViewModel() { Produtos = new Dictionary<int, int>() };

            try
            {
                ret.Data = DateTime.ParseExact(valores.Get("data"), "yyyy-MM-dd", null);

                if (!string.IsNullOrEmpty(valores.Get("produtos")))
                {
                    var produtos = JsonConvert.DeserializeObject<List<dynamic>>(valores.Get("produtos"));

                    foreach (var produto in produtos)
                    {
                        /*Tentando adicionar os produtos
                         * Apenas um produto com o mesmo IdProduto será adicionado, caso
                         * tenha outra entrada ou saída com o mesmo IdProduto (Key), a lista de produtos será
                         * zerada, recebendo null e assim cancelando a entrada.
                         * */
                        try
                        {
                            ret.Produtos.Add((int)produto.IdProduto, (int)produto.Quantidade);
                        }
                        catch (ArgumentException)
                        {
                            ret.Produtos = null;
                        }
                    }




                };
            }
            catch (System.Exception ex)
            {
                throw;
            }

            return ret;
        }
    }
}