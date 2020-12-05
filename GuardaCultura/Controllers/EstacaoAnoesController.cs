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
    public class EstacaoAnoesController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public EstacaoAnoesController(GuardaCulturaContext context)
        {
            _context = context;
        }

        // GET: EstacaoAnoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstacaoAno.ToListAsync());
        }

        // GET: EstacaoAnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacaoAno = await _context.EstacaoAno
                .FirstOrDefaultAsync(m => m.EstacaoAnoId == id);
            if (estacaoAno == null)
            {
                return NotFound();
            }

            return View(estacaoAno);
        }

        // GET: EstacaoAnoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstacaoAnoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstacaoAnoId,Nome_estacao")] EstacaoAno estacaoAno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estacaoAno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estacaoAno);
        }

        // GET: EstacaoAnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacaoAno = await _context.EstacaoAno.FindAsync(id);
            if (estacaoAno == null)
            {
                return NotFound();
            }
            return View(estacaoAno);
        }

        // POST: EstacaoAnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstacaoAnoId,Nome_estacao")] EstacaoAno estacaoAno)
        {
            if (id != estacaoAno.EstacaoAnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estacaoAno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstacaoAnoExists(estacaoAno.EstacaoAnoId))
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
            return View(estacaoAno);
        }

        // GET: EstacaoAnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacaoAno = await _context.EstacaoAno
                .FirstOrDefaultAsync(m => m.EstacaoAnoId == id);
            if (estacaoAno == null)
            {
                return NotFound();
            }

            return View(estacaoAno);
        }

        // POST: EstacaoAnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estacaoAno = await _context.EstacaoAno.FindAsync(id);
            _context.EstacaoAno.Remove(estacaoAno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstacaoAnoExists(int id)
        {
            return _context.EstacaoAno.Any(e => e.EstacaoAnoId == id);
        }
    }
}
