using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class TipoImagem
    {
        public int TipoImagemId { get; set; }

        [Required]
        public string Descrição { get; set; }

        public ICollection<Fotografia> Fotografias { get; set; }
    }
}
