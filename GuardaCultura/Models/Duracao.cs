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
        [StringLength(2)]
        public int HorasInicio { get; set; }// >=0 e <= 23

        [Required]
        [StringLength(2)]
        public int HorasFim { get; set; }// >=0 e <= 23

        public ICollection<Atratividade> Atratividades { get; set; }
    }
}
