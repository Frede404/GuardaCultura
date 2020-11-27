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
        public int Horas { get; set; }
        
        public ICollection<Ocupacao> Ocupacaos { get; set; }
        
        public ICollection<Atratividade> Atratividades { get; set; }
    }
}
