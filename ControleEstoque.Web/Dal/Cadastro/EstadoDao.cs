using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class EstadoDao
    {
        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var ctx = new Context())
            {
                ret = ctx.Estados.Count();

            }

            return ret;
        }

        public static List<EstadoModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", int idPais = 0, string ordem = "")
        {
            //Por se tratar de uma consulta complexa
            //Será utilizado o Dapper em con
            var ret = new List<EstadoModel>();

            using (var ctx = new Context())
            {
                

                var filtroWhere = "";
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
                }

                if (idPais > 0)
                {
                    filtroWhere +=
                        (string.IsNullOrEmpty(filtroWhere) ? " where" : " and") +
                        string.Format(" id_pais = {0}", idPais);
                }

                var pos = (pagina - 1) * tamPagina;
                var paginacao = "";
                if (pagina > 0 && tamPagina > 0)
                {
                    paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                }

                var sql =
                    "select id, nome, uf, id_pais as IdPais, ativo" +
                    " from estado" +
                    filtroWhere +
                    " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
                    paginacao;

                ret = ctx.Database.Connection.Query<EstadoModel>(sql).ToList();
            }

            return ret;
        }

        public static EstadoModel RecuperarPeloId(int id)
        {
            EstadoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                var sql = "select id, nome, uf, id_pais as IdPais, ativo from estado where (id = @id)";
                var parametros = new { id };
                ret = conexao.Query<EstadoModel>(sql, parametros).SingleOrDefault();
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

                    var sql = "delete from estado where (id = @id)";
                    var parametros = new { id };
                    ret = (conexao.Execute(sql, parametros) > 0);
                }
            }

            return ret;
        }

        public static int Salvar(EstadoModel em)
        {
            var ret = 0;

            var model = RecuperarPeloId(em.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();

                if (model == null)
                {
                    var sql = "insert into estado (nome, uf, id_pais, ativo) values (@nome, @uf, @id_pais, @ativo); select convert(int, scope_identity())";
                    var parametros = new { nome = em.Nome, uf = em.UF, id_pais = em.IdPais, ativo = (em.Ativo ? 1 : 0) };
                    ret = conexao.ExecuteScalar<int>(sql, parametros);
                }
                else
                {
                    var sql = "update estado set nome=@nome, uf=@uf, id_pais=@id_pais, ativo=@ativo where id = @id";
                    var parametros = new { id = em.Id, nome = em.Nome, uf = em.UF, id_pais = em.IdPais, ativo = (em.Ativo ? 1 : 0) };
                    if (conexao.Execute(sql, parametros) > 0)
                    {
                        ret = em.Id;
                    }
                }
            }

            return ret;
        }
    }
} 

