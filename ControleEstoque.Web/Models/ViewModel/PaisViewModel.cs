using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Web.Models
{
    public class PaisViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        [MaxLength(60, ErrorMessage = "O nome pode ter no máximo 60 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o nome em português.")]
        [MaxLength(60, ErrorMessage = "O nome em português pode ter no máximo 60 caracteres.")]
        public string NomePt { get; set; }
        [Required(ErrorMessage = "Preencha a sigla.")]
        [MaxLength(2, ErrorMessage = "A sigla pode ter no máximo 2 caracteres.")]
        public string Sigla { get; set; }
        [Required(ErrorMessage = "Preencha o código Bacen.")]
        [MaxLength(5, ErrorMessage = "O código Bacen deve ter 5 caracteres.")]
        public string Bacen { get; set; }
        public bool Ativo { get; set; }



    }
}