using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Data;
using GuardaCultura.Models;

namespace GuardaCultura.Controllers
{
    public class AtratividadesController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public AtratividadesController(GuardaCulturaContext context)
        {
            _context = context;
        }

        // GET: Atratividades
        public async Task<IActionResult> Index()
        {
            var guardaCulturaContext = _context.Atratividade.Include(a => a.Duracao).Include(a => a.EstacaoAno).Include(a => a.Miradouro);
            return View(await guardaCulturaContext.ToListAsync());
        }

        // GET: Atratividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atratividade = await _context.Atratividade
                .Include(a => a.Duracao)
                .Include(a => a.EstacaoAno)
                .Include(a => a.Miradouro)
                .FirstOrDefaultAsync(m => m.AtratividadeId == id);
            if (atratividade == null)
            {
                return NotFound();
            }

            return View(atratividade);
        }

        // GET: Atratividades/Create
        public IActionResult Create()
        {
            ViewData["DuracaoId"] = new SelectList(_context.Duracao, "DuracaoId", "DuracaoId");
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao");
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps");
            return View();
        }

        // POST: Atratividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AtratividadeId,DuracaoId,EstacaoAnoId,MiradouroId")] Atratividade atratividade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atratividade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DuracaoId"] = new SelectList(_context.Duracao, "DuracaoId", "DuracaoId", atratividade.DuracaoId);
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", atratividade.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", atratividade.MiradouroId);
            return View(atratividade);
        }

        // GET: Atratividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atratividade = await _context.Atratividade.FindAsync(id);
            if (atratividade == null)
            {
                return NotFound();
            }
            ViewData["DuracaoId"] = new SelectList(_context.Duracao, "DuracaoId", "DuracaoId", atratividade.DuracaoId);
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", atratividade.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", atratividade.MiradouroId);
            return View(atratividade);
        }

        // POST: Atratividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AtratividadeId,DuracaoId,EstacaoAnoId,MiradouroId")] Atratividade atratividade)
        {
            if (id != atratividade.AtratividadeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atratividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtratividadeExists(atratividade.AtratividadeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DuracaoId"] = new SelectList(_context.Duracao, "DuracaoId", "DuracaoId", atratividade.DuracaoId);
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", atratividade.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", atratividade.MiradouroId);
            return View(atratividade);
        }

        // GET: Atratividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atratividade = await _context.Atratividade
                .Include(a => a.Duracao)
                .Include(a => a.EstacaoAno)
                .Include(a => a.Miradouro)
                .FirstOrDefaultAsync(m => m.AtratividadeId == id);
            if (atratividade == null)
            {
                return NotFound();
            }

            return View(atratividade);
        }

        // POST: Atratividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atratividade = await _context.Atratividade.FindAsync(id);
            _context.Atratividade.Remove(atratividade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtratividadeExists(int id)
        {
            return _context.Atratividade.Any(e => e.AtratividadeId == id);
        }
    }
}
