using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class RegistarPessoaViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        [StringLength(512)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório")]
        [StringLength(512)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password óbrigatória")]
        public string Password { get; set; }

        [Required(ErrorMessage = "  Password óbrigatória")]
        [Compare("Password", ErrorMessage = "Password nao corresponde")]
        public string ConfirmPassword { get; set; }

        [StringLength(5)]
        public string Ultima_Lingua { get; set; }

        public string Data_Nasc { get; set; }

        public string Sexo { get; set; }

        public string Nacionalidade { get; set; }

        public float Fiabilidade { get; set; }

        public bool Bloqueio { get; set; }

        public string erroregistar { get; set; }
    }
}
