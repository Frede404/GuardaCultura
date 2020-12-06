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

        public AtratividadesController(GuardaCulturaContext context)// recebe bd
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
        public async Task<IActionResult> Details(int? id)// recebe id
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
        [ValidateAntiForgeryToken]// validacao de seguranca
        public async Task<IActionResult> Create([Bind("AtratividadeId,DuracaoId,EstacaoAnoId,MiradouroId")] Atratividade atratividade)
        {
            if (ModelState.IsValid)
            {
                //todo: validacoes antes de inserir a atratividade
                _context.Add(atratividade);
                await _context.SaveChangesAsync();

                //todo: informar o utilizador, atratividade criada com sucesso
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
                //todo: talvez alguem tenha apagado essa atratividade. "mostrar uma mensagem apropriada ao utilizador"
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
        public async Task<IActionResult> Edit(int id, [Bind("AtratividadeId,DuracaoId,EstacaoAnoId,MiradouroId")] Atratividade atratividade)//serve para evitar alguns ataques, so recebe campos que estejam no bind
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
                        // todo: talvez alguem apagou essa atratividade
                        // pergunta ao utilizador se quer criar uma nova com os mesmos dados
                        return NotFound();
                    }
                    else
                    {
                        // todo: mostrar o erro e perguntar se quer tentar outra vez
                        throw;
                    }
                }
                // todo: informar o utilizador que a atratividade foi editada com sucesso
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
                // todo: talvez alguem apagou essa atratividade, informar o utilizador
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

            // todo: informar o utilizador que a atratividade foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool AtratividadeExists(int id)
        {
            return _context.Atratividade.Any(e => e.AtratividadeId == id);
        }
    }
}
