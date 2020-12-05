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
    public class MiradouroesController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public MiradouroesController(GuardaCulturaContext context)
        {
            _context = context;
        }

        // GET: Miradouroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Miradouro.ToListAsync());
        }

        // GET: Miradouroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miradouro = await _context.Miradouro
                .FirstOrDefaultAsync(m => m.MiradouroId == id);
            if (miradouro == null)
            {
                return NotFound();
            }

            return View(miradouro);
        }

        // GET: Miradouroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miradouroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MiradouroId,Nome,Localizacao,Coordenadas_gps,Terreno,E_Miradouro,Condicoes,Ocupacao_maxima,Descricao")] Miradouro miradouro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miradouro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miradouro);
        }

        // GET: Miradouroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miradouro = await _context.Miradouro.FindAsync(id);
            if (miradouro == null)
            {
                return NotFound();
            }
            return View(miradouro);
        }

        // POST: Miradouroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MiradouroId,Nome,Localizacao,Coordenadas_gps,Terreno,E_Miradouro,Condicoes,Ocupacao_maxima,Descricao")] Miradouro miradouro)
        {
            if (id != miradouro.MiradouroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miradouro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiradouroExists(miradouro.MiradouroId))
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
            return View(miradouro);
        }

        // GET: Miradouroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miradouro = await _context.Miradouro
                .FirstOrDefaultAsync(m => m.MiradouroId == id);
            if (miradouro == null)
            {
                return NotFound();
            }

            return View(miradouro);
        }

        // POST: Miradouroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miradouro = await _context.Miradouro.FindAsync(id);
            _context.Miradouro.Remove(miradouro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiradouroExists(int id)
        {
            return _context.Miradouro.Any(e => e.MiradouroId == id);
        }
    }
}
