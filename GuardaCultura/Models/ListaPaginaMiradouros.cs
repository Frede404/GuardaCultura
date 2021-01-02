using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class ListaPaginaMiradouros
    {

        public IEnumerable<Miradouro> Miradouros { get; set; }

        public IEnumerable<Fotografia> Fotografias { get; set; }

        public PagingInfoPaginaMiradouros pagination { get; set; }

    }
}
