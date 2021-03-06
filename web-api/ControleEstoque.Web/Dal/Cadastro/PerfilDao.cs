﻿using ControleEstoque.Web.Models;
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
using static Dapper.SqlMapper;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class PerfilDao
    {

        //Entity e Singleton 100% implementado, verificar métodos das regras de negócios

        private static Context ctx = SingletonContext.GetInstance();

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            ret = ctx.Perfis.AsNoTracking().Count();
            return ret;

        }


        public static List<Perfil> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {

            var ret = new List<Perfil>();
            if (tamPagina != 0 && pagina != 0)
            {
                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {
                    ret = ctx.Perfis.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {
                    ret = ctx.Perfis.AsNoTracking().OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }
            else if (somenteAtivos)
            {
                ret = ctx.Perfis.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Ativo == true).ToList();
            }
            else
            {
                ret = ctx.Perfis.AsNoTracking().OrderBy(x => x.Nome).ToList();
            }

            return ret;
        }

        public static List<Perfil> RecuperarListaAtivos()
        {
            var ret = new List<Perfil>();
            ret = ctx.Perfis.AsNoTracking().Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();
            return ret;
        }

        public static Perfil RecuperarPeloId(int? id)
        {
            if(id != null)
            {
                return ctx.Perfis.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }else
            {
                return null;
            }
           
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var existing = ctx.Perfis.FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.Perfis.Attach(existing);
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

        public static int Salvar(Perfil pm)
        {
            var ret = 0;
            var model = RecuperarPeloId(pm.Id);
            if (model == null)
            {
                Cadastrar(pm);
            }
            else
            {
                Alterar(pm);
            }
            ret = pm.Id;
            return ret;

        }

        public static bool Cadastrar(Perfil pm)
        {
            ctx.Perfis.Add(pm);
            ctx.SaveChanges();
            return true;
        }
        public static bool Alterar(Perfil pm)
        {
            var existing = ctx.Perfis.FirstOrDefault(x => x.Id == pm.Id);
            if (existing != null) {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.Perfis.Attach(pm);
                ctx.Entry(pm).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            #pragma warning disable 0168
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }





        public static bool VerificarNome(Perfil p)
        {
            var result = ctx.Perfis.FirstOrDefault(x => x.Nome.Equals(p.Nome));
            if(result == null)
            {
                return false;
            }

            return true;
        }


        public static bool VerificarNomeEId(Perfil p)
        {
            var result = ctx.Perfis.FirstOrDefault(x => x.Nome.Equals(p.Nome) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }




    }
}