using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Models
{
    public class Usuario
    {
 
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Informe o senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o e-mail")]
        public string Email {get;set; }
        public int IdPerfil { get; set; }
        public Perfil Perfil { get; set; }


    }
}