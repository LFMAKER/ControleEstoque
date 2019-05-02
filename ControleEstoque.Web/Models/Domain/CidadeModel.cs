using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Models
{
    public class CidadeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        [MaxLength(30, ErrorMessage = "O nome pode ter no máximo 30 caracteres.")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Selecione o país.")]
        public int IdPais { get; set; }

        [Required(ErrorMessage = "Selecione o estado.")]
        public int IdEstado { get; set; }

        public virtual EstadoModel Estado { get; set; }

    
    }
}