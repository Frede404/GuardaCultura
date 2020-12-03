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
    }
}
