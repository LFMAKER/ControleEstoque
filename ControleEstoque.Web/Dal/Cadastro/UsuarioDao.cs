using ControleEstoque.Web.Helpers;
using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class UsuarioDao
    {
        private static Context ctx = SingletonContext.GetInstance();

        /// <summary>
        /// Retorna um usuário válido ou null
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static Usuario ValidarUsuario(string login, string senha)
        {
            Usuario ret = null;
            senha = CriptoHelper.HashMD5(senha);
            ret = ctx.Usuarios.Where(x => x.Login.Equals(login) && x.Senha.Equals(senha)).FirstOrDefault();
            return ret;
        }

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            ret = ctx.Usuarios.Count();
            return ret;

        }

        public static List<Usuario> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            var ret = new List<Usuario>();

            if (tamPagina != 0 && pagina != 0)
            {
                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.Usuarios.Include("Perfil").OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.Usuarios.Include("Perfil").OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }
            else
            {
                ret = ctx.Usuarios.Include("Perfil").OrderBy(x => x.Nome).ToList();
            }

            return ret;



        }

        public static Usuario RecuperarPeloId(int id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static Usuario RecuperarPeloLogin(string login)
        {
            return ctx.Usuarios.Where(x => x.Login.Equals(login)).FirstOrDefault();
        }


        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPeloId(id) != null)
            {

                Usuario user = ctx.Usuarios.Find(id);
                ctx.Usuarios.Remove(user);
                ctx.SaveChanges();
                ret = true;
            }

            return ret;
        }


        //O método Salvar deve ser construído.
        public static int Salvar(Usuario um, int? IdPerfil)
        {
            var ret = 0;
            um.Perfil = PerfilDao.RecuperarPeloId(IdPerfil);
            var model = RecuperarPeloId(um.Id);

            if (model == null)
            {
                //Encriptando a senha
                um.Senha = CriptoHelper.HashMD5(um.Senha);
                ctx.Usuarios.Add(um);
            }
            else
            {
                if (!string.IsNullOrEmpty(um.Senha))
                {
                    um.Senha = CriptoHelper.HashMD5(um.Senha);
                    //var sql = "update usuario set nome=@nome, login=@login, senha=@senha where id = @id";
                    //var parametros = new { id = um.Id, nome = um.Nome, login = um.Login, senha = CriptoHelper.HashMD5(um.Senha) };
                    //if (ctx.Database.Connection.Execute(sql, parametros) > 0)
                    //{
                    //    ret = um.Id;
                    //}

                    ctx.Entry(um).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    //var sql = "update usuario set nome=@nome, login=@login where id = @id";
                    //var parametros = new { id = um.Id, nome = um.Nome, login = um.Login };
                    //if (ctx.Database.Connection.Execute(sql, parametros) > 0)
                    //{
                    //    ret = um.Id;
                    //}

                    ctx.Entry(um).State = System.Data.Entity.EntityState.Modified;
                }
            }
            ctx.SaveChanges();
            ret = um.Id;
            //}

            return ret;



        }

        public static string RecuperarStringNomePerfis(Usuario um)
        {
            var ret = string.Empty;

            //using (var ctx = new Context())
            //{
            ret = ctx.Usuarios.Where(x => x.Nome.Equals(um.Nome)).Include("Perfil").Select(x => x.Perfil.Nome).SingleOrDefault();
            //}
            return ret;
        }


        public static void SalvarUsuarioComPerfilNoModelo(Usuario um)
        {
            ctx.Usuarios.Add(um);
        }


    }
}