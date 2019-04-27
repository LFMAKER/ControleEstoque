using ControleEstoque.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using ControleEstoque.Web.Dal.Cadastro;

namespace ControleEstoque.Web.Models
{
    public class PerfilModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public List<UsuarioModel> Usuarios { get; set; }

        public PerfilModel()
        {
            this.Usuarios = new List<UsuarioModel>();
        }

        public void CarregarUsuarios(PerfilModel pm)
        {

            Usuarios.Clear();
            PerfilDao.CarregarUsers(pm);


        }
    }
}