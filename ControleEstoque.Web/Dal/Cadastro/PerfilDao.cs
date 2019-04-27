using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select count(*) from perfil";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<PerfilModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<PerfilModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    var pos = (pagina - 1) * tamPagina;

                    var filtroWhere = "";
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
                    }



                    comando.Connection = conexao;
                    comando.CommandText = string.Format(
                        "select *" +
                        " from perfil" +
                        filtroWhere +
                        " order by nome" +
                        " offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new PerfilModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static void CarregarUsers(PerfilModel pm)
        {

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText =
                        "select u.* " +
                        "from perfil_usuario pu, usuario u " +
                        "where (pu.id_perfil = @id_perfil) and (pu.id_usuario = u.id)";

                    comando.Parameters.Add("@id_perfil", SqlDbType.Int).Value = pm.Id;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        pm.Usuarios.Add(new UsuarioModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Login = (string)reader["login"]
                        });
                    }
                }
            }
        }


        public static List<PerfilModel> RecuperarListaAtivos()
        {
            var ret = new List<PerfilModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("select * from perfil where ativo=1 order by nome");
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new PerfilModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
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
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from perfil where (id = @id)";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new PerfilModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Ativo = (bool)reader["ativo"]
                        };
                    }
                }
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
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from perfil where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);
                    }
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
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.Transaction = transacao;

                        if (model == null)
                        {
                            comando.CommandText = "insert into perfil (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";

                            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = pm.Nome;
                            comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (pm.Ativo ? 1 : 0);

                            ret = (int)comando.ExecuteScalar();
                        }
                        else
                        {
                            comando.CommandText = "update perfil set nome=@nome, ativo=@ativo where id = @id";

                            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = pm.Nome;
                            comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (pm.Ativo ? 1 : 0);
                            comando.Parameters.Add("@id", SqlDbType.Int).Value = pm.Id;

                            if (comando.ExecuteNonQuery() > 0)
                            {
                                ret = pm.Id;
                            }
                        }
                    }
                    if (pm.Usuarios != null && pm.Usuarios.Count > 0)
                    {

                        using (var comandoExclusaoPerfilUsuario = new SqlCommand())
                        {
                            comandoExclusaoPerfilUsuario.Connection = conexao;
                            comandoExclusaoPerfilUsuario.Transaction = transacao;


                            comandoExclusaoPerfilUsuario.CommandText = "delete from perfil_usuario where (id_perfil = @id_perfil)";

                            comandoExclusaoPerfilUsuario.Parameters.Add("@id_perfil", SqlDbType.Int).Value = pm.Id;

                            comandoExclusaoPerfilUsuario.ExecuteScalar();

                        }
                        if (pm.Usuarios[0].Id != -1)
                        {
                            foreach (var usuario in pm.Usuarios)
                            {
                                using (var comandoInclusaoPerfilUsuario = new SqlCommand())
                                {
                                    comandoInclusaoPerfilUsuario.Connection = conexao;
                                    comandoInclusaoPerfilUsuario.Transaction = transacao;


                                    comandoInclusaoPerfilUsuario.CommandText = "insert into perfil_usuario (id_perfil, id_usuario) values (@id_perfil, @id_usuario)";

                                    comandoInclusaoPerfilUsuario.Parameters.Add("@id_perfil", SqlDbType.Int).Value = pm.Id;
                                    comandoInclusaoPerfilUsuario.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario.Id;

                                    comandoInclusaoPerfilUsuario.ExecuteScalar();

                                }
                            }
                        }
                    }
                    transacao.Commit();
                }

            }

            return ret;
        }

        public static UsuarioModel tagLayout(string login)
        {
            UsuarioModel ret = null;
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from usuario where (login = @login)";

                    //Evitando SQL INJECTION
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Login = (string)reader["login"],
                            Senha = (string)reader["senha"]
                        };
                    }
                }
            }

            return ret;
        }




        public static string RecuperarTagRole(string id)
        {
            //UsuarioModel u = UsuarioModel.RecuperarIdLogado(id);
            //PerfilModel p = PerfilModel.RecuperarPeloId(u.IdPerfil);
            //string tag = p.Nome;
            //return tag;
            return null;
        }





    }
}