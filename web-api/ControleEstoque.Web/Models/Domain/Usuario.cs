using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using ControleEstoque.Web.Models.Domain;
namespace ControleEstoque.Web.Models
{
    public class Usuario
    {
 
        public int Id { get; set; }
        [Required(ErrorMessage = "Informe o login")]
        [MaxLength(50, ErrorMessage = "O login pode ter no máximo 50 caracteres.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Informe o senha")]
        [MaxLength(50, ErrorMessage = "A senha pode ter no máximo 50 caracteres.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        [MaxLength(200, ErrorMessage = "O nome pode ter no máximo 200 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o e-mail")]
        [MaxLength(200, ErrorMessage = "O e-mail pode ter no máximo 200 caracteres.")]
        public string Email {get;set; }
        public int IdPerfil { get; set; }
        public Perfil Perfil { get; set; }
        public KeyControle KeyC { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + " | Login: " + Login + " | Senha: " + Senha
                + " | Nome: " + Nome + " | E-mail: " + Email + " | IdPerfil: " + IdPerfil;
        }

    }
}