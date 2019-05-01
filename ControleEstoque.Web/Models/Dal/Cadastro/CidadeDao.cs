using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class CidadeDao
    {

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                ret = conexao.ExecuteScalar<int>("select count(*) from cidade");
            }

            return ret;
        }

        public static List<CidadeModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", int idEstado = 0, string ordem = "")
        {
            var ret = new List<CidadeModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var pos = (pagina - 1) * tamPagina;

                var filtroWhere = "";
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtroWhere = string.Format(" (lower(c.nome) like '%{0}%') and", filtro.ToLower());
                }

                if (idEstado > 0)
                {
                    filtroWhere += string.Format(" (id_estado = {0}) and", idEstado);
                }

                var paginacao = "";
                if (pagina > 0 && tamPagina > 0)
                {
                    paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                }

                var sql =
                    "select c.*, e.id_pais" +
                    " from cidade c, estado e" +
                    " where" +
                    filtroWhere +
                    " (c.id_estado = e.id)" +
                    " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "c.nome") +
                    paginacao;

                ret = conexao.Query<CidadeModel>(sql).ToList();
            }

            return ret;
        }

        public static CidadeModel RecuperarPeloId(int id)
        {
            CidadeModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                var sql = "select c.id, c.nome, c.ativo, c.id_estado as IdEstado, e.id_pais as IdPais from cidade c, estado e where (c.id = @id) and (c.id_estado = e.id)";
                var parametros = new { id };
                ret = conexao.Query<CidadeModel>(sql, parametros).SingleOrDefault();
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

                    var sql = "delete from cidade where (id = @id)";
                    var parametros = new { id };
                    ret = (conexao.Execute(sql, parametros) > 0);
                }
            }

            return ret;
        }

        public static int Salvar(CidadeModel cm)
        {
            var ret = 0;

            var model = RecuperarPeloId(cm.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                if (model == null)
                {
                    var sql = "insert into cidade (nome, id_estado, ativo) values (@nome, @id_estado, @ativo); select convert(int, scope_identity())";
                    var parametros = new { nome = cm.Nome, id_estado = cm.IdEstado, ativo = (cm.Ativo ? 1 : 0) };
                    ret = conexao.ExecuteScalar<int>(sql, parametros);
                }
                else
                {
                    var sql = "update cidade set nome=@nome, id_estado=@id_estado, ativo=@ativo where id = @id";
                    var parametros = new { id = cm.Id, nome = cm.Nome, id_estado = cm.IdEstado, ativo = (cm.Ativo ? 1 : 0) };
                    if (conexao.Execute(sql, parametros) > 0)
                    {
                        ret = cm.Id;
                    }
                }
            }

            return ret;
        }


    }
}