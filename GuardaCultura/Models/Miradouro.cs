using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Miradouro
    {
        public int MiradouroId { get; set; }

        [Required(ErrorMessage = " Escreva o nome do miradouro que pretende inserir!")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "O nome do miradouro deve ter no minimo 3 caracteres e no maximo 256")]
        public string Nome { get; set; }

        [Required(ErrorMessage = " Escreva a localização do miradouro que pretende inserir!")]
        [StringLength(256, ErrorMessage = "A localização do miradouro deve ter no minimo 3 caracteres e no maximo 256")]
        public string Localizacao { get; set; }

        //[Required(ErrorMessage = " Campo obrigatório. Insira as Coordenadas da Latitude DD do miradouro !")]
        public string Latitude_DD { get; set; }

        //[Required(ErrorMessage = " Campo obrigatório. Insira as Coordenadas da Longitude DD do miradouro!")]
        public string Longitude_DD { get; set; }

        //[Required(ErrorMessage = " Campo obrigatório. Insira as Coordenadas da Latitude DMS do miradouro!")]
        public string Latitude_DMS { get; set; }

        //[Required(ErrorMessage = " Campo obrigatório. Insira as Coordenadas da Longitude DMS do miradouro!")]
        public string Longitude_DMS { get; set; }

        [Required(ErrorMessage = " Campo obrigatório. Insira o tipo de terreno do miradouro!")]// campo, cidade...
        public string Terreno { get; set; }

        [Required]
        public bool E_Miradouro { get; set; }//inserido pelo sistema
   
        public string Condicoes { get; set; }
        
        public int Ocupacao_maxima { get; set; }// required se for miradouro
        
        [StringLength(256, MinimumLength = 3, ErrorMessage = "A localização do miradouro deve ter no minimo 3 caracteres e no maximo 256")]
        public string Descricao { get; set; }// required se for miradouro

        [Required]
        public bool Ativo { get; set; }

        public bool Disponibilidade { get; set; }

        public ICollection<Atratividade> Atratividades { get; set; }

        public ICollection<Fotografia> Fotografias  { get; set; }

        public ICollection<Ocupacao> Ocupacaos  { get; set; }
    }
}
