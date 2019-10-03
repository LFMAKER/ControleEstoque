using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace ControleEstoque.Web.Controllers.APIExport
{
    [RoutePrefix("api")]
    public class APIExportInventoryController : ApiController
    {
        

        [HttpPost]
        [Route("Fornecedores")]
        [AllowAnonymous]
        public IHttpActionResult GetFornecedores(string key)
        {

            var listaKeys = UsuarioDao.ListarKeysControle();
            bool autenticado = false;
            foreach (var ikey in listaKeys)
            {
                if (key.Equals(ikey.Codigo))
                {
                    autenticado = true;
                }
            }

            if (autenticado)
            {
                List<Fornecedor> lista = FornecedorDao.RecuperarLista();
                if (lista != null)
                {
                    return Ok(lista);
                }
            }

            return Unauthorized();

        }

        [Route("UnidadesMedida")]
        public IHttpActionResult GetUnidadesMedida()
        {
            List<UnidadeMedida> lista = UnidadeMedidaDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }

            return NotFound();


        }

        [Route("GruposProduto")]
        public IHttpActionResult GetGruposProduto()
        {
            List<GrupoProduto> lista = GrupoProdutoDao.RecuperarLista();

            if (lista != null)
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [Route("LocaisArmazenamento")]
        public IHttpActionResult GetLocaisArmazenamento()
        {
            List<LocalArmazenamento> lista = LocalArmazenamentoDao.RecuperarLista();

            if (lista != null)
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [Route("MarcasProduto")]
        public IHttpActionResult GetMarcasProduto()
        {
            List<MarcaProduto> lista = MarcaProdutoDao.RecuperarLista();

            if (lista != null)
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [Route("Perfis")]
        public IHttpActionResult GetPerfils()
        {
            List<Perfil> lista = PerfilDao.RecuperarLista();


            if (lista != null)
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [Route("Produtos")]
        public IHttpActionResult GetProdutos()
        {
            List<Produto> lista = ProdutoDao.RecuperarLista();

            if (lista != null)
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [Route("Usuarios")]
        public IHttpActionResult GetUsuarios()
        {

            List<Usuario> lista = UsuarioDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }



        [HttpPost]
        [Route("Autenticar")]
        public IHttpActionResult Autenticar(Usuario user)
        {

            var ret = UsuarioDao.ValidarUsuario(user.Login, user.Senha);
            return Ok(ret);

        }
    }
}
