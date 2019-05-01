using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class UnidadeMedidaDao
    {

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                ret = conexao.ExecuteScalar<int>("select count(*) from unidade_medida");
            }

            return ret;
        }

        public static List<UnidadeMedidaModel> RecuperarLista(int pagina, int tamPagina, string ordem = "")
        {
            var ret = new List<UnidadeMedidaModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var pos = (pagina - 1) * tamPagina;

                var sql = string.Format(
                    "select *" +
                    " from unidade_medida" +
                    " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
                    " offset {0} rows fetch next {1} rows only",
                    pos > 0 ? pos - 1 : 0, tamPagina);

                ret = conexao.Query<UnidadeMedidaModel>(sql).ToList();
            }

            return ret;
        }

        public static UnidadeMedidaModel RecuperarPeloId(int id)
        {
            UnidadeMedidaModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select * from unidade_medida where (id = @id)";
                var parametros = new { id };
                ret = conexao.Query<UnidadeMedidaModel>(sql, parametros).SingleOrDefault();
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

                    var sql = "delete from unidade_medida where (id = @id)";
                    var parametros = new { id };
                    ret = (conexao.Execute(sql, parametros) > 0);
                }
            }

            return ret;
        }

        public static int Salvar(UnidadeMedidaModel um)
        {
            var ret = 0;

            var model = RecuperarPeloId(um.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                if (model == null)
                {
                    var sql = "insert into unidade_medida (nome, sigla, ativo) values (@nome, @sigla, @ativo); select convert(int, scope_identity())";
                    var parametros = new { nome = um.Nome, sigla = um.Sigla, ativo = (um.Ativo ? 1 : 0) };
                    ret = conexao.ExecuteScalar<int>(sql, parametros);
                }
                else
                {
                    var sql = "update unidade_medida set nome=@nome, sigla=@sigla, ativo=@ativo where id = @id";
                    var parametros = new { id = um.Id, nome = um.Nome, sigla = um.Sigla, ativo = (um.Ativo ? 1 : 0) };
                    if (conexao.Execute(sql, parametros) > 0)
                    {
                        ret = um.Id;
                    }
                }
            }

            return ret;
        }

    }
}