using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class Log
    {

        public string Uuario { get; set; }
        public string Operacao { get; set; }
        public string Ip { get; set; }
        public string MacAddress { get; set; }
        public string DataOperacao { get; set; }
        public string Criticidade{ get; set; }   




        public static string IpUsuario()
        {

            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];

        }

        public static string MacAddressUsuario()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up)
                .Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();
        }

    }
}