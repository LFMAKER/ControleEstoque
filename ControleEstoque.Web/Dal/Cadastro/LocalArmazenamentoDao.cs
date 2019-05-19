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
    public class LocalArmazenamentoDao
    {

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();
            //    ret = conexao.ExecuteScalar<int>("select count(*) from local_armazenamento");
            //}

            //return ret;

            using (var ctx = new Context())
            {
                ret = ctx.LocaisArmazenamentos.Count();
            }
            return ret;

        }

        public static List<LocalArmazenamento> RecuperarLista(int pagina, int tamPagina, string filtro ="")
        {
            //var ret = new List<LocalArmazenamento>();

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var pos = (pagina - 1) * tamPagina;

            //    var sql = string.Format(
            //            "select *" +
            //            " from local_armazenamento" +
            //            " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
            //            " offset {0} rows fetch next {1} rows only",
            //            pos > 0 ? pos - 1 : 0, tamPagina);

            //    ret = conexao.Query<LocalArmazenamento>(sql).ToList();
            //}

            //return ret;
            var ret = new List<LocalArmazenamento>();

            using (var ctx = new Context())
            {

                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.LocaisArmazenamentos.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.LocaisArmazenamentos.OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }

            return ret;
        }

        public static LocalArmazenamento RecuperarPeloId(int id)
        {
            //LocalArmazenamento ret = null;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from local_armazenamento where (id = @id)";
            //    var parametros = new { id };
            //    ret = conexao.Query<LocalArmazenamento>(sql, parametros).SingleOrDefault();
            //}

            //return ret;
            using (var ctx = new Context())
            {
                return ctx.LocaisArmazenamentos.Find(id);
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            //var ret = false;

            //if (RecuperarPeloId(id) != null)
            //{
            //    using (var conexao = new SqlConnection())
            //    {
            //        conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //        conexao.Open();

            //        var sql = "delete from local_armazenamento where (id = @id)";
            //        var parametros = new { id };
            //        ret = (conexao.Execute(sql, parametros) > 0);
            //    }
            //}

            //return ret;
            var ret = false;
            var forn = new LocalArmazenamento { Id = id };
            using (var ctx = new Context())
            {
                ctx.LocaisArmazenamentos.Attach(forn);
                ctx.Entry(forn).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            return ret;
        }

        public static int Salvar(LocalArmazenamento la)
        {
            //    var ret = 0;

            //    var model = RecuperarPeloId(la.Id);

            //    using (var conexao = new SqlConnection())
            //    {
            //        conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //        conexao.Open();

            //        if (model == null)
            //        {
            //            var sql = "insert into local_armazenamento (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";
            //            var parametros = new { nome = la.Nome, ativo = (la.Ativo ? 1 : 0) };
            //            ret = conexao.ExecuteScalar<int>(sql, parametros);
            //        }
            //        else
            //        {
            //            var sql = "update local_armazenamento set nome=@nome, ativo=@ativo where id = @id";
            //            var parametros = new { id = la.Id, nome = la.Nome, ativo = (la.Ativo ? 1 : 0) };
            //            if (conexao.Execute(sql, parametros) > 0)
            //            {
            //                ret = la.Id;
            //            }
            //        }
            //    }

            //    return ret;



            var ret = 0;
            var model = RecuperarPeloId(la.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.LocaisArmazenamentos.Add(la);
                }
                else
                {

                    ctx.Entry(la).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = la.Id;
            }
            return ret;
        }
    }
}