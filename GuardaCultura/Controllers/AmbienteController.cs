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

            var Maxclassificacao = from foto in _context.Fotografia
                         where foto.Aprovada==true
                         group foto by foto.MiradouroId into fotogroup
                         orderby fotogroup.Key
                         select new
                         {
                             miradouroId = fotogroup.Key,
                             melhorclassificacao = fotogroup.Max(p => p.Classificacao)
                         };

            var FotosMax = (from foto in _context.Fotografia
                          join testes in Maxclassificacao on new {a= foto.MiradouroId, b=foto.Classificacao } equals new {a= testes.miradouroId, b=testes.melhorclassificacao }
                          where foto.Aprovada == true
                          orderby foto.MiradouroId
                          select new 
                          {
                              Nome=foto.Nome,
                              MiradoutoId=foto.MiradouroId,
                              Foto=foto.Foto,
                              Classificacao=foto.Classificacao
                          });
            
            return View(
                new ListaPaginaMiradouros
                {
                    Miradouros = _context.Miradouro
                    .OrderBy(p => p.MiradouroId)
                    .Where(p => p.Ativo == true)
                    .Where(p => p.E_Miradouro == true)
                    .Skip((page - 1) * paginacao.PageSize)
                    .Take(paginacao.PageSize),

                    fotoapresentacao= FotosMax.Select(p => new Fotografia
                    {
                        Nome = p.Nome,
                        Foto = p.Foto,
                        MiradouroId = p.MiradoutoId,
                        Classificacao = p.Classificacao
                    }),

                    Fotografias= _context.Fotografia
                    .OrderBy(p => p.MiradouroId)
                    .OrderByDescending(p => p.Classificacao)
                    .Where(p => p.Aprovada == true),

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
