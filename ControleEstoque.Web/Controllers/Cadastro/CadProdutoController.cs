using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using ControleEstoque.Web.Models.Dal.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadProdutoController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = ProdutoDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            var quant = ProdutoDao.RecuperarQuantidade();
            
            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            /*Recuperando os itens abaixos para os DropDownLists
            Obs: O método deverá retornar todas as linhas, sem filtragem e apenas os ativos*/
            ViewBag.UnidadesMedida = UnidadeMedidaDao.RecuperarLista(0, 0, "", true);
            ViewBag.Grupos = GrupoProdutoDao.RecuperarLista(0, 0, "", true);
            ViewBag.Marcas = MarcaProdutoDao.RecuperarLista(0, 0, "", true);
            ViewBag.Fornecedores = FornecedorDao.RecuperarLista(0, 0, "", true);
            ViewBag.LocaisArmazenamento = LocalArmazenamentoDao.RecuperarLista(0, 0, "", true);

            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ProdutoPagina(int pagina, int tamPag, string filtro)
        {
            var lista = ProdutoDao.RecuperarLista(pagina, tamPag, filtro);

            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarProduto(int id)
        {
            return Json(ProdutoDao.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirProduto(int id)
        {
            return Json(ProdutoDao.ExcluirPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarProduto(Produto model, int? IdUnidadeMedida,
            int? IdGrupo, int? IdMarca, int? IdFornecedor,
            int? IdLocalArmazenamento)
        {
            //Iniciando variáveis auxiliares
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;
            bool validationDropDownListError = false;
            string validationDropDownListErrorMessage = "Os campos a seguir são obrigatórios: ";

            //Recuperando relações
            model.UnidadeMedida = UnidadeMedidaDao.RecuperarPeloId(IdUnidadeMedida);
            model.GrupoProduto = GrupoProdutoDao.RecuperarPeloId(IdGrupo);
            model.MarcaProduto = MarcaProdutoDao.RecuperarPeloId(IdMarca);
            model.Fornecedor = FornecedorDao.RecuperarPeloId(IdFornecedor);
            model.LocalArmazenamento = LocalArmazenamentoDao.RecuperarPeloId(IdLocalArmazenamento);


            /*Validando se todas os DropDownList foram preenchidos pelo usuário,
             * Caso não tenha, será construido uma mensagem de erro na variável
             * validationDropDownListErrorMessage e a variável validationDropDownListError receberá true
             * e ao final será retornado a mensagem ao usuário via JSON.
             * Cabe ao Front-end exibir essa mensagem.
             * */
            if (model.UnidadeMedida == null)
            {
                validationDropDownListError = true;
                validationDropDownListErrorMessage += "  • Unidade de Medida ";
            }
            if (model.GrupoProduto == null)
            {
                validationDropDownListError = true;
                validationDropDownListErrorMessage += "  • Grupo ";

            }
           if (model.MarcaProduto == null)
            {
                validationDropDownListError = true;
                validationDropDownListErrorMessage += "  • Marca ";
            }
            if (model.Fornecedor == null)
            {
                validationDropDownListError = true;
                validationDropDownListErrorMessage += "  • Fornecedor ";
            }
            if (model.LocalArmazenamento == null)
            {
                validationDropDownListError = true;
                validationDropDownListErrorMessage += "  • Local de Armazenamento ";
            }

            /*
             A condicional verifica se o validationDropDownListError está falso, 
             se estiver, significa que a validação anterior foi aprovada e o usuário
             preencheu todos os campos, não existem dados nullos no modelo a ser gravado.
             */
            if (!validationDropDownListError)
            {

                /*
                 O ModelState verifica se os demais campos atribuidos como required no
                 modelo foram preenchidos, caso não atribui a mensagem "Aviso" ao resultado
                 e resulta uma lista de todos as mensagens de erros do ModelState para
                 "mensagens"
                 */
                if (!ModelState.IsValid)
                {
                    resultado = "AVISO";
                    mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    /*O ModelState e todos os DropDownList estão validos
                     Agora o sistema deverá tentar gravar as informações.
                     */
                    try
                    {
                        /*O método salvar é chamado e realiza a operação, o mesmo
                         * deverá retornar o id do modelo gravado caso tenha ocorrido
                         * tudo certo, esse id será fundamental para o front-end caso
                         * esteja utilizando uma abordagem em AJAX.
                         * */
                        
                        var id = ProdutoDao.Salvar(model);
                        //Se o id for maior que 0 significa que ocorreu tudo certo
                        if (id > 0)
                        {
                            idSalvo = id.ToString();
                        }
                        else
                        {
                            /*Aparentemente ocorreu um erro no processo de Salvar*/
                            resultado = "ERRO";
                        }
                    }
                    catch (Exception ex) //Uma exception foi detectada
                    {
                        /*Como o sistema está utilizando uma abordagem em AJAX,
                         * a tela não será atualizada, logo o usuário não receberá a mensagem de 
                         * error gigantesca do Asp.net, para isso será atribuido a mensagem
                         * "Erro" ao resultado.
                         * Para que assim será tratado no front-end uma mensagem mais amigável. 
                         * */

                        resultado = "ERRO";
                    }
                }
            }else
            {
                /* A validação dos DropDownList FALHOU, assim o resultado recebe
                 todas as mensagens de erros construídas referente aos DropDownList
                 */
                resultado = validationDropDownListErrorMessage;
            }

            /* Será retornado para o front-end:
             *  - Resultado: Se o front-end recebere algo além de "OK" que é o valor inicial
             * da variável resultado, o mesmo saberá que por algum motivo não foi realizado o
             * cadastro no banco de daods, e cabe a ele exibir oque a variável resultado retornou.
             *  - Mensagens: Caso a validação dos demais campos tenha falhado no ModelState, será
             *  retornado uma lista com todas as mensagens de erros.
             *  - IdSalvo: O Id será retonado para que o sistema consiga trabalhar utilizando
             *  AJAX, pois como a tela não atualiza, será necessário saber o IdSalvo para que 
             *  caso o usuário queira realizar um novo cadastro na mesma tela, ele consiga assim
             *  mostrar os dados corretos para o mesmo.
             * 
             * 
             * */
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }
    }
}