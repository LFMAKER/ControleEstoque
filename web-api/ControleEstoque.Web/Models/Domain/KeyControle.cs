using System;
namespace ControleEstoque.Web.Models
{
    public class KeyControle
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime UltimoUso { get; set; }
        public int QuantidadeDeChamadas { get; set; }
        public bool Ativada { get; set; }
    }
}