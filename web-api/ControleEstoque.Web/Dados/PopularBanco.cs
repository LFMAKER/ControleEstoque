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

        public static void Inserir(ControleEstoque.Web.Models.Context context)
        {

            
            var existingPerfil = context.Perfis.Where(x => x.Nome.Equals("Gerente")).FirstOrDefault();
            if(existingPerfil == null)
            {
                Perfil perfil = new Perfil
                {
                    Nome = "Gerente",
                    Ativo = true
                };
                context.Perfis.Add(perfil);
            }


            var existingUser = context.Usuarios.Where(x => x.Login.Equals("admin")).FirstOrDefault();
            if(existingUser == null)
            {
                Usuario user = new Usuario
                {
                    Login = "admin",
                    Senha = Helpers.CriptoHelper.HashMD5("admin"),
                    Perfil = ctx.Perfis.Find(1),
                    IdPerfil = 1,
                    Email = "admin@admin.com",
                    Nome = "admin"

                };
                context.Usuarios.Add(user);
            }


            


        }

    }
}