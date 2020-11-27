using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Atratividade
    {
        public int AtratividadeId { get; set; }
        
        public int HoraId { get; set; }
        
        public int EstacaoAnoId { get; set; }
        
        public int MiradouroId { get; set; }
    }
}
