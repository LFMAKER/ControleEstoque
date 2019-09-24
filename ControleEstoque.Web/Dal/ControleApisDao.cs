using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dal
{
    public class ControleApisDao
    {


        private static Context ctx = SingletonContext.GetInstance();
        public static bool VerificarStatusApiPorNome(string NomeApi)
        {

            var ret = ctx.ControleApis.Where(x => x.Nome.Equals(NomeApi)).FirstOrDefault();
            var status = ret.Status;
            return status;


        }



    }
}