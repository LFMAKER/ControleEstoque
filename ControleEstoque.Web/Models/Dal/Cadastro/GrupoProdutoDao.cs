using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;


namespace ControleEstoque.Web.Dal.Cadastro
{
    public class GrupoProdutoDao
    {

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                ret = conexao.ExecuteScalar<int>("select count(*) from grupo_produto");
            }

            return ret;
        }

        public static List<GrupoProdutoModel> RecuperarLista(int pagina, int tamPagina, string filtro = "", string ordem = "")
        {
            var ret = new List<GrupoProdutoModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var pos = (pagina - 1) * tamPagina;

                var filtroWhere = "";
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
                }

                var sql = string.Format(
                    "select *" +
                    " from grupo_produto" +
                    filtroWhere +
                    " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
                    " offset {0} rows fetch next {1} rows only",
                    pos > 0 ? pos - 1 : 0, tamPagina);

                ret = conexao.Query<GrupoProdutoModel>(sql).ToList();
            }

            return ret;
        }

        public static GrupoProdutoModel RecuperarPeloId(int id)
        {
            GrupoProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select * from grupo_produto where (id = @id)";
                var parametros = new { id };
                ret = conexao.Query<GrupoProdutoModel>(sql, parametros).SingleOrDefault();
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

                    var sql = "delete from grupo_produto where (id = @id)";
                    var parametros = new { id };
                    ret = (conexao.Execute(sql, parametros) > 0);
                }
            }

            return ret;
        }

        public static int Salvar(GrupoProdutoModel gp)
        {
            var ret = 0;

            var model = RecuperarPeloId(gp.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                if (model == null)
                {
                    var sql = "insert into grupo_produto (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";
                    var parametros = new { nome = gp.Nome, ativo = (gp.Ativo ? 1 : 0) };
                    ret = conexao.ExecuteScalar<int>(sql, parametros);
                }
                else
                {
                    var sql = "update grupo_produto set nome=@nome, ativo=@ativo where id = @id";
                    var parametros = new { id = gp.Id, nome = gp.Nome, ativo = (gp.Ativo ? 1 : 0) };
                    if (conexao.Execute(sql, parametros) > 0)
                    {
                        ret = gp.Id;
                    }
                }
            }

            return ret;
        }


    }
}