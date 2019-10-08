using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;

namespace ControleEstoque.Web.Controllers.APIExport
{
    [RoutePrefix("api")]
    public class APIExportInventoryController : ApiController
    {

       ///////////////////////////////////////////////////////////// FORNECEDORES
        //FORNECEDORES CREATE
        [HttpPost]
        [Route("FornecedoresCreate")]
        [AllowAnonymous]
        public IHttpActionResult FornecedoresCreate(Fornecedor fornecedor)
        {
            var ret = FornecedorDao.Cadastrar(fornecedor);
            if (ret)
            {
                return Ok(new {mensagem = "Cadastro realizado com sucesso!", body = ret });
            }

            return BadRequest("Ocorreu um error no cadastro!");
            

        }

        //FORNECEDORES LIST
        [HttpGet]
        [Route("FornecedoresList")]
        [AllowAnonymous]
        public IHttpActionResult FornecedoresList()
        {
            List<Fornecedor> lista = FornecedorDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return BadRequest("Nenhum fornecedor cadastro!");

        }

        //FORNECEDORES UPDATE
        [HttpPost]
        [Route("FornecedoresUpdate")]
        [AllowAnonymous]
        public IHttpActionResult FornecedoresUpdate(Fornecedor fornecedor)
        {
            var ret = FornecedorDao.Alterar(fornecedor);
            return Ok(ret);
        }

        //FORNECEDORES DELETE
        [HttpPost]
        [Route("FornecedoresDelete")]
        [AllowAnonymous]
        public IHttpActionResult FornecedoresDelete(Fornecedor fornecedor)
        {
            var ret = FornecedorDao.ExcluirPeloId(fornecedor.Id);
            return Ok(ret);
        }


        ///////////////////////////////////////////////////////////// GRUPO PRODUTO
        //GrupoProduto CREATE
        [HttpPost]
        [Route("GrupoProdutoCreate")]
        [AllowAnonymous]
        public IHttpActionResult GrupoProdutoCreate(GrupoProduto grupoProduto)
        {
            var ret = GrupoProdutoDao.Cadastrar(grupoProduto);
            return Ok(ret);

        }

