using System;

namespace ControleEstoque.Web.Models
{
    public class SaidaProduto
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

    }
}