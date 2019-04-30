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
    public class PaisDao
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
                    comando.CommandText = "select count(*) from pais";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<PaisModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            var ret = new List<PaisModel>();

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

                    var paginacao = "";
                    if (pagina > 0 && tamPagina > 0)
                    {
                        paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                            pos > 0 ? pos - 1 : 0, tamPagina);
                    }

                    comando.Connection = conexao;
                    comando.CommandText =
                        "select *" +
                        " from pais" +
                        filtroWhere +
                        " order by nome" +
                        paginacao;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new PaisModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            NomePt = (string)reader["nome_pt"],
                            Sigla = (string)reader["sigla"],
                            Bacen = (string)reader["bacen"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static PaisModel RecuperarPeloId(int id)
        {
            PaisModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from pais where (id = @id)";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new PaisModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            NomePt = (string)reader["nome_pt"],
                            Sigla = (string)reader["sigla"],
                            Bacen = (string)reader["bacen"],
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
                        comando.CommandText = "delete from pais where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public static int Salvar(PaisModel pm)
        {
            var ret = 0;

            var model = RecuperarPeloId(pm.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into pais (nome, nome_pt, sigla, bacen, ativo) VALUES(@nome, @nome_pt, @sigla, @bacen, @ativo); select convert(int, scope_identity())";


                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = pm.Nome;
                        comando.Parameters.Add("@nome_pt", SqlDbType.VarChar).Value = pm.NomePt;
                        comando.Parameters.Add("@sigla", SqlDbType.VarChar).Value = pm.Sigla;
                        comando.Parameters.Add("@bacen", SqlDbType.VarChar).Value = pm.Bacen;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (pm.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update pais set nome=@nome, nome_pt=@nome_pt, sigla=@sigla, bacen=@bacen, ativo=@ativo where id = @id";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = pm.Id;
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = pm.Nome;
                        comando.Parameters.Add("@nome_pt", SqlDbType.VarChar).Value = pm.NomePt;
                        comando.Parameters.Add("@sigla", SqlDbType.VarChar).Value = pm.Sigla;
                        comando.Parameters.Add("@bacen", SqlDbType.VarChar).Value = pm.Bacen;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (pm.Ativo ? 1 : 0);

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = pm.Id;
                        }
                    }
                }
            }

            return ret;
        }
    }
}