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

        public static Usuario ValidarUsuario(string login, string senha)
        {
            Usuario ret = null;
            senha = CriptoHelper.HashMD5(senha);
            //using (var ctx = new Context())
            //{
            ret = ctx.Usuarios.Where(x => x.Login.Equals(login) && x.Senha.Equals(senha)).FirstOrDefault();

            //var sql = "select * from usuario where login=@login and senha=@senha";
            //var parametros = new { login, senha = CriptoHelper.HashMD5(senha) };
            //ret = db.Database.Connection.Query<Usuario>(sql, parametros).SingleOrDefault();
            //}

            return ret;
        }

        public static int RecuperarQuantidade()
        {
            //    var ret = 0;

            //    using (var conexao = new SqlConnection())
            //    {
            //        conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //        conexao.Open();

            //        ret = conexao.ExecuteScalar<int>("select count(*) from usuario");
            //    }

            //    return ret;
            var ret = 0;
            //using (var ctx = new Context())
            //{
            ret = ctx.Usuarios.Count();
            //}
            return ret;

        }

        public static List<Usuario> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            //var ret = new List<Usuario>();

            //using (var ctx = new Context())
            //{


            //    string sql;
            //    if (pagina == -1 || tamPagina == -1)
            //    {
            //        sql =
            //            "select *" +
            //            "from usuario" +
            //            " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome");
            //    }
            //    else
            //    {
            //        var pos = (pagina - 1) * tamPagina;
            //        sql = string.Format(
            //            "select *" +
            //            " from usuario" +
            //            " offset {0} rows fetch next {1} rows only",
            //            pos > 0 ? pos - 1 : 0, tamPagina);
            //    }

            //    ret = ctx.Database.Connection.Query<Usuario>(sql).ToList();
            //}

            //return ret;


            var ret = new List<Usuario>();

            //using (var ctx = new Context())
            //{
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
            //}

            return ret;



        }

        public static Usuario RecuperarPeloId(int id)
        {
            //Usuario ret = null;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from usuario where (id = @id)";
            //    var parametros = new { id };
            //    ret = conexao.Query<Usuario>(sql, parametros).SingleOrDefault();
            //}

            //return ret;

            //using (var ctx = new Context())
            //{
            return ctx.Usuarios.Find(id);
            //}
        }

        public static Usuario RecuperarPeloLogin(string login)
        {
            //Usuario ret = null;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from usuario where (login = @login)";
            //    var parametros = new { login };
            //    ret = conexao.Query<Usuario>(sql, parametros).SingleOrDefault();
            //}

            //return ret;

            //using (var ctx = new Context())
            //{
            return ctx.Usuarios.Where(x => x.Login.Equals(login)).FirstOrDefault();
            //}
        }


        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPeloId(id) != null)
            {
                //using (var conexao = new SqlConnection())
                //{
                //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                //    conexao.Open();

                //    var sql = "delete from usuario where (id = @id)";
                //    var parametros = new { id };
                //    ret = (conexao.Execute(sql, parametros) > 0);
                //}

                Usuario user = ctx.Usuarios.Find(id);

                //using (var ctx = new Context())
                //{
                ctx.Usuarios.Remove(user);
                ctx.SaveChanges();
                ret = true;
                //}
            }

            return ret;
        }

        public static int Salvar(Usuario um, int IdPerfil)
        {
            var ret = 0;
            //Recuperando o Perfil
            um.Perfil = PerfilDao.RecuperarPeloId(IdPerfil);

            var model = RecuperarPeloId(um.Id);

            //using (var ctx = new Context())
            //{


            if (model == null)
            {
                //Encriptando a senha
                um.Senha = CriptoHelper.HashMD5(um.Senha);
                //var sql = "insert into usuario (nome, login, senha) values (@nome, @login, @senha); select convert(int, scope_identity())";
                //var parametros = new { nome = um.Nome,login = um.Login, senha = CriptoHelper.HashMD5(um.Senha) };
                //ret = ctx.Database.Connection.ExecuteScalar<int>(sql, parametros);
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
    }
}