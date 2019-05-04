using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class PerfilDao
    {


        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                ret = conexao.ExecuteScalar<int>("select count(*) from perfil");
            }

            return ret;
        }

        public static List<PerfilModel> RecuperarLista(int pagina, int tamPagina, string ordem = "")
        {
            var ret = new List<PerfilModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var pos = (pagina - 1) * tamPagina;

                var sql = string.Format(
                    "select *" +
                    " from perfil" +
                    " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
                    " offset {0} rows fetch next {1} rows only",
                    pos > 0 ? pos - 1 : 0, tamPagina);

                ret = conexao.Query<PerfilModel>(sql).ToList();
            }

            return ret;
        }

        public static void CarregarUsers(PerfilModel pm)
        {

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql =
                    "select u.* " +
                    "from perfil_usuario pu, usuario u " +
                    "where (pu.id_perfil = @id_perfil) and (pu.id_usuario = u.id)";
                var parametros = new { id_perfil = pm.Id };
                pm.Usuarios = conexao.Query<UsuarioModel>(sql, parametros).ToList();
            }
        }

        public static List<PerfilModel> RecuperarListaAtivos()
        {
            var ret = new List<PerfilModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select * from perfil where ativo=1 order by nome";
                ret = conexao.Query<PerfilModel>(sql).ToList();
            }

            return ret;
        }

        public static PerfilModel RecuperarPeloId(int id)
        {
            PerfilModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select * from perfil where (id = @id)";
                var parametros = new { id };
                ret = conexao.Query<PerfilModel>(sql, parametros).SingleOrDefault();
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

                    //Removendo o vinculo com o perfil_usuario para poder apagar o perfil
                    var sql = "delete from perfil_usuario where (id_perfil = @id)";
                    var parametros = new { id };
                    conexao.Execute(sql, parametros);


                    sql = "delete from perfil where (id = @id)";
                    parametros = new { id };
                    ret = (conexao.Execute(sql, parametros) > 0);

                  


                }
            }

            return ret;
        }

        public static int Salvar(PerfilModel pm)
        {
            var ret = 0;

            var model = RecuperarPeloId(pm.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                using (var transacao = conexao.BeginTransaction())
                {
                    if (model == null)
                    {
                        var sql = "insert into perfil (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";
                        var parametros = new { nome = pm.Nome, ativo = (pm.Ativo ? 1 : 0) };
                        ret = conexao.ExecuteScalar<int>(sql, parametros, transacao);
                        pm.Id = ret;
                    }
                    else
                    {
                        var sql = "update perfil set nome=@nome, ativo=@ativo where id = @id";
                        var parametros = new { id = pm.Id, nome = pm.Nome, ativo = (pm.Ativo ? 1 : 0) };
                        if (conexao.Execute(sql, parametros, transacao) > 0)
                        {
                            ret = pm.Id;
                        }
                    }

                    if (pm.Usuarios != null && pm.Usuarios.Count > 0)
                    {
                        var sql = "delete from perfil_usuario where (id_perfil = @id_perfil)";
                        var parametros = new { id_perfil = pm.Id };
                        conexao.Execute(sql, parametros, transacao);

                        if (pm.Usuarios[0].Id != -1)
                        {
                            foreach (var usuario in pm.Usuarios)
                            {
                                sql = "insert into perfil_usuario (id_perfil, id_usuario) values (@id_perfil, @id_usuario)";
                                var parametrosUsuario = new { id_perfil = pm.Id, id_usuario = usuario.Id };
                                conexao.Execute(sql, parametrosUsuario, transacao);
                            }
                        }
                    }

                    transacao.Commit();
                }
            }

            return ret;
        }



    }
}