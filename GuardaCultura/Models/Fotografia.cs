using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Fotografia
    {
        public int FotografiaId { get; set; }

        [Required(ErrorMessage = "Escreva o nome da Fotografia que pretende inserir!")]
        public string Nome { get; set; }//alt

        public string Data_imagem { get; set; }

        public float Classificacao { get; set; }// não é introduzida pelo utilizador, inicializada a 5

        //[Required(ErrorMessage = "Inserir Foto!")]
        public byte[] Foto { get; set; }

        [Required]
        public bool Aprovada { get; set; }

        [Required]
        public int N_Votos { get; set; }
        
        public int EstacaoAnoId { get; set; }

        public EstacaoAno EstacaoAno { get; set; }

        public int PessoaId { get; set; }

        public Pessoa Pessoa { get; set; }

        public int MiradouroId { get; set; }

        public Miradouro Miradouro { get; set; }

        public int TipoImagemId { get; set; }

        public TipoImagem TipoImagem { get; set; }
    }
}
