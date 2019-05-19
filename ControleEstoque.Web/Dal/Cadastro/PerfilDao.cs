using ControleEstoque.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class PerfilDao
    {


        public static int RecuperarQuantidade()
        {
            var ret = 0;
            using (var ctx = new Context())
            {
                ret = ctx.Perfis.Count();
            }
            return ret;

        }
        //TODO: Arrumar listagem

        public static List<Perfil> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            //var ret = new List<Perfil>();

            //using (var ctx = new Context())
            //{


            //    var pos = (pagina - 1) * tamPagina;

            //    var sql = string.Format(
            //        "select *" +
            //        " from perfil" +
            //        " order by " + (!string.IsNullOrEmpty(ordem) ? ordem : "nome") +
            //        " offset {0} rows fetch next {1} rows only",
            //        pos > 0 ? pos - 1 : 0, tamPagina);

            //    ret = ctx.Database.Connection.Query<Perfil>(sql).ToList();
            //}

            //return ret;

            var ret = new List<Perfil>();

            using (var ctx = new Context())
            {

                if (tamPagina != 0 && pagina != 0)
                {


                    var pos = (pagina - 1) * tamPagina;
                    if (!string.IsNullOrEmpty(filtro))
                    {

                        ret = ctx.Perfis.OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                    }
                    else
                    {

                        ret = ctx.Perfis.OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                    }
                }else
                {
                    ret = ctx.Perfis.OrderBy(x => x.Nome).ToList();
                }
            }

            return ret;

        }

        //public static List<Usuario> CarregarUsers(Perfil pm)
        //{
        //    List<Usuario> ret;
        //    using (var ctx = new Context())
        //    {
        //        var sql =
        //            "select u.* " +
        //            "from perfil_usuario pu, usuario u " +
        //            "where (pu.id_perfil = @id_perfil) and (pu.id_usuario = u.id)";
        //        var parametros = new { id_perfil = pm.Id };
        //        /*pm.Usuarios*/ 
        //        ret = ctx.Database.Connection.Query<Usuario>(sql, parametros).ToList();


        //    }
        //    return ret;
        //}

        public static List<Perfil> RecuperarListaAtivos()
        {
            var ret = new List<Perfil>();
            using (var ctx = new Context())
            {
                ret = ctx.Perfis.Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();
            }
            return ret;
            //    using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from perfil where ativo=1 order by nome";
            //    ret = conexao.Query<Perfil>(sql).ToList();
            //}

            //return ret;
        }

        public static Perfil RecuperarPeloId(int id)
        {
            //Perfil ret = null;

            //using (var conexao = new SqlConnection())
            //{
            //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
            //    conexao.Open();

            //    var sql = "select * from perfil where (id = @id)";
            //    var parametros = new { id };
            //    ret = conexao.Query<Perfil>(sql, parametros).SingleOrDefault();
            //}

            //return ret;

            using (var ctx = new Context())
            {
                return ctx.Perfis.Find(id);
            }
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;


            if (RecuperarPeloId(id) != null)
            {
                //using (var conexao = new SqlConnection())
                //{
                //    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                //    conexao.Open();

                //    //Removendo o vinculo com o perfil_usuario para poder apagar o perfil
                //    var sql = "delete from perfil_usuario where (id_perfil = @id)";
                //    var parametros = new { id };
                //    conexao.Execute(sql, parametros);


                //    sql = "delete from perfil where (id = @id)";
                //    parametros = new { id };
                //    ret = (conexao.Execute(sql, parametros) > 0);
                //}

                using (var ctx = new Context())
                {
                    //Removendo o vínculo com o perfil_usuario para poder apagar o perfil
                    var sql = "delete from perfil_usuario where (id_perfil = @id)";
                    var parametros = new { id };
                    ctx.Database.Connection.Execute(sql, parametros);


                    sql = "delete from perfil where (id = @id)";
                    parametros = new { id };
                    ret = (ctx.Database.Connection.Execute(sql, parametros) > 0);

                    //Apagando o perfil
                }
            }
            return ret;
        }

        public static int Salvar(Perfil pm)
        {
            var ret = 0;
            var model = RecuperarPeloId(pm.Id);
            using (var ctx = new Context())
            {
                if (model == null)
                {

                    ctx.Perfis.Add(pm);
                }
                else
                {

                    ctx.Entry(pm).State = System.Data.Entity.EntityState.Modified;

                }
                ctx.SaveChanges();
                ret = pm.Id;
            }
            return ret;

        }



    }
}