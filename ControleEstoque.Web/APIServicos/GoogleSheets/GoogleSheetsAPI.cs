using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ControleEstoque.Web.Models;
using System.Web;
using System.IO;
using static Google.Apis.Sheets.v4.SheetsService;
using Newtonsoft.Json;


namespace ControleEstoque.Web.APIServicos.GoogleSheets
{
    

    public class GoogleSheetsAPI
    {
        /*
         Desenvolvido por Leonardo Oliveira
         Função: Listar e Gravar dados em uma planilha especifica.
         V1: Construção da Classe com seus devidos métodos - 04/05/2019
         Falta fazer: Refatorar o código;
         Observação: Por motivos críticos de segurança, a credentials não deve ser subida para o Github.
         */


        static string[] Scopes = { SheetsService.Scope.Spreadsheets  };
        static string ApplicationName = "Inventory Analytics";
        static string[] testeScope = { SheetsService.Scope.Spreadsheets };
        public static UserCredential credential;
        
        public static bool AutenticarGoogle(string userLogado = null)
        {
            

            bool result = false;
            //Credencial do Desenvolvedor - Isso não está relacionado ao usuário do sistema
            var File = System.Web.Hosting.HostingEnvironment.MapPath("~/credentials.json");

            //Autenticando no Google API
            using (var stream =
                new FileStream(File, FileMode.Open, FileAccess.Read))
            {
                //Diferente da credentials, o token está relacionado ao usuário logado no sistema
                string tokenUser = "token" + userLogado + ".json";

                string caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/APIServicos/GoogleSheets/TokensUsers"), tokenUser);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(caminho, true)).Result;
                //Console.WriteLine("Credential file saved to: " + caminho);
                result = true;
            }

            return result;
        }


        public static List<Log> RequestLogsListar(string userLogado = null)
        {
            List<Log> LogsFounded = new List<Log>();

            //Autenticando
            if (AutenticarGoogle(userLogado))
            {

            //var File = System.Web.Hosting.HostingEnvironment.MapPath("~/credentials.json");

            ////Autenticando no Google API
            //using (var stream =
            //    new FileStream(File, FileMode.Open, FileAccess.Read))
            //{
            //    string caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/API/GoogleSheets/"), "token.json");
            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        Scopes,
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore(caminho, true)).Result;
            //    Console.WriteLine("Credential file saved to: " + caminho);
            //}

            // Criando Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1GpvsT6-nEdueW-uFnWI6h7Gt38Bh4fjbcrVaDkqnUWs";
            String range = "Class Data!A2:F";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

                ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {

                foreach (var row in values)
                {
                    //Cria um usuário Log e adiciona os values do response, após adiciona em uma List<Log>
                    Log r = new Log
                    {
                        Uuario = (string)row[0],
                        Operacao = (string)row[1],
                        Ip = (string)row[2],
                        MacAddress = (string)row[3],
                        Criticidade = (string)row[4],
                        DataOperacao = (string)row[5]

                    };
                    LogsFounded.Add(r);//Adicionando os resultado na Lista

                }
            }
            else
            {
                LogsFounded = null;//O response.Values está vazio, logo a lista será null
            }

            return LogsFounded;//Retornando uma List<Log> com todos os resultados ou null caso vazio.
            }else
            {
                LogsFounded = null;
                return LogsFounded;
            }



        }
        public static void RequestLogsGravar(List<IList<object>> data)
        {
            UserCredential credential;

            //Localizando o arquivo credentials.json, necessário para autenticar
            //e definindo ele como uma variavel do hosting, para permitir o uso no FileStream.
            var File = System.Web.Hosting.HostingEnvironment.MapPath("~/credentials.json");

            //Autenticando no Google API
            using (var stream =
                new FileStream(File, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }


            // Criando um Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            string spreadsheetId = "1GpvsT6-nEdueW-uFnWI6h7Gt38Bh4fjbcrVaDkqnUWs";
            string range = "Class Data!A2:F";
            
            //Montando ValueRange
            var dataValueRange = new ValueRange();
            dataValueRange.Range = range;
            dataValueRange.Values = data;


            var request = service.Spreadsheets.Values.Append(dataValueRange, spreadsheetId, range);
            //Montando InsertDataOption e ValueInputOption
            request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
         
            //Executando a API
            AppendValuesResponse response = request.Execute();

            /*
             A API Permite retornar o response em JSON
             */

        }


    }
}