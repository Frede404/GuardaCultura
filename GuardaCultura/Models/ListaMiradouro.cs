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

        public int grausLat { get; set; }

        public float minutoLat { get; set; }

        public float segundosLat { get; set; }

        public char DirNS { get; set; }

        public int grausLong { get; set; }

        public float minutosLong { get; set; }

        public float segundosLong { get; set; }

        public char DirWE { get; set; }

        public double latiDD { get; set; }
        
        public double longDD { get; set; }
    }
}
