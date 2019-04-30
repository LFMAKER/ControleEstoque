using ControleEstoque.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class UsuarioModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o Login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Informe a Senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe o Nome")]
        public string Nome { get; set; }


       
    }
}



