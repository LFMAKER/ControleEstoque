using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Models.Dal.Cadastro
{
    public class FornecedorDao
    {

        //private static Context ctx = SingletonContext.GetInstance();
        public static int RecuperarQuantidade()
        {
            var ret = 0;
            using (var ctx = new Context())
            {
                ret = ctx.Fornecedores.Count();
            }
            return ret;
        }

        public static List<Fornecedor> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            var ret = new List<Fornecedor>();

            using (var ctx = new Context())
            {

                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {
                    
                    ret = ctx.Fornecedores.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }else{

                    ret = ctx.Fornecedores.OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                } 
            }

            return ret;
        }

        public static Fornecedor RecuperarPeloId(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.Fornecedores.Find(id);
            }

        }

        public static bool ExcluirPeloId(int id)
        {
            //var ret = false;
            //using (var ctx = new Context())
            //{
            //    Fornecedor recuperado = ctx.Fornecedores.Find(id);
            //    ctx.Fornecedores.Remove(recuperado);
            //    ctx.SaveChanges();
            //    ret = true;
            //}
            //return ret;
            var ret = false;
            var forn = new Fornecedor { Id = id };
            using (var ctx = new Context())
            {
                ctx.Fornecedores.Attach(forn);
                ctx.Entry(forn).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            return ret;


        }

        public static int Salvar(Fornecedor fm)
        {
            var ret = 0;
            var model = RecuperarPeloId(fm.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.Fornecedores.Add(fm);
                }
                else
                {

                    ctx.Entry(fm).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = fm.Id;
            }
            return ret;
        }




    }
}