using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class UnidadeMedidaDao
    {

        public static int RecuperarQuantidade()
        {
            //var ret = 0;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    ret = conexao.ExecuteScalar<int>("select count(*) from unidade_medida");
            //}

            //return ret;
            var ret = 0;
            using (var ctx = new Context())
            {
                ret = ctx.UnidadesMedida.Count();
            }
            return ret;
        }

        public static List<UnidadeMedida> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            //var ret = new List<UnidadeMedida>();

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var pos = (pagina - 1) * tamPagina;

            //    var sql = string.Format(
            //        "select *" +
            //        " from unidade_medida" +
            //        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
            //        " offset {0} rows fetch next {1} rows only",
            //        pos > 0 ? pos - 1 : 0, tamPagina);

            //    ret = conexao.Query<UnidadeMedida>(sql).ToList();
            //}

            //return ret;

            var ret = new List<UnidadeMedida>();

            using (var ctx = new Context())
            {

                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.UnidadesMedida.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.UnidadesMedida.OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }

            return ret;
        }

        public static UnidadeMedida RecuperarPeloId(int id)
        {
            //UnidadeMedida ret = null;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from unidade_medida where (id = @id)";
            //    var parametros = new { id };
            //    ret = conexao.Query<UnidadeMedida>(sql, parametros).SingleOrDefault();
            //}

            //return ret;


            using (var ctx = new Context())
            {
                return ctx.UnidadesMedida.Find(id);
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            //    var ret = false;

            //    if (RecuperarPeloId(id) != null)
            //    {
            //        using (var conexao = new SqlConnection())
            //        {
            //            conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //            conexao.Open();

            //            var sql = "delete from unidade_medida where (id = @id)";
            //            var parametros = new { id };
            //            ret = (conexao.Execute(sql, parametros) > 0);
            //        }
            //    }

            //    return ret;


            var ret = false;
            var unidade = new UnidadeMedida { Id = id };
            using (var ctx = new Context())
            {
                ctx.UnidadesMedida.Attach(unidade);
                ctx.Entry(unidade).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            return ret;
        }


        public static int Salvar(UnidadeMedida um)
        {
            //var ret = 0;

            //var model = RecuperarPeloId(um.Id);

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    if (model == null)
            //    {
            //        var sql = "insert into unidade_medida (nome, sigla, ativo) values (@nome, @sigla, @ativo); select convert(int, scope_identity())";
            //        var parametros = new { nome = um.Nome, sigla = um.Sigla, ativo = (um.Ativo ? 1 : 0) };
            //        ret = conexao.ExecuteScalar<int>(sql, parametros);
            //    }
            //    else
            //    {
            //        var sql = "update unidade_medida set nome=@nome, sigla=@sigla, ativo=@ativo where id = @id";
            //        var parametros = new { id = um.Id, nome = um.Nome, sigla = um.Sigla, ativo = (um.Ativo ? 1 : 0) };
            //        if (conexao.Execute(sql, parametros) > 0)
            //        {
            //            ret = um.Id;
            //        }
            //    }
            //}

            //return ret;



            var ret = 0;
            var model = RecuperarPeloId(um.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.UnidadesMedida.Add(um);
                }
                else
                {

                    ctx.Entry(um).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = um.Id;
            }
            return ret;
        }

    }
}