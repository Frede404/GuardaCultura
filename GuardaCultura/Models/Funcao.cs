using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Funcao
    {
        public int FuncaoId { get; set; }
        
        [Required]
        public String FuncaoDesempenhar { get; set; }

        public ICollection<Pessoa> Pessoas { get; set; }
    }
}
