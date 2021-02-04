using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class ListaOcupacao
    {
        public IEnumerable<Ocupacao> ocupacao { get; set; }

        public IEnumerable<Miradouro> miradouro { get; set; }

        public IEnumerable<Hora> horas { get; set; }

        public PagingInfoOcupacao pagination { get; set; }
    }
}
