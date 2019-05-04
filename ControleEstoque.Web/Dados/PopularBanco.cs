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
            GrupoProdutoModel GrupoProduto1 = new GrupoProdutoModel
            {
                Nome = "Hospitalar",
                Ativo = true
            };
            GrupoProdutoDao.Salvar(GrupoProduto1);

            MarcaProdutoModel MarcaProduto1 = new MarcaProdutoModel
            {
                Nome = "OKX LTA",
                Ativo = true
            };
            MarcaProdutoDao.Salvar(MarcaProduto1);

            LocalArmazenamentoModel LocalArmazenamento1 = new LocalArmazenamentoModel
            {
                Nome = "Salar 223",
                Ativo = true
            };

            LocalArmazenamentoDao.Salvar(LocalArmazenamento1);

            UnidadeMedidaModel UnidadeMedida1 = new UnidadeMedidaModel
            {
                Nome = "Kilogramas",
                Sigla = "KG",
                Ativo = true
            };
            UnidadeMedidaDao.Salvar(UnidadeMedida1);


            PaisModel Pais1 = new PaisModel
            {
                Nome = "Brazil",
                NomePt = "Brasil",
                Sigla = "BR",
                Bacen = "00023",
                Ativo = true
            };

            PaisDao.Salvar(Pais1);

            EstadoModel Estado1 = new EstadoModel
            {
                Nome = "Parana",
                UF = "PR",
                IdPais = 1,
                Pais = null,
                Ativo = true
            };

            EstadoDao.Salvar(Estado1);

            CidadeModel Cidade1 = new CidadeModel
            {
                Nome = "Curitiba",
                IdEstado = 1,
                IdPais = 1,
                Estado = null,
                Ativo = true
            };

            CidadeDao.Salvar(Cidade1);



        }

    }
}