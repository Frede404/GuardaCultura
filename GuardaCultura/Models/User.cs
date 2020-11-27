﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class User
    {
        public int PessoaId { get; set; }

        [Required]
        [StringLength(512)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório ter uma password"]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password tem de ter no minimo 8 caracteres e no maximo 20")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório"]
        [StringLength(512)]
        public string Email { get; set; }

        [Required]
        [StringLength(5)]
        public string Ultima_Lingua { get; set; }

        [Required]
        [StringLength(512)]
        public string Funcao { get; set; }

        public string Data_Nasc { get; set; }

        public string Sexo { get; set; }

        public string Nacionalidade { get; set; }

        public float Fiabilidade { get; set; }

        public ICollection<Fotografia> Fotografias { get; set; }
    }
}
