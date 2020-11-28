using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Duracao
    {
        public int DuracaoId { get; set; }

        [Required]
        public int HorasInicio { get; set; }

        [Required]
        public int HorasFim { get; set; }

        public ICollection<Atratividade> Atratividades { get; set; }
    }
}
