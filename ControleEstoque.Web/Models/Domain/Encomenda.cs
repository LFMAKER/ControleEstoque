using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models.Domain
{
    public class Encomenda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string LocalCompra { get; set; }
        public string CodigoRastreio { get; set; }
        public string StatusEncomenda { get; set; }
    }
}