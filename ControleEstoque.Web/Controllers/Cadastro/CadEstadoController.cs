﻿using ControleEstoque.Web.Dal.Cadastro;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{
    [Authorize(Roles = "Gerente,Administrativo,Operador")]
    public class CadEstadoController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = EstadoDao.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina);
            var quant = EstadoDao.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;
            ViewBag.Paises = PaisDao.RecuperarLista();

            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EstadoPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            var lista = EstadoDao.RecuperarLista(pagina, tamPag, filtro, ordem: ordem);

            return Json(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarEstadosDoPais(int idPais)
        {
            var lista = EstadoDao.RecuperarLista(idPais: idPais);

            return Json(lista);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarEstado(int id)
        {
            return Json(EstadoDao.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Administrativo")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirEstado(int id)
        {
            return Json(EstadoDao.ExcluirPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarEstado(EstadoModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = EstadoDao.Salvar(model);
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }
    }
}