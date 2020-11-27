using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(512)]
        public string Nome { get; set; }

        [Required]
        [StringLength(512)]
        public string Password { get; set; }

        
        [StringLength(512)]
        public string Email { get; set; }

        [StringLength(512)]
        public string Ultima_Lingua { get; set; }

        [Required]
        public int Funcao { get; set; }

        public string Data_Nasc { get; set; }

        public string Sexo { get; set; }

        public string Nacionalidade { get; set; }

        public float Fiabilidade { get; set; }
    }
}
