using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dados
{
    public class PopularBanco
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static void Inserir()
        {


            Perfil perfil = new Perfil
            {
                Nome = "Gerente",
                Ativo = true
            };
            PerfilDao.Salvar(perfil);


            Usuario user = new Usuario
            {
                Login = "admin",
                Senha = Helpers.CriptoHelper.HashMD5("admin"),
                Perfil = ctx.Perfis.Find(1)

            };
            UsuarioDao.Salvar(user);





        }

    }
}