using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Dados
{
    public class PopularBanco
    {

        public static void Inserir()
        {
            GrupoProduto GrupoProduto1 = new GrupoProduto
            {
                Nome = "Hospitalar",
                Ativo = true
            };
            GrupoProdutoDao.Salvar(GrupoProduto1);

            MarcaProduto MarcaProduto1 = new MarcaProduto
            {
                Nome = "OKX LTA",
                Ativo = true
            };
            MarcaProdutoDao.Salvar(MarcaProduto1);

            LocalArmazenamento LocalArmazenamento1 = new LocalArmazenamento
            {
                Nome = "Salar 223",
                Ativo = true
            };

            LocalArmazenamentoDao.Salvar(LocalArmazenamento1);

            UnidadeMedida UnidadeMedida1 = new UnidadeMedida
            {
                Nome = "Kilogramas",
                Sigla = "KG",
                Ativo = true
            };
            UnidadeMedidaDao.Salvar(UnidadeMedida1);

           
            
           



        }

    }
}