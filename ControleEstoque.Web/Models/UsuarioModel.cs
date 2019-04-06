using ControleEstoque.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
    public class UsuarioModel
    {

        public static bool ValidarUsuario(string login, string senha)
        {
            var ret = false;
            using (var conexao = new SqlConnection())
            {
                
                conexao.ConnectionString = @"Data Source=DESKTOP-3GOF1VJ\SQLEXPRESS2017;Initial Catalog=controle-estoque;User Id=sa;Password=123";
                conexao.Open();
                using(var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    /*
                     O trecho abaixo evita SQL Injection e Criptografa a senha
                     */                         
                    comando.CommandText = "select count(*) from usuario where login=@login and senha=@senha";
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);

                    ret = ((int)comando.ExecuteScalar() > 0);
                }
            }

            return ret;
        }

      
    }
}