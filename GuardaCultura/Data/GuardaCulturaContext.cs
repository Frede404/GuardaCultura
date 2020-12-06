using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Models;

namespace GuardaCultura.Data
{
    public class GuardaCulturaContext : DbContext
    {
        public GuardaCulturaContext (DbContextOptions<GuardaCulturaContext> options)
            : base(options)
        {
        }

        public DbSet<GuardaCultura.Models.Miradouro> Miradouro { get; set; }

        public DbSet<GuardaCultura.Models.EstacaoAno> EstacaoAno { get; set; }

        public DbSet<GuardaCultura.Models.Hora> Hora { get; set; }

        public DbSet<GuardaCultura.Models.Ocupacao> Ocupacao { get; set; }

        public DbSet<GuardaCultura.Models.Atratividade> Atratividade { get; set; }

        public DbSet<GuardaCultura.Models.TipoImagem> TipoImagem { get; set; }

        public DbSet<GuardaCultura.Models.Duracao> Duracao { get; set; }

        public DbSet<GuardaCultura.Models.Fotografia> Fotografia { get; set; }

        public DbSet<GuardaCultura.Models.Funcao> Funcao { get; set; }

    }
}
