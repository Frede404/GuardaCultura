using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class ListaMiradouro
    {
         public IEnumerable<Miradouro> Miradouros { get; set; }

        public PagingInfoMiradouro pagination { get; set; }

        public int e_miradouro { get; set; }

        public int estado { get; set; }
    }
}
