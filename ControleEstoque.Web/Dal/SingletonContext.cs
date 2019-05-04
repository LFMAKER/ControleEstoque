using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasOsorioA.DAL
{
    class SingletonContext
    {


        private static Context ctx;
        public static Context GetInstance()
        {
            //verificar se o contexto é != de null
            if (ctx == null)
            {
                ctx = new Context();
            }

            return ctx;
        }

    }
}