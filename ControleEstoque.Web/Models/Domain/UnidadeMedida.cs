using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Models
{
    public class UnidadeMedida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha a sigla.")]
        [MaxLength(3, ErrorMessage = "A sigla deve ter no máximo 3 caracteres!")]
        public string Sigla { get; set; }

        public bool Ativo { get; set; }

       
    }
}