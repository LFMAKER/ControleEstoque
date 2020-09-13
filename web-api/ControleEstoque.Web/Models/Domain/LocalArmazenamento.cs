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
        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres!")]
        public string Nome { get; set; }
        public int CapacidadeTotal { get; set; }
        public int CapacidadeAtual { get; set; }

        public bool Ativo { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + " | Nome: " + Nome + " | Capacidade Total: " + CapacidadeTotal + "Capacidade Atual: " + CapacidadeAtual;
        }


    }
}