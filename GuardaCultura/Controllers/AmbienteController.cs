using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardaCultura.Data;
using GuardaCultura.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardaCultura.Controllers
{
    public class AmbienteController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public AmbienteController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        public IActionResult Ambiente()
        {
            return View();
        }
        public IActionResult QualidadeAr()
        {
            return View();
        }
        public IActionResult Paisagens()
        {
            return View();
        }
        public async Task<IActionResult> MiradourosAsync()
        {
            return View(await _context.Miradouro.ToListAsync());
        }
        public IActionResult Sobre()
        {
            return View();
        }
    }
}
