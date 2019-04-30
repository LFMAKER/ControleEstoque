﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControleEstoque.Web.Models
{
	public class EstadoModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        [MaxLength(30, ErrorMessage = "O nome pode ter no máximo 30 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha a UF.")]
        [MaxLength(3, ErrorMessage = "A UF deve ter 2 caracteres.")]
        public string UF { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Selecione o país.")]
        public int IdPais { get; set; }


    }
}