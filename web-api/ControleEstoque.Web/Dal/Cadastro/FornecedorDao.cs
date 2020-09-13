using ControleEstoque.Web.Models;
using Dapper;
using System;
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
    public class FornecedorDao
    {

        private static Context ctx = SingletonContext.GetInstance();
        public static int RecuperarQuantidade()
        {
            var ret = 0;

            ret = ctx.Fornecedores.AsNoTracking().Count();
            return ret;
        }

        public static List<Fornecedor> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {
            var ret = new List<Fornecedor>();


            if (tamPagina != 0 && pagina != 0)
            {
                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.Fornecedores.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.Fornecedores.AsNoTracking().OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }
            else if (somenteAtivos)
            {
                ret = ctx.Fornecedores.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Ativo == true).ToList();
            }
            else
            {
                ret = ctx.Fornecedores.AsNoTracking().OrderBy(x => x.Nome).ToList();
            }

            return ret;
        }

        public static Fornecedor RecuperarPeloId(int? id)
        {
            if (id != null)
            {
                return ctx.Fornecedores.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return null;
            }

        }

        public static bool ExcluirPeloId(int id)
        {



            var ret = false;
            var existing = ctx.Fornecedores.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.Fornecedores.Attach(existing);
                ctx.Entry(existing).State = EntityState.Deleted;
                ctx.SaveChanges();
                ret = true;
            }
            catch (DbUpdateException)
            {
                ret = false;
            }
            catch (InvalidOperationException)
            {
                ret = false;
            }
            //Limpando qualquer Exception que tenha ficado gravado no Object do Entity
            //Se não limpar, caso ocorra uma excessão na exclusão, ele sempre vai ficar persistindo 
            //o erro, mesmo que o proximo objeto esteja sem nenhum problema.
            ctx.DetachAllEntities();
            return ret;


        }

        public static int Salvar(Fornecedor fm)
        {
            var ret = 0;
            var model = RecuperarPeloId(fm.Id);
            if (model == null)
            {
                Cadastrar(fm);
            }
            else
            {
                Alterar(fm);
            }
            ret = fm.Id;
            return ret;
        }

        public static bool Cadastrar(Fornecedor fm)
        {
            ctx.Fornecedores.Add(fm);
            ctx.SaveChanges();
            return true;
        }

        public static bool Alterar(Fornecedor fm)
        {
            var existing = ctx.Fornecedores.FirstOrDefault(x => x.Id == fm.Id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.Fornecedores.Attach(fm);
                ctx.Entry(fm).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            #pragma warning disable 0168
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }


        public static bool VerificarNumDocumento(Fornecedor p)
        {
            var result = ctx.Fornecedores.FirstOrDefault(x => x.NumDocumento.Equals(p.NumDocumento));
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public static bool VerificarNumDocumentoEId(Fornecedor p)
        {
            var result = ctx.Fornecedores.FirstOrDefault(x => x.NumDocumento.Equals(p.NumDocumento) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }











    }
}