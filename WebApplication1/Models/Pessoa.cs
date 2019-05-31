using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.CustomValidations;

namespace WebApplication1.Models
{
    public class Pessoa
    {
        [Key]
        public int Codigo { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [ValidacaoCPF(ErrorMessage = "CPF inválido!")]
        [ValidacaoCPFExistente(ErrorMessage = "Este CPF já está cadastrado!!")]
        public string CPF { get; set; }
        [Required]
        [StringLength(2)]
        public string UF { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimento { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 6)]
        public string Senha { get; set; }
    }
}