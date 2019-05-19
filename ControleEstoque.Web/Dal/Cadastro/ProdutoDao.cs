using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Models.Dal.Cadastro
{
    public class ProdutoDao
    {
        public static int RecuperarQuantidade()
        {
            //var ret = 0;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    ret = conexao.ExecuteScalar<int>("select count(*) from produto");
            //}

            //return ret;


            var ret = 0;
            using (var ctx = new Context())
            {
                ret = ctx.Produtos.Count();
            }
            return ret;
        }

        public static List<Produto> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {
            var ret = new List<Produto>();

            using (var ctx = new Context())
            {

                var filtroWhere = "";
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtroWhere = string.Format("where (lower(nome) like '%{0}%') ", filtro.ToLower());
                }

                if (somenteAtivos)
                {
                    filtroWhere = (string.IsNullOrEmpty(filtroWhere) ? "where" : "and ") + "(ativo = 1) ";
                }

                var pos = (pagina - 1) * tamPagina;
                var paginacao = "";
                if (pagina > 0 && tamPagina > 0)
                {
                    paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                }

                var sql =
                    "select id, codigo, nome, ativo, preco_custo as PrecoCusto, preco_venda as PrecoVenda, " +
                    "quant_estoque as QuantEstoque, id_unidade_medida as IdUnidadeMedida, id_grupo as IdGrupo, " +
                    "id_marca as IdMarca, id_fornecedor as IdFornecedor, id_local_armazenamento as IdLocalArmazenamento " +
                    "from produto " +
                    filtroWhere +
                    paginacao;

                ret = ctx.Database.Connection.Query<Produto>(sql).ToList();
            }

            return ret;
        }

        public static Produto RecuperarPeloId(int id)
        {
            Produto ret = null;

            using (var ctx = new Context())
            {

                var sql =
                    "select id, codigo, nome, ativo, preco_custo as PrecoCusto, preco_venda as PrecoVenda, " +
                    "quant_estoque as QuantEstoque, id_unidade_medida as IdUnidadeMedida, id_grupo as IdGrupo, " +
                    "id_marca as IdMarca, id_fornecedor as IdFornecedor, id_local_armazenamento as IdLocalArmazenamento " +
                    "from produto " +
                    "where (id = @id)";
                var parametros = new { id };
                ret = ctx.Database.Connection.Query<Produto>(sql, parametros).SingleOrDefault();
            }

            return ret;
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

                //    var sql = "delete from produto where (id = @id)";
                //    var parametros = new { id };
                //    ret = (conexao.Execute(sql, parametros) > 0);
                //}


                var forn = new Produto { Id = id };
                using (var ctx = new Context())
                {
                    ctx.Produtos.Attach(forn);
                    ctx.Entry(forn).State = EntityState.Deleted;
                    ctx.SaveChanges();
                    ret = true;
                }
            }

            return ret;
        }

        public static int Salvar(Produto pm)
        {
            //var ret = 0;

            //var model = RecuperarPeloId(pm.Id);

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    if (model == null)
            //    {
            //        var sql =
            //            "insert into produto " +
            //            "(codigo, nome, preco_custo, preco_venda, quant_estoque, id_unidade_medida, id_grupo, id_marca, " +
            //            "id_fornecedor, id_local_armazenamento, ativo) values " +
            //            "(@codigo, @nome, @preco_custo, @preco_venda, @quant_estoque, @id_unidade_medida, @id_grupo, @id_marca, " +
            //            "@id_fornecedor, @id_local_armazenamento, @ativo); select convert(int, scope_identity())";
            //        var parametros = new
            //        {
            //            codigo = pm.Codigo,
            //            nome = pm.Nome,
            //            preco_custo = pm.PrecoCusto,
            //            preco_venda = pm.PrecoVenda,
            //            quant_estoque = pm.QuantEstoque,
            //            id_unidade_medida = pm.IdUnidadeMedida,
            //            id_grupo = pm.IdGrupo,
            //            id_marca = pm.IdMarca,
            //            id_fornecedor = pm.IdFornecedor,
            //            id_local_armazenamento = pm.IdLocalArmazenamento,
            //            ativo = (pm.Ativo ? 1 : 0)
            //        };
            //        ret = conexao.ExecuteScalar<int>(sql, parametros);
            //    }
            //    else
            //    {
            //        var sql =
            //            "update produto set codigo=@codigo, nome=@nome, preco_custo=@preco_custo, " +
            //            "preco_venda=@preco_venda, quant_estoque=@quant_estoque, id_unidade_medida=@id_unidade_medida, " +
            //            "id_grupo=@id_grupo, id_marca=@id_marca, id_fornecedor=@id_fornecedor, " +
            //            "id_local_armazenamento=@id_local_armazenamento, ativo=@ativo where id = @id";
            //        var parametros = new
            //        {
            //            codigo = pm.Codigo,
            //            nome = pm.Nome,
            //            preco_custo = pm.PrecoCusto,
            //            preco_venda = pm.PrecoVenda,
            //            quant_estoque = pm.QuantEstoque,
            //            id_unidade_medida = pm.IdUnidadeMedida,
            //            id_grupo = pm.IdGrupo,
            //            id_marca = pm.IdMarca,
            //            id_fornecedor = pm.IdFornecedor,
            //            id_local_armazenamento = pm.IdLocalArmazenamento,
            //            ativo = (pm.Ativo ? 1 : 0),
            //            id = pm.Id
            //        };
            //        if (conexao.Execute(sql, parametros) > 0)
            //        {
            //            ret = pm.Id;
            //        }
            //    }
            //}

            //return ret;

            var ret = 0;
            var model = RecuperarPeloId(pm.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.Produtos.Add(pm);
                }
                else
                {

                    ctx.Entry(pm).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = pm.Id;
            }
            return ret;
        }

        public static string SalvarPedidoEntrada(DateTime data, Dictionary<int, int> produtos)
        {
            return SalvarPedido(data, produtos, "entrada_produto", true);
        }

        public static string SalvarPedidoSaida(DateTime data, Dictionary<int, int> produtos)
        {
            return SalvarPedido(data, produtos, "saida_produto", false);
        }

        public static string SalvarPedido(DateTime data, Dictionary<int, int> produtos, string nomeTabela, bool entrada)
        {
            //var ret = "";

            //try
            //{
            //    using (var conexao = new SqlConnection())
            //    {
            //        conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //        conexao.Open();

            //        var numPedido = conexao.ExecuteScalar<int>($"select next value for sec_{nomeTabela}").ToString("D10");

            //        using (var transacao = conexao.BeginTransaction())
            //        {
            //            foreach (var produto in produtos)
            //            {
            //                var sql = $"insert into {nomeTabela} (numero, data, id_produto, quant) values (@numero, @data, @id_produto, @quant)";
            //                var parametrosInsert = new { numero = numPedido, data, id_produto = produto.Key, quant = produto.Value };
            //                conexao.Execute(sql, parametrosInsert, transacao);

            //                var sinal = (entrada ? "+" : "-");
            //                sql = $"update produto set quant_estoque = quant_estoque {sinal} @quant_estoque where (id = @id)";
            //                var parametrosUpdate = new { id = produto.Key, quant_estoque = produto.Value };
            //                conexao.Execute(sql, parametrosUpdate, transacao);
            //            }

            //            transacao.Commit();

            //            ret = numPedido;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            //return ret;
            return null;
        }

    }

}
