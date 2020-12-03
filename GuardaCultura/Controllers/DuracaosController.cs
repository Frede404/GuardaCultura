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

        public DuracaosController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: Duracaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Duracao.ToListAsync());
        }

        // GET: Duracaos/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
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
                // todo: validacoes adicionais antes de inserir a duracao
                _context.Add(duracao);
                await _context.SaveChangesAsync();
                // todo: informar o utilizador, duracao criada com sucesso
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
                // todo: talvez alguem tenha apagado essa duracao. " mostrar uma mensagem apropriada ao utilizador"
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
                        // todo: talvez alguem apagou essa duracao
                        // pergunta ao utilizador se quer criar uma nova com os mesmos dados
                        return NotFound();
                    }
                    else
                    {
                        // todo: mostrar o erro e perguntar se quer tentar outra vez
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
                // todo: talvez alguem apagou essa duracao, informar o utilizador
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
            // todo: informar o utilizador que a duracao foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool DuracaoExists(int id)
        {
            return _context.Duracao.Any(e => e.DuracaoId == id);
        }
    }
}
