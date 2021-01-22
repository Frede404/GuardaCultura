using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class MiradouroFoto
    {
        public int MiradouroId { get; set; }

        public int ContagemRepetido { get; set; }

        public string Nome { get; set; }

        public string Localizacao { get; set; }

        public string Coordenadas_DD { get; set; }

        public string Coordenadas_DMS { get; set; }

        public string Terreno { get; set; }

        public bool E_Miradouro { get; set; }

        public string Condicoes { get; set; }

        public int Ocupacao_maxima { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public byte[] Fotografia { get; set; }

        public int FotoId { get; set; }

        public float Classificacao { get; set; }

        public bool Aprovada { get; set; }

        /*public byte[] Fotografia2 { get; set; }

        public int Foto2Id { get; set; }

        public byte[] Fotografia3 { get; set; }

        public int Foto3Id { get; set; }

        public byte[] Fotografia4 { get; set; }

        public int Foto4Id { get; set; }

        public byte[] Fotografia5 { get; set; }

        public int Foto5Id { get; set; }

        public byte[] Fotografia6 { get; set; }

        public int Foto6Id { get; set; }*/
    }
}
