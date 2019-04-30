﻿using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class CidadeDao
    {

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
                    comando.CommandText = "select count(*) from cidade";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<CidadeModel> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", int idEstado = 0)
        {
            var ret = new List<CidadeModel>();

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
                        filtroWhere = string.Format(" (lower(c.nome) like '%{0}%') and", filtro.ToLower());
                    }

                    if (idEstado > 0)
                    {
                        filtroWhere += string.Format(" (id_estado = {0}) and", idEstado);
                    }

                    var paginacao = "";
                    if (pagina > 0 && tamPagina > 0)
                    {
                        paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                            pos > 0 ? pos - 1 : 0, tamPagina);
                    }

                    comando.Connection = conexao;
                    comando.CommandText =
                        "select c.*, e.id_pais" +
                        " from cidade c, estado e" +
                        " where" +
                        filtroWhere +
                        " (c.id_estado = e.id)" +
                        " order by c.nome" +
                        paginacao;

                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new CidadeModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            IdEstado = (int)reader["id_estado"],
                            IdPais = (int)reader["id_pais"],
                            Ativo = (bool)reader["ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static CidadeModel RecuperarPeloId(int id)
        {
            CidadeModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select c.*, e.id_pais from cidade c, estado e where (c.id = @id) and (c.id_estado = e.id)";

                    comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new CidadeModel
                        {
                            Id = (int)reader["id"],
                            Nome = (string)reader["nome"],
                            IdEstado = (int)reader["id_estado"],
                            IdPais = (int)reader["id_pais"],
                            Ativo = (bool)reader["ativo"]
                        };
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
                        comando.CommandText = "delete from cidade where (id = @id)";

                        comando.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public static int Salvar(CidadeModel cm)
        {
            var ret = 0;

            var model = RecuperarPeloId(cm.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into cidade (nome, id_estado, ativo) values (@nome, @id_estado, @ativo); select convert(int, scope_identity())";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = cm.Nome;
                        comando.Parameters.Add("@id_estado", SqlDbType.Int).Value = cm.IdEstado;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (cm.Ativo ? 1 : 0);

                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update cidade set nome=@nome, id_estado=@id_estado, ativo=@ativo where id = @id";

                        comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = cm.Nome;
                        comando.Parameters.Add("@id_estado", SqlDbType.Int).Value = cm.IdEstado;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (cm.Ativo ? 1 : 0);
                        comando.Parameters.Add("@id", SqlDbType.Int).Value = cm.Id;

                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = cm.Id;
                        }
                    }
                }
            }

            return ret;
        }


    }
}