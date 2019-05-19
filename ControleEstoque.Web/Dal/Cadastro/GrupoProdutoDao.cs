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
    public class GrupoProdutoDao
    {

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            using (var ctx = new Context())
            {
                
                ret = ctx.GruposProdutos.Count();
            }
            return ret;
        }

        public static List<GrupoProduto> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            //var ret = new List<GrupoProduto>();

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var pos = (pagina - 1) * tamPagina;

            //    var filtroWhere = "";
            //    if (!string.IsNullOrEmpty(filtro))
            //    {
            //        filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
            //    }

            //    var sql = string.Format(
            //        "select *" +
            //        " from grupo_produto" +
            //        filtroWhere +
            //        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
            //        " offset {0} rows fetch next {1} rows only",
            //        pos > 0 ? pos - 1 : 0, tamPagina);

            //    ret = conexao.Query<GrupoProduto>(sql).ToList();
            //}

            //return ret;
            var ret = new List<GrupoProduto>();

            using (var ctx = new Context())
            {

                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.GruposProdutos.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.GruposProdutos.OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }

            return ret;



        }

        public static GrupoProduto RecuperarPeloId(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.GruposProdutos.Find(id);
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var forn = new GrupoProduto { Id = id };
            using (var ctx = new Context())
            {
                ctx.GruposProdutos.Attach(forn);
                ctx.Entry(forn).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            return ret;
        }

        public static int Salvar(GrupoProduto gp)
        {
            //var ret = 0;

            //var model = RecuperarPeloId(gp.Id);

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    if (model == null)
            //    {
            //        var sql = "insert into grupo_produto (nome, ativo) values (@nome, @ativo); select convert(int, scope_identity())";
            //        var parametros = new { nome = gp.Nome, ativo = (gp.Ativo ? 1 : 0) };
            //        ret = conexao.ExecuteScalar<int>(sql, parametros);
            //    }
            //    else
            //    {
            //        var sql = "update grupo_produto set nome=@nome, ativo=@ativo where id = @id";
            //        var parametros = new { id = gp.Id, nome = gp.Nome, ativo = (gp.Ativo ? 1 : 0) };
            //        if (conexao.Execute(sql, parametros) > 0)
            //        {
            //            ret = gp.Id;
            //        }
            //    }
            //}

            //return ret;

            var ret = 0;
            var model = RecuperarPeloId(gp.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.GruposProdutos.Add(gp);
                }
                else
                {

                    ctx.Entry(gp).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = gp.Id;
            }
            return ret;
        }


    }
}