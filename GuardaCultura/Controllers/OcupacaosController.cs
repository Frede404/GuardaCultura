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
    public class OcupacaosController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public OcupacaosController(GuardaCulturaContext context)
        {
            _context = context;
        }

        // GET: Ocupacaos
        public async Task<IActionResult> Index()
        {
            var guardaCulturaContext = _context.Ocupacao.Include(o => o.Hora).Include(o => o.Miradouro);
            return View(await guardaCulturaContext.ToListAsync());
        }

        // GET: Ocupacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocupacao = await _context.Ocupacao
                .Include(o => o.Hora)
                .Include(o => o.Miradouro)
                .FirstOrDefaultAsync(m => m.OcupacaoId == id);
            if (ocupacao == null)
            {
                return NotFound();
            }

            return View(ocupacao);
        }

        // GET: Ocupacaos/Create
        public IActionResult Create()
        {
            ViewData["HoraId"] = new SelectList(_context.Hora, "HoraId", "HoraId");
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps");
            return View();
        }

        // POST: Ocupacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OcupacaoId,Numero_pessoas,Data,MiradouroId,HoraId")] Ocupacao ocupacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocupacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoraId"] = new SelectList(_context.Hora, "HoraId", "HoraId", ocupacao.HoraId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", ocupacao.MiradouroId);
            return View(ocupacao);
        }

        // GET: Ocupacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocupacao = await _context.Ocupacao.FindAsync(id);
            if (ocupacao == null)
            {
                return NotFound();
            }
            ViewData["HoraId"] = new SelectList(_context.Hora, "HoraId", "HoraId", ocupacao.HoraId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", ocupacao.MiradouroId);
            return View(ocupacao);
        }

        // POST: Ocupacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OcupacaoId,Numero_pessoas,Data,MiradouroId,HoraId")] Ocupacao ocupacao)
        {
            if (id != ocupacao.OcupacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocupacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcupacaoExists(ocupacao.OcupacaoId))
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
            ViewData["HoraId"] = new SelectList(_context.Hora, "HoraId", "HoraId", ocupacao.HoraId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", ocupacao.MiradouroId);
            return View(ocupacao);
        }

        // GET: Ocupacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocupacao = await _context.Ocupacao
                .Include(o => o.Hora)
                .Include(o => o.Miradouro)
                .FirstOrDefaultAsync(m => m.OcupacaoId == id);
            if (ocupacao == null)
            {
                return NotFound();
            }

            return View(ocupacao);
        }

        // POST: Ocupacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocupacao = await _context.Ocupacao.FindAsync(id);
            _context.Ocupacao.Remove(ocupacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcupacaoExists(int id)
        {
            return _context.Ocupacao.Any(e => e.OcupacaoId == id);
        }
    }
}
