﻿using ControleEstoque.Web.Helpers;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class UsuarioDao
    {
        public static UsuarioModel ValidarUsuario(string login, string senha)
        {
            UsuarioModel ret = null;
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = @"Data Source=DESKTOP-3GOF1VJ\SQLEXPRESS2017;Initial Catalog=controle-estoque;User Id=sa;Password=123";
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    /*
                     O trecho abaixo evita SQL Injection e Criptografa a senha
                     */
                    comando.CommandText = "select * from usuario where login=@login and senha=@senha";
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Senha = (string)reader["senha"],
                            Nome = (string)reader["nome"]


                        };
                    }
                }
            }

            return ret;
        }


        public static List<UsuarioModel> RecuperarLista(int pagina = -1, int tamPagina = -1, string filtro = "")
        {
            var ret = new List<UsuarioModel>();
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    var pos = (pagina - 1) * tamPagina;

                    var filtroWhere = "";
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
                    }


                    comando.Connection = conexao;


                    if (pagina == -1 || tamPagina == -1)
                    {

                        comando.CommandText = "select * from usuario " + filtroWhere + " order by nome";

                    }
                    else
                    {


                        comando.CommandText = string.Format(
                             "select *" +
                            " from usuario" +
                            filtroWhere +
                            " order by nome" +
                            " offset {0} rows fetch next {1} rows only",
                            pos > 0 ? pos - 1 : 0, tamPagina);

                    }


                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new UsuarioModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Login = (string)reader["login"]
                        });
                    }
                }
            }

            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            if (RecuperarPeloId(id) != null)
            {
                using (var conexao = new SqlConnection())
                {

                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from usuario where id = @id";

                        //Evitando SQL INJECTION
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        ret = (comando.ExecuteNonQuery() > 0);

                    }
                }

            }
            return ret;
        }
        public static UsuarioModel RecuperarPeloId(int id)
        {
            UsuarioModel ret = null;
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from usuario where (id = @id)";

                    //Evitando SQL INJECTION
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            Login = (string)reader["login"],
                            Senha = (string)reader["senha"]
                        };
                    }
                }
            }

            return ret;
        }

        public static int Salvar(UsuarioModel um)
        {
            var ret = 0;
            var model = RecuperarPeloId(um.Id);


            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    if (model == null)
                    {

                        comando.CommandText = "insert into usuario (nome, login, senha) values (@nome, @login, @senha); select convert(int, scope_identity())";

                        //Evitando SQL INJECTION
                        comando.Parameters.Add("@login", SqlDbType.VarChar).Value = um.Login;
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = um.Nome;
                        comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(um.Senha);



                        ret = ((int)comando.ExecuteScalar());

                    }
                    else
                    {
                        //É preciso verificar se a senha veio vazia ou não, se veio não atualiza a senha,
                        //caso venha preenchida o campo senha será atualizado
                        comando.CommandText =
                            "update usuario set nome=@nome, login=@login" +
                           (!string.IsNullOrEmpty(um.Senha) ? ", senha=@senha" : "") +
                           " where id=@id";

                        //Evitando SQL INJECTION
                        comando.Parameters.Add("@login", SqlDbType.VarChar).Value = um.Login;
                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = um.Nome;
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = um.Id;


                        if (!string.IsNullOrEmpty(um.Senha))
                        {
                            comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(um.Senha);
                        }


                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = um.Id;
                        }
                    }
                }
            }
            return ret;
        }

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {

                    comando.Connection = conexao;
                    comando.CommandText = "select count(*) from usuario";
                    ret = (int)comando.ExecuteScalar();


                }
            }

            return ret;
        }



        public static UsuarioModel RecuperarIdLogado(string login)
        {
            UsuarioModel ret = null;
            using (var conexao = new SqlConnection())
            {

                conexao.ConnectionString = @"Data Source=DESKTOP-3GOF1VJ\SQLEXPRESS2017;Initial Catalog=controle-estoque;User Id=sa;Password=123";
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    /*
                     O trecho abaixo evita SQL Injection e Criptografa a senha
                     */
                    comando.CommandText = "select * from usuario where login=@login";
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (int)reader["id"],
                            Login = (string)reader["login"],
                            Senha = (string)reader["senha"],
                            Nome = (string)reader["nome"]



                        };
                    }
                }
            }

            return ret;
        }


        public static string RecuperarStringNomePerfis(UsuarioModel um)
        {
            var ret = string.Empty;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format(
                        "select p.nome " +
                        "from perfil_usuario pu, perfil p " +
                        "where (pu.id_usuario = @id_usuario) and (pu.id_perfil = p.id) and (p.ativo = 1)");

                    comando.Parameters.Add("@id_usuario", SqlDbType.Int).Value = um.Id;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret += (ret != string.Empty ? ";" : string.Empty) + (string)reader["nome"];
                    }
                }
            }

            return ret;
        }

    }
}