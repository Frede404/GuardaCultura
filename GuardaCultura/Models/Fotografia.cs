using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Fotografia
    {
        public int ImagemId { get; set; }

        [Required]
        public string nome { get; set; }//alt

        public string data_imagem { get; set; }

        public float classificacao { get; set; }

        //public TipoImagem foto { get; set; }

        [Required]
        public int EstacaoAnoId { get; set; }

        public int PessoaID { get; set; }

        public int MiradouroId { get; set; }

        public int TipoImagemId { get; set; }
    }
}
