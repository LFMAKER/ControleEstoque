using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class GrupoProdutoDao
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            ret = ctx.GruposProdutos.AsNoTracking().Count();
            return ret;
        }

        public static List<GrupoProduto> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {
            var ret = new List<GrupoProduto>();


            if (tamPagina != 0 && pagina != 0)
            {
                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.GruposProdutos.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.GruposProdutos.AsNoTracking().OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }
            else if (somenteAtivos)
            {
                ret = ctx.GruposProdutos.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Ativo == true).ToList();
            }
            else
            {
                ret = ctx.GruposProdutos.AsNoTracking().OrderBy(x => x.Nome).ToList();
            }
            return ret;
        }

        public static GrupoProduto RecuperarPeloId(int? id)
        {
            if (id != null)
            {
                return ctx.GruposProdutos.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var existing = ctx.GruposProdutos.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.GruposProdutos.Attach(existing);
                ctx.Entry(existing).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            catch (DbUpdateException)
            {
                ret = false;
            }
            //Limpando qualquer Exception que tenha ficado gravado no Object do Entity
            //Se não limpar, caso ocorra uma excessão na exclusão, ele sempre vai ficar persistindo 
            //o erro, mesmo que o proximo objeto esteja sem nenhum problema.
            ctx.DetachAllEntities();
            return ret;
        }

        public static int Salvar(GrupoProduto gp)
        {
            var ret = 0;
            var model = RecuperarPeloId(gp.Id);
            if (model == null)
            {
                Cadastrar(gp);
            }
            else
            {
                Alterar(gp);
            }
            ret = gp.Id;
            return ret;
        }

        public static bool Cadastrar(GrupoProduto gp)
        {
            ctx.GruposProdutos.Add(gp);
            ctx.SaveChanges();
            return true;
        }
        public static bool Alterar(GrupoProduto gp)
        {
            var existing = ctx.GruposProdutos.FirstOrDefault(x => x.Id == gp.Id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.GruposProdutos.Attach(gp);
                ctx.Entry(gp).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return true;
        }




    }
}