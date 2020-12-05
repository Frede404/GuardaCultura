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
    public class FotografiasController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public FotografiasController(GuardaCulturaContext context)
        {
            _context = context;
        }

        // GET: Fotografias
        public async Task<IActionResult> Index()
        {
            var guardaCulturaContext = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem);
            return View(await guardaCulturaContext.ToListAsync());
        }

        // GET: Fotografias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografia
                .Include(f => f.EstacaoAno)
                .Include(f => f.Miradouro)
                .Include(f => f.Pessoa)
                .Include(f => f.TipoImagem)
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografia == null)
            {
                return NotFound();
            }

            return View(fotografia);
        }

        // GET: Fotografias/Create
        public IActionResult Create()
        {
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao");
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps");
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Email");
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao");
            return View();
        }

        // POST: Fotografias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FotografiaId,Nome,Data_imagem,Classificacao,Foto,EstacaoAnoId,PessoaId,MiradouroId,TipoImagemId")] Fotografia fotografia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fotografia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Email", fotografia.PessoaId);
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao", fotografia.TipoImagemId);
            return View(fotografia);
        }

        // GET: Fotografias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografia.FindAsync(id);
            if (fotografia == null)
            {
                return NotFound();
            }
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Email", fotografia.PessoaId);
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao", fotografia.TipoImagemId);
            return View(fotografia);
        }

        // POST: Fotografias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FotografiaId,Nome,Data_imagem,Classificacao,Foto,EstacaoAnoId,PessoaId,MiradouroId,TipoImagemId")] Fotografia fotografia)
        {
            if (id != fotografia.FotografiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fotografia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotografiaExists(fotografia.FotografiaId))
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
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Coordenadas_gps", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Email", fotografia.PessoaId);
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao", fotografia.TipoImagemId);
            return View(fotografia);
        }

        // GET: Fotografias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografia
                .Include(f => f.EstacaoAno)
                .Include(f => f.Miradouro)
                .Include(f => f.Pessoa)
                .Include(f => f.TipoImagem)
                .FirstOrDefaultAsync(m => m.FotografiaId == id);
            if (fotografia == null)
            {
                return NotFound();
            }

            return View(fotografia);
        }

        // POST: Fotografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotografia = await _context.Fotografia.FindAsync(id);
            _context.Fotografia.Remove(fotografia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FotografiaExists(int id)
        {
            return _context.Fotografia.Any(e => e.FotografiaId == id);
        }
    }
}
