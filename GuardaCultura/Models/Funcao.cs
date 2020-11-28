using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Funcao
    {
        public int FuncaoId { get; set; }

        public int FuncaoDesempenhar { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
