using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Hora
    {
        public int HoraId { get; set; }
        
        [Required]
        public int Horas { get; set; }// >=0 e <= 23
        
        public ICollection<Ocupacao> Ocupacaos { get; set; }
    }
}
