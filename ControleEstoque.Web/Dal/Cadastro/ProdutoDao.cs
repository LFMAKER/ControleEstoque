using ControleEstoque.Web.Dal.Cadastro;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VendasOsorioA.DAL;

namespace ControleEstoque.Web.Models.Dal.Cadastro
{
    public class ProdutoDao
    {
        private static Context ctx = SingletonContext.GetInstance();


        //TODO: Realizar o controle da exception de uma exclusão de produtos com relação

        /// <summary>
        /// Retorna a quantidade de produtos 
        /// </summary>
        /// <returns></returns>
        public static int RecuperarQuantidade()
        {
            var ret = 0;
            ret = ctx.Produtos.AsNoTracking().Count();
            return ret;
        }

        /// <summary>
        /// Retorna uma lista de produtos com base nos parâmetros informados
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="tamPagina"></param>
        /// <param name="filtro"></param>
        /// <param name="somenteAtivos"></param>
        /// <returns></returns>
        public static List<Produto> RecuperarLista(int pagina = 0, int tamPagina = 0, string filtro = "", bool somenteAtivos = false)
        {
            var ret = new List<Produto>();
            if (tamPagina != 0 && pagina != 0)
            {
                var pos = (pagina - 1) * tamPagina;
                if (!string.IsNullOrEmpty(filtro))
                {
                    ret = ctx.Produtos.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Nome.ToLower().Contains(filtro.ToLower())).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }
                else
                {
                    ret = ctx.Produtos.AsNoTracking().OrderBy(x => x.Nome).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).ToList();
                }

