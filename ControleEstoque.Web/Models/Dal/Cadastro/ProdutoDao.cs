using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models.Dal.Cadastro
{
    public class ProdutoDao
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
                    comando.CommandText = "select count(*) from produto";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<ProdutoModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", string ordem = "")
        {
            var ret = new List<ProdutoModel>();

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
                        filtroWhere = string.Format(" where (lower(nome) like '%{0}%')", filtro.ToLower());
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
                        " from produto" +
                        filtroWhere +
                        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
                        paginacao;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new ProdutoModel
                        {
                            Id = (int)reader["id"],
                            Codigo = (string)reader["codigo"],
                            Nome = (string)reader["nome"],
                            PrecoCusto = (decimal)reader["preco_custo"],
                            PrecoVenda = (decimal)reader["preco_venda"],
                            QuantEstoque = (int)reader["quant_estoque"],
                            IdUnidadeMedida = (int)reader["id_unidade_medida"],
                            IdGrupo = (int)reader["id_grupo"],
                            IdMarca = (int)reader["id_marca"],
                            IdFornecedor = (int)reader["id_fornecedor"],
                            IdLocalArmazenamento = (int)reader["id_local_armazenamento"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static ProdutoModel RecuperarPeloId(int id)
        {
            ProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from produto where (id = @id)";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new ProdutoModel
                        {
                            Id = (int)reader["id"],
                            Codigo = (string)reader["codigo"],
                            Nome = (string)reader["nome"],
                            PrecoCusto = (decimal)reader["preco_custo"],
                            PrecoVenda = (decimal)reader["preco_venda"],
                            QuantEstoque = (int)reader["quant_estoque"],
                            IdUnidadeMedida = (int)reader["id_unidade_medida"],
                            IdGrupo = (int)reader["id_grupo"],
                            IdMarca = (int)reader["id_marca"],
                            IdFornecedor = (int)reader["id_fornecedor"],
                            IdLocalArmazenamento = (int)reader["id_local_armazenamento"],
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
                        comando.CommandText = "delete from produto where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public static int Salvar(ProdutoModel pm)
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
                        comando.CommandText =
                            "insert into produto " +
                            "(codigo, nome, preco_custo, preco_venda, quant_estoque, id_unidade_medida, id_grupo, id_marca, " +
                            "id_fornecedor, id_local_armazenamento, ativo) values " +
                            "(@codigo, @nome, @preco_custo, @preco_venda, @quant_estoque, @id_unidade_medida, @id_grupo, @id_marca, " +
                            "@id_fornecedor, @id_local_armazenamento, @ativo); select convert(int, scope_identity())";

                        comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = pm.Codigo;
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = pm.Nome;
                        comando.Parameters.Add("@preco_custo", SqlDbType.Decimal).Value = pm.PrecoCusto;
                        comando.Parameters.Add("@preco_venda", SqlDbType.Decimal).Value = pm.PrecoVenda;
                        comando.Parameters.Add("@quant_estoque", SqlDbType.Int).Value = pm.QuantEstoque;
                        comando.Parameters.Add("@id_unidade_medida", SqlDbType.Int).Value = pm.IdUnidadeMedida;
                        comando.Parameters.Add("@id_grupo", SqlDbType.Int).Value = pm.IdGrupo;
                        comando.Parameters.Add("@id_marca", SqlDbType.Int).Value = pm.IdMarca;
                        comando.Parameters.Add("@id_fornecedor", SqlDbType.Int).Value = pm.IdFornecedor;
                        comando.Parameters.Add("@id_local_armazenamento", SqlDbType.Int).Value = pm.IdLocalArmazenamento;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (pm.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText =
                            "update produto set codigo=@codigo, nome=@nome, preco_custo=@preco_custo, " +
                            "preco_venda=@preco_venda, quant_estoque=@quant_estoque, id_unidade_medida=@id_unidade_medida, " +
                            "id_grupo=@id_grupo, id_marca=@id_marca, id_fornecedor=@id_fornecedor, " +
                            "id_local_armazenamento=@id_local_armazenamento, ativo=@ativo where id = @id";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = pm.Id;
                        comando.Parameters.Add("@codigo", SqlDbType.VarChar).Value = pm.Codigo;
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = pm.Nome;
                        comando.Parameters.Add("@preco_custo", SqlDbType.Decimal).Value = pm.PrecoCusto;
                        comando.Parameters.Add("@preco_venda", SqlDbType.Decimal).Value = pm.PrecoVenda;
                        comando.Parameters.Add("@quant_estoque", SqlDbType.Int).Value = pm.QuantEstoque;
                        comando.Parameters.Add("@id_unidade_medida", SqlDbType.Int).Value = pm.IdUnidadeMedida;
                        comando.Parameters.Add("@id_grupo", SqlDbType.Int).Value = pm.IdGrupo;
                        comando.Parameters.Add("@id_marca", SqlDbType.Int).Value = pm.IdMarca;
                        comando.Parameters.Add("@id_fornecedor", SqlDbType.Int).Value = pm.IdFornecedor;
                        comando.Parameters.Add("@id_local_armazenamento", SqlDbType.Int).Value = pm.IdLocalArmazenamento;
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
