using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ControleEstoque.Web.Models.Domain;

namespace ControleEstoque.Web.API.Correios
{
    public class CorreiosAPI
    {

        public static string Rastreio()
        {
            wsCorreios.AtendeClienteClient cliente = new wsCorreios.AtendeClienteClient();
            //string[] lista = { codigo };
            //lista, tipo de consulta, resultado, usuario e senha
            //usuario: ECT, senha: SRO
            string cep = "83312045";
            var resultado = cliente.consultaCEP(cep);
            //string resultado = cliente.consultaSRO(lista, "L", "T", "ECT", "SRO");
            
            return resultado.end;
        }


    }
}