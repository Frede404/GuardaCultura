﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Models
{
    public class PagingInfoOcupacao
    {
        public const int TAM_PAGINA = 15;
        public const int NUMERO_AANTES_ = 3;

        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    }
}
