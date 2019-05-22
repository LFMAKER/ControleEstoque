using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Models
{
    public class LocalArmazenamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        public string Nome { get; set; }
        public int CapacidadeTotal { get; set; }
        public int CapacidadeAtual { get; set; }

        public bool Ativo { get; set; }

    }
}