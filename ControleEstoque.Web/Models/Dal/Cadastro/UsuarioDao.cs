using ControleEstoque.Web.Helpers;
using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class UsuarioDao
    {
        public static UsuarioModel ValidarUsuario(string login, string senha)
        {
            UsuarioModel ret = null;

            using (var db = new Context())
            {
                

                var sql = "select * from usuario where login=@login and senha=@senha";
                var parametros = new { login, senha = CriptoHelper.HashMD5(senha) };
                ret = db.Database.Connection.Query<UsuarioModel>(sql, parametros).SingleOrDefault();
            }

            return ret;
        }

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                ret = conexao.ExecuteScalar<int>("select count(*) from usuario");
            }

            return ret;
        }

        public static List<UsuarioModel> RecuperarLista(int pagina = -1, int tamPagina = -1, string ordem = "")
        {
            var ret = new List<UsuarioModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                string sql;
                if (pagina == -1 || tamPagina == -1)
                {
                    sql =
                        "select *" +
                        "from usuario" +
                        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome");
                }
                else
                {
                    var pos = (pagina - 1) * tamPagina;
                    sql = string.Format(
                        "select *" +
                        " from usuario" +
                        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
                        " offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                }

                ret = conexao.Query<UsuarioModel>(sql).ToList();
            }

            return ret;
        }

        public static UsuarioModel RecuperarPeloId(int id)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select * from usuario where (id = @id)";
                var parametros = new { id };
                ret = conexao.Query<UsuarioModel>(sql, parametros).SingleOrDefault();
            }

            return ret;
        }

        public static UsuarioModel RecuperarPeloLogin(string login)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select * from usuario where (login = @login)";
                var parametros = new { login };
                ret = conexao.Query<UsuarioModel>(sql, parametros).SingleOrDefault();
            }

            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPeloId(id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();

                    var sql = "delete from usuario where (id = @id)";
                    var parametros = new { id };
                    ret = (conexao.Execute(sql, parametros) > 0);
                }
            }

            return ret;
        }

        public static int Salvar(UsuarioModel um)
        {
            var ret = 0;

            var model = RecuperarPeloId(um.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                if (model == null)
                {
                    var sql = "insert into usuario (nome, login, senha) values (@nome, @login, @senha); select convert(int, scope_identity())";
                    var parametros = new { nome = um.Nome,login = um.Login, senha = CriptoHelper.HashMD5(um.Senha) };
                    ret = conexao.ExecuteScalar<int>(sql, parametros);
                }
                else
                {
                    if (!string.IsNullOrEmpty(um.Senha))
                    {
                        var sql = "update usuario set nome=@nome, login=@login, senha=@senha where id = @id";
                        var parametros = new { id = um.Id, nome = um.Nome, login = um.Login, senha = CriptoHelper.HashMD5(um.Senha) };
                        if (conexao.Execute(sql, parametros) > 0)
                        {
                            ret = um.Id;
                        }
                    }
                    else
                    {
                        var sql = "update usuario set nome=@nome, login=@login where id = @id";
                        var parametros = new { id = um.Id, nome = um.Nome, login = um.Login };
                        if (conexao.Execute(sql, parametros) > 0)
                        {
                            ret = um.Id;
                        }
                    }
                }
            }

            return ret;
        }

        public static string RecuperarStringNomePerfis(UsuarioModel um)
        {
            var ret = string.Empty;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql =
                        "select p.nome " +
                        "from perfil_usuario pu, perfil p " +
                        "where (pu.id_usuario = @id_usuario) and (pu.id_perfil = p.id) and (p.ativo = 1)";
                
                var parametros = new { id_usuario = um.Id };
                var matriculas = conexao.Query<string>(sql, parametros).ToList();
                if (matriculas.Count > 0)
                {
                    ret = string.Join(";", matriculas);
                }
            }

            return ret;
        }

     
     
    }
}