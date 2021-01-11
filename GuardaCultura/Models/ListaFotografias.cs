using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class ListaFotografias
    {
         public IEnumerable<Fotografia> Fotografias { get; set; }

        public PagingInfoFotografias pagination { get; set; }

        public int aprovacao { get; set; }

        public string ordenacao { get; set; }

        public int direcaoordena { get; set; }
    }
}
