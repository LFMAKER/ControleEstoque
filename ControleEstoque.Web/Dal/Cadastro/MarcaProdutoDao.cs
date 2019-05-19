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
    public class MarcaProdutoDao
    {
        public static int RecuperarQuantidade()
        {
            //var ret = 0;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();
            //    ret = conexao.ExecuteScalar<int>("select count(*) from marca_produto");
            //}

            //return ret;


            var ret = 0;
            using (var ctx = new Context())
            {
                ret = ctx.MarcasProdutos.Count();
            }
            return ret;
        }

        public static List<MarcaProduto> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            //var ret = new List<MarcaProduto>();

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var pos = (pagina - 1) * tamPagina;

            //    var sql = string.Format(
            //        "select *" +
            //        " from marca_produto" +
            //        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
            //        " offset {0} rows fetch next {1} rows only",
            //        pos > 0 ? pos - 1 : 0, tamPagina);

            //    ret = conexao.Query<MarcaProduto>(sql).ToList();
            //}

            //return ret;


            var ret = new List<MarcaProduto>();

            using (var ctx = new Context())
            {

                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.MarcasProdutos.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.MarcasProdutos.OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }

            return ret;
        }

        public static MarcaProduto RecuperarPeloId(int id)
        {
            //MarcaProduto ret = null;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from marca_produto where (id = @id)";
            //    var parametros = new { id };
            //    ret = conexao.Query<MarcaProduto>(sql, parametros).SingleOrDefault();
            //}

            //return ret;

            using (var ctx = new Context())
            {
                return ctx.MarcasProdutos.Find(id);
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

            //        var sql = "delete from marca_produto where (id = @id)";
            //        var parametros = new { id };
            //        ret = (conexao.Execute(sql, parametros) > 0);
            //    }
            //}

            //return ret;

            var ret = false;
            var forn = new MarcaProduto { Id = id };
            using (var ctx = new Context())
            {
                ctx.MarcasProdutos.Attach(forn);
                ctx.Entry(forn).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            return ret;

        }

        public static int Salvar(MarcaProduto mp)
        {
            //    var ret = 0;

            //    var model = RecuperarPeloId(mp.Id);

            //    using (var conexao = new SqlConnection())
            //    {
            //        conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //        conexao.Open();

            //        if (model == null)
            //        {
            //            var sql = "insert into marca_produto (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";
            //            var parametros = new { nome = mp.Nome, ativo = (mp.Ativo ? 1 : 0) };
            //            ret = conexao.ExecuteScalar<int>(sql, parametros);
            //        }
            //        else
            //        {
            //            var sql = "update marca_produto set nome=@nome, ativo=@ativo where id = @id";
            //            var parametros = new { id = mp.Id, nome = mp.Nome, ativo = (mp.Ativo ? 1 : 0) };
            //            if (conexao.Execute(sql, parametros) > 0)
            //            {
            //                ret = mp.Id;
            //            }
            //        }
            //    }

            //    return ret;


            var ret = 0;
            var model = RecuperarPeloId(mp.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.MarcasProdutos.Add(mp);
                }
                else
                {

                    ctx.Entry(mp).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = mp.Id;
            }
            return ret;
        }
    }
}