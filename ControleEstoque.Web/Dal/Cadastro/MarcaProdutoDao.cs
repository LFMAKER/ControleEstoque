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
    public class MarcaProdutoDao
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
                    comando.CommandText = "select count(*) from marca_produto";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<MarcaProdutoModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<MarcaProdutoModel>();

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
                        " from marca_produto" +
                        filtroWhere +
                        " order by nome" +
                        " offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new MarcaProdutoModel
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

        public static MarcaProdutoModel RecuperarPeloId(int id)
        {
            MarcaProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from marca_produto where (id = @id)";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new MarcaProdutoModel
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
                        comando.CommandText = "delete from marca_produto where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public static int Salvar(MarcaProdutoModel mp)
        {
            var ret = 0;

            var model = RecuperarPeloId(mp.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into marca_produto (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = mp.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (mp.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update marca_produto set nome=@nome, ativo=@ativo where id = @id";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = mp.Nome;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (mp.Ativo ? 1 : 0);
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = mp.Id;

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = mp.Id;
                        }
                    }
                }
            }

            return ret;
        }
    }
}