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
    public class DuracaosController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public DuracaosController(GuardaCulturaContext context)
        {
            _context = context;
        }

        // GET: Duracaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Duracao.ToListAsync());
        }

        // GET: Duracaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duracao = await _context.Duracao
                .FirstOrDefaultAsync(m => m.DuracaoId == id);
            if (duracao == null)
            {
                return NotFound();
            }

            return View(duracao);
        }

        // GET: Duracaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Duracaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuracaoId,HorasInicio,HorasFim")] Duracao duracao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duracao);
        }

        // GET: Duracaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duracao = await _context.Duracao.FindAsync(id);
            if (duracao == null)
            {
                return NotFound();
            }
            return View(duracao);
        }

        // POST: Duracaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DuracaoId,HorasInicio,HorasFim")] Duracao duracao)
        {
            if (id != duracao.DuracaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duracao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuracaoExists(duracao.DuracaoId))
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
            return View(duracao);
        }

        // GET: Duracaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duracao = await _context.Duracao
                .FirstOrDefaultAsync(m => m.DuracaoId == id);
            if (duracao == null)
            {
                return NotFound();
            }

            return View(duracao);
        }

        // POST: Duracaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duracao = await _context.Duracao.FindAsync(id);
            _context.Duracao.Remove(duracao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuracaoExists(int id)
        {
            return _context.Duracao.Any(e => e.DuracaoId == id);
        }
    }
}
