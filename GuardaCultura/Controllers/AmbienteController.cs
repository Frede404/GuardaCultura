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
        public async Task<IActionResult> MiradourosAsync(int page = 1)
        {
            var paginacao = new PagingInfoPaginaMiradouros
            {
                CurrentPage = page,
                PageSize = PagingInfoPaginaMiradouros.TAM_PAGINA,
                TotalItems = _context.Miradouro
                .Where(p => p.Ativo == true)
                .Where(p => p.E_Miradouro == true).Count()
            };
            return View(
                new ListaPaginaMiradouros
                {
                    Miradouros = _context.Miradouro
                    .OrderBy(p => p.MiradouroId)
                    .Where(p => p.Ativo == true)
                    .Where(p => p.E_Miradouro == true)
                    .Skip((page - 1) * paginacao.PageSize),
                    Fotografias = _context.Fotografia,
                    pagination = paginacao
                }
                );
            //return View(await _context.Miradouro.ToListAsync());
        }
        public IActionResult Sobre()
        {
            return View();
        }
    }
}
