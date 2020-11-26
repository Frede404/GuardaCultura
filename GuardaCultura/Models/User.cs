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

        [Required]
        [StringLength(512)]
        public string Email { get; set; }

        public string data_nasc { get; set; }

        public string sexo { get; set; }

        public string Nacionalidade { get; set; }

        public float fiabilidade { get; set; }

        public string lingua { get; set; }

        public int privilegios { get; set; }
    }
}