                if (somenteAtivos)
                {
                    ret = ctx.Produtos.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Ativo == true).ToList();
                }

            }
            else
            {
                ret = ctx.Produtos.AsNoTracking().OrderBy(x => x.Nome).ToList();
            }

            return ret;
        }

        /// <summary>
        /// Retorna um objeto de porduto recuperado pelo parâmetro id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Produto RecuperarPeloId(int? id)
        {
            if (id != null)
            {
                return ctx.Produtos.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// Realiza a exclusão de um produto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            var existing = ctx.Produtos.Include("LocalArmazenamento").FirstOrDefault(x => x.Id == id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                //TODO: Tentar AtualizarCapacidadeAtual depois de remover o produto

                LocalArmazenamentoDao.AtualizarCapacidadeAtual(existing.LocalArmazenamento, existing.QuantEstoque, "Remover", id);
                existing = ctx.Produtos.Include("LocalArmazenamento").FirstOrDefault(x => x.Id == id);
                if (existing != null)
                {
                    ctx.Entry(existing).State = EntityState.Detached;
                }
                ctx.Produtos.Attach(existing);
                ctx.Entry(existing).State = EntityState.Deleted;
                ctx.SaveChanges();

                ret = true;
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return ret;
        }

        /// <summary>
        /// Realiza o processo de salvar ou alterar um produto
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static int Salvar(Produto pm)
        {
            var ret = 0;
            bool detachAndAtach = RealizarDetachAndAtach(pm);
            if (detachAndAtach)
            {
                var model = RecuperarPeloId(pm.Id);
                if (model == null)
                {
                    Cadastrar(pm);
                }
                else
                {
                    Alterar(pm);
                }
                ret = pm.Id;
            }
            return ret;
        }

        /// <summary>
        /// Realiza o cadastro direto de um produto sem verificar se é um novo produto ou uma alteração de um existente
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Cadastrar(Produto p)
        {
            ctx.Produtos.Add(p);
            ctx.SaveChanges();

            //Atualizando CapacidadeAtual LocalArmazenamento
            LocalArmazenamentoDao.AtualizarCapacidadeAtual(p.LocalArmazenamento, p.QuantEstoque, "Cadastrar");
            return true;
        }

        /// <summary>
        /// Realiza a alteração direta de um produto sem verificar se o mesmo já existe.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Alterar(Produto p)
        {
            var existing = ctx.Produtos.FirstOrDefault(x => x.Id == p.Id);
            if (existing != null)
            {
                ctx.Entry(existing).State = EntityState.Detached;
            }

            try
            {
                ctx.Produtos.Attach(p);
                ctx.Entry(p).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return true;
        }

        public static bool RealizarDetachAndAtach(Produto pm)
        {
            try
            {
                /*Attachando para evitar duplicação, pois como estou trabalhando com o conceito
                 * AsNoTracking, pode ocorrer que o entity não encontre o objeto no contexto e duplique, 
                 * ou encontre um objeto diferente e trate o que desejamos utilizar como um novo.
                 * 
                 **/
                /*Para facilitar o debug desse método, será utilizado vários try catch*/

                //Try Fornecedor
                var existingFornecedor = ctx.Fornecedores.FirstOrDefault(x => x.Id == pm.Fornecedor.Id);
                if (existingFornecedor != null)
                {
                    ctx.Entry(existingFornecedor).State = EntityState.Detached;
                }

                try
                {
                    ctx.Fornecedores.Attach(pm.Fornecedor);

                }
                catch (System.Exception ex)
                {
                    throw;
                }

                //Try GrupoProduto
                var existingGrupo = ctx.GruposProdutos.FirstOrDefault(x => x.Id == pm.GrupoProduto.Id);
                if (existingGrupo != null)
                {
                    ctx.Entry(existingGrupo).State = EntityState.Detached;
                }

                try
                {
                    ctx.GruposProdutos.Attach(pm.GrupoProduto);

                }
                catch (System.Exception ex)
                {
                    throw;
                }

                //Try LocaisArmazenamento
                var existingLocais = ctx.LocaisArmazenamentos.FirstOrDefault(x => x.Id == pm.LocalArmazenamento.Id);
                if (existingLocais != null)
                {
                    ctx.Entry(existingLocais).State = EntityState.Detached;
                }

                try
                {
                    ctx.LocaisArmazenamentos.Attach(pm.LocalArmazenamento);

                }
                catch (System.Exception ex)
                {
                    throw;
                }

                //Try Marcas
                var existingMarcas = ctx.MarcasProdutos.FirstOrDefault(x => x.Id == pm.MarcaProduto.Id);
                if (existingMarcas != null)
                {
                    ctx.Entry(existingMarcas).State = EntityState.Detached;
                }

                try
                {
                    ctx.MarcasProdutos.Attach(pm.MarcaProduto);

                }
                catch (System.Exception ex)
                {
                    throw;
                }

                //Try UnidadesMedidas
                var existingUnidades = ctx.UnidadesMedida.FirstOrDefault(x => x.Id == pm.UnidadeMedida.Id);
                if (existingUnidades != null)
                {
                    ctx.Entry(existingUnidades).State = EntityState.Detached;
                }

                try
                {
                    ctx.UnidadesMedida.Attach(pm.UnidadeMedida);

                }
                catch (System.Exception ex)
                {
                    throw;
                }

                //return true;
            }
            catch (Exception)
            {

                throw;
            }

            return true;
        }


        public static int RecuperarCapacidadeLivreArmazenamentoProduto(int? id)
        {
            Produto recuperado = ctx.Produtos.AsNoTracking()
                .Include("LocalArmazenamento")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            int capacidadeLivre = recuperado.LocalArmazenamento.CapacidadeTotal - recuperado.LocalArmazenamento.CapacidadeAtual;
            return capacidadeLivre;

        }

        public static int RecuperarEstoqueAtualProduto(int? id)
        {
            Produto recuperado = ctx.Produtos.AsNoTracking()
                .Include("LocalArmazenamento")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            int estoqueAtual = recuperado.QuantEstoque;
            return estoqueAtual;

        }


        //EM CONSTRUÇÂO--------------------------------------------------------------------
        //TODO:  Construir Entrada e saída de produtos


        public static string SalvarPedidoEntrada(DateTime data, Dictionary<int, int> produtos)
        {
            return SalvarPedido(data, produtos, "entrada_produto", true);
        }

        public static string SalvarPedidoSaida(DateTime data, Dictionary<int, int> produtos)
        {
            return SalvarPedido(data, produtos, "saida_produto", false);
        }


        //TODO: Verificar se o método funciona, atualizarCapacidadeAtual, verificar JS no Front da Entrada
        public static string SalvarPedido(DateTime data, Dictionary<int, int> produtos, string nomeTabela, bool entrada)
        {
            var ret = "";

            try
            {

                var numPedido = "";
                var numPedidoCalculo = 0;
                if (nomeTabela.Equals("entrada_produto"))
                {
                    EntradaProduto resultadoConsulta = null;

                    var quantidadeEntradas = ctx.EntradasProdutos.Count();
                    if (quantidadeEntradas > 0)
                    {
                        resultadoConsulta = ctx.EntradasProdutos.OrderByDescending(x => x.Id).Take(1).Single();
                        numPedidoCalculo = (Convert.ToInt32(resultadoConsulta.Numero) + 1);
                        numPedido = numPedidoCalculo.ToString();

                    }
                    else
                    {
                        numPedido = Convert.ToString(1);
                    }
                }
                else if (nomeTabela.Equals("saida_produto"))
                {
                    SaidaProduto resultadoConsulta = null;

                    var quantidadeSaidas = ctx.SaidasProdutos.Count();
                    if (quantidadeSaidas > 0)
                    {
                        resultadoConsulta = ctx.SaidasProdutos.OrderByDescending(x => x.Id).Take(1).Single();
                        numPedidoCalculo = (Convert.ToInt32(resultadoConsulta.Numero) + 1);
                        numPedido = numPedidoCalculo.ToString();

                    }
                    else
                    {
                        numPedido = Convert.ToString(1);
                    }
                }


                using (var transacao = ctx.Database.BeginTransaction())
                {
                    foreach (var produto in produtos)
                    {

                        if (nomeTabela.Equals("entrada_produto"))
                        {
                            EntradaProduto ep = new EntradaProduto();
                            ep.Numero = numPedido;
                            ep.Data = data;
                            ep.IdProduto = produto.Key;
                            ep.Quantidade = produto.Value;

                            ctx.EntradasProdutos.Add(ep);


                            Produto recuperado = ctx.Produtos.Find(produto.Key);

                            var existingProduto = ctx.Produtos.FirstOrDefault(x => x.Id == produto.Key);
                            if (existingProduto != null)
                            {
                                ctx.Entry(existingProduto).State = EntityState.Detached;
                            }

                            try
                            {
                                ctx.Produtos.Attach(recuperado);
                                recuperado.QuantEstoque = recuperado.QuantEstoque + produto.Value;
                                ctx.Entry(recuperado).State = EntityState.Modified;
                                ctx.SaveChanges();

                            }
                            catch (System.Exception ex)
                            {
                                throw;
                            }





                        }
                        else if (nomeTabela.Equals("saida_produto"))
                        {
                            SaidaProduto ep = new SaidaProduto();
                            ep.Numero = numPedido;
                            ep.Data = data;
                            ep.IdProduto = produto.Key;
                            ep.Quantidade = produto.Value;

                            ctx.SaidasProdutos.Add(ep);


                            Produto recuperado = ctx.Produtos.Find(produto.Key);

                            var existingProduto = ctx.Produtos.FirstOrDefault(x => x.Id == produto.Key);
                            if (existingProduto != null)
                            {
                                ctx.Entry(existingProduto).State = EntityState.Detached;
                            }

                            try
                            {
                                ctx.Produtos.Attach(recuperado);
                                recuperado.QuantEstoque = recuperado.QuantEstoque - produto.Value;
                                ctx.Entry(recuperado).State = EntityState.Modified;
                                ctx.SaveChanges();

                            }
                            catch (System.Exception ex)
                            {
                                throw;
                            }
                        }
                    }

                    transacao.Commit();

                    ret = numPedido;
                }

            }
            catch (Exception ex)
            {
            }

            return ret;
        }


        public static List<EntradaProduto> RecuperarListaEntradaProdutos(int pagina = 0, int tamPagina = 0, string dataInicio = "", string dataFim = "" )
        {
            var pos = (pagina - 1) * tamPagina;
            if (tamPagina != 0 && pagina != 0 && dataInicio == "" && dataFim == "" && (dataInicio != null && dataFim != null))
            {
                
                return ctx.EntradasProdutos.AsNoTracking().OrderByDescending(x => x.Data).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();

            }else if (tamPagina != 0 && pagina != 0 && ((dataInicio != "" && dataFim != "") &&  (dataInicio != null && dataFim != null)))
            {
                var dataInicioConvertida = Convert.ToDateTime(dataInicio);
                var dataFimConvertida = Convert.ToDateTime(dataFim);


                return ctx.EntradasProdutos.OrderByDescending(x => x.Data).Where(x => x.Data >= dataInicioConvertida && x.Data <= dataFimConvertida).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();
            }else if (tamPagina != 0 && pagina != 0 && ((dataInicio == "" && dataFim == "")|| (dataInicio == null && dataFim == null)))
            {
                return ctx.EntradasProdutos.AsNoTracking().OrderByDescending(x => x.Data).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();
            }
            else
            {
                return ctx.EntradasProdutos.OrderByDescending(x => x.Data).Include("Produto").ToList();
            }

        }


        public static List<SaidaProduto> RecuperarListaSaidaProdutos(int pagina = 0, int tamPagina = 0, string dataInicio = "", string dataFim = "")
        {
            var pos = (pagina - 1) * tamPagina;
            if (tamPagina != 0 && pagina != 0 && dataInicio == "" && dataFim == "" && (dataInicio != null && dataFim != null))
            {

                return ctx.SaidasProdutos.AsNoTracking().OrderByDescending(x => x.Data).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();

            }
            else if (tamPagina != 0 && pagina != 0 && ((dataInicio != "" && dataFim != "") && (dataInicio != null && dataFim != null)))
            {
                var dataInicioConvertida = Convert.ToDateTime(dataInicio);
                var dataFimConvertida = Convert.ToDateTime(dataFim);


                return ctx.SaidasProdutos.OrderByDescending(x => x.Data).Where(x => x.Data >= dataInicioConvertida && x.Data <= dataFimConvertida).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();
            }
            else if (tamPagina != 0 && pagina != 0 && ((dataInicio == "" && dataFim == "") || (dataInicio == null && dataFim == null)))
            {
                return ctx.SaidasProdutos.AsNoTracking().OrderByDescending(x => x.Data).Skip(pos > 0 ? pos - 1 : 0).Take(tamPagina).Include("Produto").ToList();
            }
            else
            {
                return ctx.SaidasProdutos.OrderByDescending(x => x.Data).Include("Produto").ToList();
            }

        }


        public static int RecuperarQuantidadeEntradaProdutos()
        {
            return ctx.EntradasProdutos.Count();
        }

        public static int RecuperarQuantidadeSaidaProdutos()
        {
            return ctx.SaidasProdutos.Count();
        }


        public static bool RemoverEntradaSaidaProduto(int? id, string tipo)
        {
            try
            {
                if (tipo.Equals("entrada"))
                {
                    var EntradaRecuperada = ctx.EntradasProdutos.Find(id);
                    ctx.EntradasProdutos.Remove(EntradaRecuperada);
                    ctx.SaveChanges();
                    return true;
                }else if (tipo.Equals("saida"))
                {
                    var SaidaRecuperada = ctx.SaidasProdutos.Find(id);
                    ctx.SaidasProdutos.Remove(SaidaRecuperada);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return false;


        }


        public static List<EntradaProduto> RecuperarEntradaPorNumero(string numero)
        {
            return ctx.EntradasProdutos.Where(x => x.Numero.Equals(numero)).ToList();
        }

        public static List<SaidaProduto> RecuperarSaidaPorNumero(string numero)
        {
            return ctx.SaidasProdutos.Where(x => x.Numero.Equals(numero)).ToList();
        }




    }

}
