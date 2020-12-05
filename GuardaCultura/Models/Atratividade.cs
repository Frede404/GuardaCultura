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
        
        public int DuracaoId { get; set; }

        public Duracao Duracao { get; set; }

        public int EstacaoAnoId { get; set; }

        public EstacaoAno EstacaoAno { get; set; }

        public int MiradouroId { get; set; }

        public Miradouro Miradouro { get; set; }
    }
}
