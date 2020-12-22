using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class MiradouroLista
    {
         public IEnumerable<Miradouro> Miradouros { get; set; }

        public PagingInfoMiradouro pagination { get; set; }

        public int mira { get; set; }

        public int ati { get; set; }
    }
}
