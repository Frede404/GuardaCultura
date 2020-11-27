using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class EstacaoAno
    {
        public int EstacaoAnoId { get; set; }
        [Required]
        [StringLength(9, MinimumLength = 5)]
        public string Nome_estacao { get; set; }
    }
}
