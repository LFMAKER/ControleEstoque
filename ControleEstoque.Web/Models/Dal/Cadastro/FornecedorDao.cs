using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models.Dal.Cadastro
{
    public class FornecedorDao
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
                    comando.CommandText = "select count(*) from fornecedor";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<FornecedorModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", string ordem = "")
        {
            var ret = new List<FornecedorModel>();

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
                        " from fornecedor" +
                        filtroWhere +
                        " order by nome" +
                        paginacao;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new FornecedorModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            RazaoSocial = (string)reader["razao_social"],
                            NumDocumento = (string)reader["num_documento"],
                            Tipo = (TipoPessoa)((int)reader["tipo"]),
                            Telefone = (string)reader["telefone"],
                            Contato = (string)reader["contato"],
                            Logradouro = (string)reader["logradouro"],
                            Numero = (string)reader["numero"],
                            Complemento = (string)reader["complemento"],
                            Cep = (string)reader["cep"],
                            IdPais = (int)reader["id_pais"],
                            IdEstado = (int)reader["id_estado"],
                            IdCidade = (int)reader["id_cidade"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static FornecedorModel RecuperarPeloId(int id)
        {
            FornecedorModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from fornecedor where (id = @id)";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new FornecedorModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            RazaoSocial = (string)reader["razao_social"],
                            NumDocumento = (string)reader["num_documento"],
                            Tipo = (TipoPessoa)((int)reader["tipo"]),
                            Telefone = (string)reader["telefone"],
                            Contato = (string)reader["contato"],
                            Logradouro = (string)reader["logradouro"],
                            Numero = (string)reader["numero"],
                            Complemento = (string)reader["complemento"],
                            Cep = (string)reader["cep"],
                            IdPais = (int)reader["id_pais"],
                            IdEstado = (int)reader["id_estado"],
                            IdCidade = (int)reader["id_cidade"],
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
                        comando.CommandText = "delete from fornecedor where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public static int Salvar(FornecedorModel fm)
        {
            var ret = 0;

            var model = RecuperarPeloId(fm.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into fornecedor (nome, razao_social, num_documento, tipo, telefone, contato, logradouro," +
                            " numero, complemento, cep, id_pais, id_estado, id_cidade, ativo) values (@nome, @razao_social, @num_documento," +
                            " @tipo, @telefone, @contato, @logradouro, @numero, @complemento, @cep, @id_pais, @id_estado, @id_cidade, @ativo);" +
                            " select convert(int, scope_identity())";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = fm.Nome;
                        comando.Parameters.Add("@razao_social", SqlDbType.VarChar).Value = fm.RazaoSocial ?? "";
                        comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = fm.NumDocumento ?? "";
                        comando.Parameters.Add("@tipo", SqlDbType.Int).Value = fm.Tipo;
                        comando.Parameters.Add("@telefone", SqlDbType.VarChar).Value = fm.Telefone ?? "";
                        comando.Parameters.Add("@contato", SqlDbType.VarChar).Value = fm.Contato ?? "";
                        comando.Parameters.Add("@logradouro", SqlDbType.VarChar).Value = fm.Logradouro ?? "";
                        comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = fm.Numero ?? "";
                        comando.Parameters.Add("@complemento", SqlDbType.VarChar).Value = fm.Complemento ?? "";
                        comando.Parameters.Add("@cep", SqlDbType.VarChar).Value = fm.Cep ?? "";
                        comando.Parameters.Add("@id_pais", SqlDbType.Int).Value = fm.IdPais;
                        comando.Parameters.Add("@id_estado", SqlDbType.Int).Value = fm.IdEstado;
                        comando.Parameters.Add("@id_cidade", SqlDbType.Int).Value = fm.IdCidade;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (fm.Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update fornecedor set nome=@nome, razao_social=@razao_social, num_documento=@num_documento," +
                            " tipo=@tipo, telefone=@telefone, contato=@contato, logradouro=@logradouro, numero=@numero, complemento=@complemento," +
                            " cep=@cep, id_pais=@id_pais, id_estado=@id_estado, id_cidade=@id_cidade, ativo=@ativo where id = @id";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = fm.Nome;
                        comando.Parameters.Add("@razao_social", SqlDbType.VarChar).Value = fm.RazaoSocial ?? "";
                        comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = fm.NumDocumento ?? "";
                        comando.Parameters.Add("@tipo", SqlDbType.Int).Value = fm.Tipo;
                        comando.Parameters.Add("@telefone", SqlDbType.VarChar).Value = fm.Telefone ?? "";
                        comando.Parameters.Add("@contato", SqlDbType.VarChar).Value = fm.Contato ?? "";
                        comando.Parameters.Add("@logradouro", SqlDbType.VarChar).Value = fm.Logradouro ?? "";
                        comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = fm.Numero ?? "";
                        comando.Parameters.Add("@complemento", SqlDbType.VarChar).Value = fm.Complemento ?? "";
                        comando.Parameters.Add("@cep", SqlDbType.VarChar).Value = fm.Cep ?? "";
                        comando.Parameters.Add("@id_pais", SqlDbType.Int).Value = fm.IdPais;
                        comando.Parameters.Add("@id_estado", SqlDbType.Int).Value = fm.IdEstado;
                        comando.Parameters.Add("@id_cidade", SqlDbType.Int).Value = fm.IdCidade;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (fm.Ativo ? 1 : 0);
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = fm.Id;

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = fm.Id;
                        }
                    }
                }
            }

            return ret;
        }
    }
}