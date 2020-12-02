﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class Miradouro
    {
        public int MiradouroId { get; set; }
        
        [Required(ErrorMessage =" Escreva o nome do miradouro que pretende inserir!")]
        [StringLength(256, MinimumLength = 3, ErrorMessage ="O nome do miradouro deve ter no minimo 3 caracteres e no maximo 256")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = " Escreva a localização do miradouro que pretende inserir!")]
        [StringLength(256, ErrorMessage = "A localização do miradouro deve ter no minimo 3 caracteres e no maximo 256")]
        public string Localizacao { get; set; }
        
        [Required]
        public string Coordenadas_gps { get; set; }

        public string Terreno { get; set; }
        
        [Required]
        public bool E_Miradouro { get; set; }

        public string Condicoes { get; set; }
        
        [Required]
        public int Ocupacao_maxima { get; set; }
        
        [Required(ErrorMessage = " Escreva a localização do miradouro que pretende inserir!")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "A localização do miradouro deve ter no minimo 3 caracteres e no maximo 256")]
        public string Descricao { get; set; }
        
        public ICollection<Atratividade> Atratividades { get; set; }

        public ICollection<Fotografia> Fotografias  { get; set; }

        public ICollection<Ocupacao> Ocupacaos  { get; set; }
    }
}