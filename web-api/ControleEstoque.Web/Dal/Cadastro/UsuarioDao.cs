using ControleEstoque.Web.Helpers;
using ControleEstoque.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Dal.Cadastro
{
    public class UsuarioDao
    {
        private static Context ctx = SingletonContext.GetInstance();

        /// <summary>
        /// Retorna um usuário válido ou null
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static Usuario ValidarUsuario(string login, string senha)
        {
            Usuario ret = null;
            senha = CriptoHelper.HashMD5(senha);
            ret = ctx.Usuarios.Where(x => x.Login.Equals(login) && x.Senha.Equals(senha)).FirstOrDefault();
            return ret;
        }

        public static async Task<Usuario> ValidarUsuarioAPI(string login, string senha)
        {
            Usuario ret = null;
            senha = CriptoHelper.HashMD5(senha);
            ret = await ctx.Usuarios.Include("KeyC").Include("Perfil").Where(x => x.Login.Equals(login) && x.Senha.Equals(senha)).FirstOrDefaultAsync();
            return ret;
        }

        public static int RecuperarQuantidade()
        {
            var ret = 0;
            ret = ctx.Usuarios.Count();
            return ret;

        }

        public static List<Usuario> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "")
        {
            var ret = new List<Usuario>();

            if (tamPagina != 0 && pagina != 0)
            {
                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {

                    ret = ctx.Usuarios.Include("Perfil").OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {

                    ret = ctx.Usuarios.Include("Perfil").OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
            }
            else
            {
                ret = ctx.Usuarios.Include("Perfil").OrderBy(x => x.Nome).ToList();
            }

            return ret;



        }

        public static Usuario RecuperarPeloId(int id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static Usuario RecuperarPeloLogin(string login)
        {
            return ctx.Usuarios.Where(x => x.Login.Equals(login)).FirstOrDefault();
        }


        public static bool ExcluirPeloId(int id)
        {
            var ret = false;

            if (RecuperarPeloId(id) != null)
            {
                try
                {
                    Usuario user = ctx.Usuarios.Include("Perfil").FirstOrDefault(x => x.Id == id);
                    ctx.Usuarios.Remove(user);
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

            }

            //Limpando qualquer Exception que tenha ficado gravado no Object do Entity
            //Se não limpar, caso ocorra uma excessão na exclusão, ele sempre vai ficar persistindo 
            //o erro, mesmo que o proximo objeto esteja sem nenhum problema.
            ctx.DetachAllEntities();
            return ret;
        }


        //O método Salvar deve ser construído.
        public static int Salvar(Usuario um)
        {
            var ret = 0;
            var model = RecuperarPeloId(um.Id);

            bool detachAndAtach = RealizarDetachAndAtach(um);
            if (detachAndAtach)
            {


                if (model == null)
                {
                    //Encriptando a senha
                    um.Senha = CriptoHelper.HashMD5(um.Senha);
                    Cadastrar(um);
                }
                else
                {
                    if (!string.IsNullOrEmpty(um.Senha))
                    {
                        um.Senha = CriptoHelper.HashMD5(um.Senha);
                        Alterar(um);
                    }
                    else
                    {
                        Alterar(um);
                    }

                }
            }
            ctx.SaveChanges();
            ret = um.Id;
            //}

            return ret;
        }


        public static bool Cadastrar(Usuario um)
        {

            ctx.Usuarios.Add(um);
            ctx.SaveChanges();
            return true;
        }

        public static bool Alterar(Usuario um)
        {
            var existing = ctx.Usuarios.FirstOrDefault(x => x.Id == um.Id);
            if (existing != null)
            {
                if (um.Senha == null)
                {
                    um.Senha = existing.Senha;
                }

                ctx.Entry(existing).State = EntityState.Detached;
            }

            var existingPerfil = ctx.Perfis.FirstOrDefault(x => x.Id == um.Perfil.Id);
            if (existingPerfil != null)
            {
                ctx.Entry(existingPerfil).State = EntityState.Detached;
            }



            try
            {

                ctx.Usuarios.Attach(um);
                ctx.Entry(um).State = EntityState.Modified;
                ctx.SaveChanges();
            }
#pragma warning disable 0168
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }



        public static string RecuperarStringNomePerfis(Usuario um)
        {
            var ret = string.Empty;

            //using (var ctx = new Context())
            //{
            ret = ctx.Usuarios.Where(x => x.Nome.Equals(um.Nome)).Include("Perfil").Select(x => x.Perfil.Nome).SingleOrDefault();
            //}
            return ret;
        }


        public static void SalvarUsuarioComPerfilNoModelo(Usuario um)
        {
            ctx.Usuarios.Add(um);
        }

        public static bool RealizarDetachAndAtach(Usuario pm)
        {
            try
            {
                /*Attachando para evitar duplicação, pois como estou trabalhando com o conceito
                 * AsNoTracking, pode ocorrer que o entity não encontre o objeto no contexto e duplique, 
                 * ou encontre um objeto diferente e trate o que desejamos utilizar como um novo.
                 * 
                 **/
                /*Para facilitar o debug desse método, será utilizado vários try catch*/

                //Try Perfis
                var existingPerfis = ctx.Perfis.FirstOrDefault(x => x.Id == pm.Perfil.Id);
                if (existingPerfis != null)
                {
                    ctx.Entry(existingPerfis).State = EntityState.Detached;

                    try
                    {
                        ctx.Perfis.Attach(pm.Perfil);

                    }
                    catch (System.Exception ex)
                    {
                        throw;
                    }
                }


                //return true;
            }
            catch (Exception)
            {

                throw;
            }

            return true;
        }



        public static bool VerificarLogin(Usuario p)
        {
            var result = ctx.Usuarios.FirstOrDefault(x => x.Login.Equals(p.Login));
            if (result == null)
            {
                return false;
            }

            return true;
        }
        public static bool VerificarEmail(Usuario p)
        {
            var result = ctx.Usuarios.FirstOrDefault(x => x.Email.Equals(p.Email));
            if (result == null)
            {
                return false;
            }

            return true;
        }


        public static bool VerificarNomeEmailEId(Usuario p)
        {
            var result = ctx.Usuarios.FirstOrDefault(x => x.Login.Equals(p.Login) && x.Email.Equals(p.Email) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public static bool VerificarLoginEId(Usuario p)
        {
            var result = ctx.Usuarios.FirstOrDefault(x => x.Login.Equals(p.Login) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public static bool VerificarEmailEId(Usuario p)
        {
            var result = ctx.Usuarios.FirstOrDefault(x => x.Email.Equals(p.Email) && x.Id == p.Id);
            if (result == null)
            {
                return false;
            }

            return true;
        }


        public static string GerarESalvarKey(string login)
        {

            Usuario u = ctx.Usuarios.Include("KeyC").Where(x => x.Login.Equals(login)).FirstOrDefault();
            if (u.KeyC != null)
            {
                var keyCodigo = u.KeyC.Codigo;
                var keyRecuperada = ctx.Keys.Where(x => x.Codigo.Equals(keyCodigo)).FirstOrDefault();
                keyRecuperada.Ativada = false;
                ctx.Entry(keyRecuperada).State = EntityState.Modified;
                ctx.SaveChanges();
            }

            string apiKey = Guid.NewGuid().ToString();
            KeyControle KeyObj = new KeyControle();
            KeyObj.Codigo = apiKey;
            KeyObj.CriadaEm = DateTime.Now;
            KeyObj.QuantidadeDeChamadas = 0;
            KeyObj.UltimoUso = DateTime.Now;
            KeyObj.Ativada = true;
            ctx.Keys.Add(KeyObj);
            ctx.SaveChanges();


            u.KeyC = ctx.Keys.Where(x => x.Codigo.Equals(KeyObj.Codigo)).FirstOrDefault();
            ctx.Entry(u).State = EntityState.Modified;
            ctx.SaveChanges();


            return apiKey;
        }

        public static string KeyAtual(string login)
        {
            Usuario u = ctx.Usuarios.Include("KeyC").Where(x => x.Login.Equals(login)).FirstOrDefault();
            if (u.KeyC != null)
            {
                var key = u.KeyC.Codigo;
                return key;
            }
            return "Não possui Key";


        }
        public static List<KeyControle> ListarKeysControle(){
            List<KeyControle> list = ctx.Keys.ToList();
            return list;
        }



}
}