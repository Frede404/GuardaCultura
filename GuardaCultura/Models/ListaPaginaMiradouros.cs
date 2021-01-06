using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{   

    public class ListaPaginaMiradouros
    {
        public IEnumerable<Miradouro> Miradouros { get; set; }

        public IEnumerable<MiradouroFoto> MiradouroPaisagem { get; set; }

        public IEnumerable<MiradouroFoto> Fotografias { get; set; }

        public IEnumerable<Fotografia> fotoapresentacao { get; set; }

        public IEnumerable<Fotografia> Fotografiasantigas { get; set; }

        public PagingInfoPaginaMiradouros pagination { get; set; }

    }
}
