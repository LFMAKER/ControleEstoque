using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using VendasOsorioA.DAL;
using System.Data.Entity;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class CidadeDao
    {
        

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            using (var ctx = new Context())
            {

                
                ret = ctx.Cidades.Count();
                
            }
            return ret;
        }

        public static List<CidadeModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", int idEstado = 0, string ordem = "")
        {
            var ret = new List<CidadeModel>();

            //Por se tratar de uma query complexa, será utilizado o Dapper em conjunto 
            //com o Entity Framework
            using (var ctx = new Context())
            {

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
                    "select c.id, c.nome, c.ativo, c.id_estado as idEstado, e.id_pais as idPais" +
                    " from cidade c, estado e" +
                    " where" +
                    filtroWhere +
                    " (c.id_estado = e.id)" +
                    " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "c.nome") +
                    paginacao;


                ret = ctx.Database.Connection.Query<CidadeModel>(sql).ToList();
            }

            return ret;
        }

        public static CidadeModel RecuperarPeloId(int id)
        {
            CidadeModel ret = null;
            using (var ctx = new Context())
            {
                ret = ctx.Cidades
                .Include(x => x.Estado)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            }
            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var cidade = new CidadeModel { Id = id };
            using (var ctx = new Context())
            {
                ctx.Cidades.Attach(cidade);
                ctx.Entry(cidade).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            return ret;
        }

        public static int Salvar(CidadeModel cm)
        {
            var ret = 0;

            var model = RecuperarPeloId(cm.Id);

            using (var ctx = new Context())
            {
                if (model == null)
                {
                    ctx.Cidades.Add(cm);
                }
                else
                {

                    ctx.Cidades.Attach(cm);
                    ctx.Entry(cm).State = EntityState.Modified;



                }


                ctx.SaveChanges();
                ret = cm.Id;


            }

           
            return ret;
        }


    }
}