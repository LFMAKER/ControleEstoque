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
    public class UnidadeMedidaDao
    {
        //Entity e Singleton 100% implementado, verificar métodos das regras de negócios
        private static Context ctx = SingletonContext.GetInstance();

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            ret = ctx.UnidadesMedida.AsNoTracking().Count();
            return ret;
        }

        public static List<UnidadeMedida> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {
            //Implementar recuperar ativos
            var ret = new List<UnidadeMedida>();
            if (tamPagina != 0 && pagina != 0)
            {

                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.UnidadesMedida.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.UnidadesMedida.AsNoTracking().OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }
            else if (somenteAtivos)
            {
                ret = ctx.UnidadesMedida.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Ativo == true).ToList();
            }
            else
            {
                ret = ctx.UnidadesMedida.AsNoTracking().OrderBy(x => x.Nome).ToList();
            }

            return ret;
        }

        public static UnidadeMedida RecuperarPeloId(int? id)
        {
            if (id != null)
            {
                return ctx.UnidadesMedida.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var existing = ctx.UnidadesMedida.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.UnidadesMedida.Attach(existing);
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


        public static int Salvar(UnidadeMedida um)
        {
            var ret = 0;
            var model = RecuperarPeloId(um.Id);
            if (model == null)
            {
                Cadastrar(um);
            }
            else
            {
                Alterar(um);
            }
            ret = um.Id;
            return ret;
        }

        public static bool Cadastrar(UnidadeMedida um)
        {
            ctx.UnidadesMedida.Add(um);
            ctx.SaveChanges();
            return true;
        }
        public static bool Alterar(UnidadeMedida um)
        {
            var existing = ctx.UnidadesMedida.FirstOrDefault(x => x.Id == um.Id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.UnidadesMedida.Attach(um);
                ctx.Entry(um).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }


        public static bool VerificarNome(UnidadeMedida um)
        {
            var result = ctx.UnidadesMedida.FirstOrDefault(x => x.Nome.Equals(um.Nome));
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public static bool VerificarSigla(UnidadeMedida p)
        {
            var result = ctx.UnidadesMedida.FirstOrDefault(x => x.Sigla.Equals(p.Sigla));
            if (result == null)
            {
                return false;
            }

            return true;
        }


        public static bool VerificarNomeSiglaEId(UnidadeMedida p)
        {
            var result = ctx.UnidadesMedida.FirstOrDefault(x => x.Nome.Equals(p.Nome) && x.Sigla.Equals(p.Sigla) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }


        public static bool VerificarNomeEId(UnidadeMedida p)
        {
            var result = ctx.UnidadesMedida.FirstOrDefault(x => x.Nome.Equals(p.Nome) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public static bool VerificarSiglaEId(UnidadeMedida p)
        {
            var result = ctx.UnidadesMedida.FirstOrDefault(x => x.Sigla.Equals(p.Sigla) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }


    }
}