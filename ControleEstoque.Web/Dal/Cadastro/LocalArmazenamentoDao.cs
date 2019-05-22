using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class LocalArmazenamentoDao
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            ret = ctx.Perfis.AsNoTracking().Count();
            return ret;

        }

        public static List<LocalArmazenamento> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {
            var ret = new List<LocalArmazenamento>();

            using (var ctx = new Context())
            {
                if (tamPagina != 0 && pagina != 0)
                {

                    var pos = (pagina - 1) * tamPagina;
                    if (!string.IsNullOrEmpty(filtro))
                    {

                        ret = ctx.LocaisArmazenamentos.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                    }
                    else
                    {

                        ret = ctx.LocaisArmazenamentos.AsNoTracking().OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                    }
                }
                else if (somenteAtivos)
                {
                    ret = ctx.LocaisArmazenamentos.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Ativo == true).ToList();
                }
                else
                {
                    ret = ctx.LocaisArmazenamentos.AsNoTracking().OrderBy(x => x.Nome).ToList();
                }
            }

            return ret;
        }

        public static LocalArmazenamento RecuperarPeloId(int? id)
        {
            if (id != null)
            {
                return ctx.LocaisArmazenamentos.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var existing = ctx.LocaisArmazenamentos.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.LocaisArmazenamentos.Attach(existing);
                ctx.Entry(existing).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return ret;
        }

        public static int Salvar(LocalArmazenamento la)
        {
            var ret = 0;
            var model = RecuperarPeloId(la.Id);
            if (model == null)
            {
                Cadastrar(la);
            }
            else
            {
                Alterar(la);
            }
            ret = la.Id;
            return ret;
        }

        public static bool Cadastrar(LocalArmazenamento la)
        {
            ctx.LocaisArmazenamentos.Add(la);
            ctx.SaveChanges();
            return true;
        }
        public static bool Alterar(LocalArmazenamento la)
        {
            var existing = ctx.LocaisArmazenamentos.FirstOrDefault(x => x.Id == la.Id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.LocaisArmazenamentos.Attach(la);
                ctx.Entry(la).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return true;
        }




        //public static bool AtualizarCapacidadeAtual(int quantidade, Produto p)
        //{


        //        using (var ctx = new Context())
        //    {
        //    }




        //    }
        public static int VerificarCapacidadeAtual(LocalArmazenamento la)
        {
            LocalArmazenamento localA = null;
            int capacidadeAtual = 0;
            localA = ctx.LocaisArmazenamentos.AsNoTracking().Where(x => x.Nome.Equals(la.Nome)).FirstOrDefault();
            capacidadeAtual = localA.CapacidadeAtual;
            return capacidadeAtual;

        }



    }
}