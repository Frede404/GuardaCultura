using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Ocupacao
    {
        public int OcupacaoId { get; set; }
        
        [Required]
        public int Numero_pessoas { get; set; }
        
        [Required]
        public string Data { get; set; }
       
        public int MiradouroId { get; set; }

        public int HoraId { get; set; }

        public Hora Hora { get; set; }
    }
}