        //GrupoProduto LIST
        [HttpGet]
        [Route("GrupoProdutoList")]
        [AllowAnonymous]
        public IHttpActionResult GrupoProdutoList()
        {
            List<GrupoProduto> lista = GrupoProdutoDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //GrupoProduto UPDATE
        [HttpPost]
        [Route("GrupoProdutoUpdate")]
        [AllowAnonymous]
        public IHttpActionResult GrupoProdutoUpdate(GrupoProduto grupoProduto)
        {
            var ret = GrupoProdutoDao.Alterar(grupoProduto);
            return Ok(ret);
        }

        //GrupoProduto DELETE
        [HttpPost]
        [Route("GrupoProdutoDelete")]
        [AllowAnonymous]
        public IHttpActionResult GrupoProdutoDelete(GrupoProduto grupoProduto)
        {
            var ret = GrupoProdutoDao.ExcluirPeloId(grupoProduto.Id);
            return Ok(ret);
        }



        ///////////////////////////////////////////////////////////// UNIDADES MEDIDA
        //UnidadeMedida CREATE
        [HttpPost]
        [Route("UnidadeMedidaCreate")]
        [AllowAnonymous]
        public IHttpActionResult UnidadeMedidaCreate(UnidadeMedida UnidadeMedida)
        {
            var ret = UnidadeMedidaDao.Cadastrar(UnidadeMedida);
            return Ok(ret);

        }

        //UnidadeMedida LIST
        [HttpGet]
        [Route("UnidadeMedidaList")]
        [AllowAnonymous]
        public IHttpActionResult UnidadeMedidaList()
        {
            List<UnidadeMedida> lista = UnidadeMedidaDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //UnidadeMedida UPDATE
        [HttpPost]
        [Route("UnidadeMedidaUpdate")]
        [AllowAnonymous]
        public IHttpActionResult UnidadeMedidaUpdate(UnidadeMedida UnidadeMedida)
        {
            var ret = UnidadeMedidaDao.Alterar(UnidadeMedida);
            return Ok(ret);
        }

        //UnidadeMedida DELETE
        [HttpPost]
        [Route("UnidadeMedidaDelete")]
        [AllowAnonymous]
        public IHttpActionResult UnidadeMedidaDelete(UnidadeMedida UnidadeMedida)
        {
            var ret = UnidadeMedidaDao.ExcluirPeloId(UnidadeMedida.Id);
            return Ok(ret);
        }


        ///////////////////////////////////////////////////////////// LOCAL ARMAZENAMENTO
        //LocalArmazenamento CREATE
        [HttpPost]
        [Route("LocalArmazenamentoCreate")]
        [AllowAnonymous]
        public IHttpActionResult LocalArmazenamentoCreate(LocalArmazenamento LocalArmazenamento)
        {
            var ret = LocalArmazenamentoDao.Cadastrar(LocalArmazenamento);
            return Ok(ret);

        }

        //LocalArmazenamento LIST
        [HttpGet]
        [Route("LocalArmazenamentoList")]
        [AllowAnonymous]
        public IHttpActionResult LocalArmazenamentoList()
        {
            List<LocalArmazenamento> lista = LocalArmazenamentoDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //LocalArmazenamento UPDATE
        [HttpPost]
        [Route("LocalArmazenamentoUpdate")]
        [AllowAnonymous]
        public IHttpActionResult LocalArmazenamentoUpdate(LocalArmazenamento LocalArmazenamento)
        {
            var ret = LocalArmazenamentoDao.Alterar(LocalArmazenamento);
            return Ok(ret);
        }

        //LocalArmazenamento DELETE
        [HttpPost]
        [Route("LocalArmazenamentoDelete")]
        [AllowAnonymous]
        public IHttpActionResult LocalArmazenamentoDelete(LocalArmazenamento LocalArmazenamento)
        {
            var ret = LocalArmazenamentoDao.ExcluirPeloId(LocalArmazenamento.Id);
            return Ok(ret);
        }



        ///////////////////////////////////////////////////////////// MARCA PRODUTO
        //MarcaProduto CREATE
        [HttpPost]
        [Route("MarcaProdutoCreate")]
        [AllowAnonymous]
        public IHttpActionResult MarcaProdutoCreate(MarcaProduto MarcaProduto)
        {
            var ret = MarcaProdutoDao.Cadastrar(MarcaProduto);
            return Ok(ret);

        }

        //MarcaProduto LIST
        [HttpGet]
        [Route("MarcaProdutoList")]
        [AllowAnonymous]
        public IHttpActionResult MarcaProdutoList()
        {
            List<MarcaProduto> lista = MarcaProdutoDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //MarcaProduto UPDATE
        [HttpPost]
        [Route("MarcaProdutoUpdate")]
        [AllowAnonymous]
        public IHttpActionResult MarcaProdutoUpdate(MarcaProduto MarcaProduto)
        {
            var ret = MarcaProdutoDao.Alterar(MarcaProduto);
            return Ok(ret);
        }

        //MarcaProduto DELETE
        [HttpPost]
        [Route("MarcaProdutoDelete")]
        [AllowAnonymous]
        public IHttpActionResult MarcaProdutoDelete(MarcaProduto MarcaProduto)
        {
            var ret = MarcaProdutoDao.ExcluirPeloId(MarcaProduto.Id);
            return Ok(ret);
        }




        ///////////////////////////////////////////////////////////// PERFIL
        //Perfil CREATE
        [HttpPost]
        [Route("PerfilCreate")]
        [AllowAnonymous]
        public IHttpActionResult MarcaProdutoCreate(Perfil Perfil)
        {
            var ret = PerfilDao.Cadastrar(Perfil);
            return Ok(ret);

        }

        //Perfil LIST
        [HttpGet]
        [Route("PerfilList")]
        [AllowAnonymous]
        public IHttpActionResult PerfilList()
        {
            List<Perfil> lista = PerfilDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //Perfil UPDATE
        [HttpPost]
        [Route("PerfilUpdate")]
        [AllowAnonymous]
        public IHttpActionResult PerfilUpdate(Perfil Perfil)
        {
            var ret = PerfilDao.Alterar(Perfil);
            return Ok(ret);
        }

        //Perfil DELETE
        [HttpPost]
        [Route("PerfilDelete")]
        [AllowAnonymous]
        public IHttpActionResult PerfilDelete(Perfil Perfil)
        {
            var ret = PerfilDao.ExcluirPeloId(Perfil.Id);
            return Ok(ret);
        }




        ///////////////////////////////////////////////////////////// PRODUTO
        //Produto CREATE
        [HttpPost]
        [Route("ProdutoCreate")]
        [AllowAnonymous]
        public IHttpActionResult ProdutoCreate(Produto Produto)
        {
            var ret = ProdutoDao.Cadastrar(Produto);
            return Ok(ret);

        }

        //Produto LIST
        [HttpGet]
        [Route("ProdutoList")]
        [AllowAnonymous]
        public IHttpActionResult ProdutoList()
        {
            List<Produto> lista = ProdutoDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //Produto UPDATE
        [HttpPost]
        [Route("ProdutoUpdate")]
        [AllowAnonymous]
        public IHttpActionResult ProdutoUpdate(Produto Produto)
        {
            var ret = ProdutoDao.Alterar(Produto);
            return Ok(ret);
        }

        //Produto DELETE
        [HttpPost]
        [Route("ProdutoDelete")]
        [AllowAnonymous]
        public IHttpActionResult ProdutoDelete(Produto Produto)
        {
            var ret = ProdutoDao.ExcluirPeloId(Produto.Id);
            return Ok(ret);
        }

        ///////////////////////////////////////////////////////////// USUARIO
        //Usuario CREATE
        [HttpPost]
        [Route("UsuarioCreate")]
        [AllowAnonymous]
        public IHttpActionResult UsuarioCreate(Usuario Usuario)
        {
            var ret = UsuarioDao.Cadastrar(Usuario);
            return Ok(ret);

        }

        //Usuario LIST
        [HttpGet]
        [Route("UsuarioList")]
        [AllowAnonymous]
        public IHttpActionResult UsuarioList()
        {
            List<Usuario> lista = UsuarioDao.RecuperarLista();
            if (lista != null)
            {
                return Ok(lista);
            }
            return NotFound();

        }

        //Usuario UPDATE
        [HttpPost]
        [Route("UsuarioUpdate")]
        [AllowAnonymous]
        public IHttpActionResult UsuarioUpdate(Usuario Usuario)
        {
            var ret = UsuarioDao.Alterar(Usuario);
            return Ok(ret);
        }

        //Usuario DELETE
        [HttpPost]
        [Route("UsuarioDelete")]
        [AllowAnonymous]
        public IHttpActionResult UsuarioDelete(Usuario Usuario)
        {
            var ret = UsuarioDao.ExcluirPeloId(Usuario.Id);
            return Ok(ret);
        }



        [HttpPost]
        [Route("Autenticar")]
        public async Task<IHttpActionResult> Autenticar(Usuario user)
        {

            var ret = await UsuarioDao.ValidarUsuarioAPI(user.Login, user.Senha);
            if(ret != null)
            {
                return Ok(ret);
            }
            return Ok(new {mensagem = "Usuário ou senha inválidos!", erro = 101 });

        }
    }
}
