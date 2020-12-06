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
    public class TipoImagemsController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public TipoImagemsController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: TipoImagems
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoImagem.ToListAsync());
        }

        // GET: TipoImagems/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoImagem = await _context.TipoImagem
                .FirstOrDefaultAsync(m => m.TipoImagemId == id);
            if (tipoImagem == null)
            {
                return NotFound();
            }

            return View(tipoImagem);
        }

        // GET: TipoImagems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoImagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoImagemId,Descricao")] TipoImagem tipoImagem)
        {
            if (ModelState.IsValid)
            {
                // todo: validacoes adicionais antes de inserir a Tipo de imagem
                _context.Add(tipoImagem);
                await _context.SaveChangesAsync();

                // todo: informar o utilizador, Tipo de imagem criada com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(tipoImagem);
        }

        // GET: TipoImagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoImagem = await _context.TipoImagem.FindAsync(id);
            if (tipoImagem == null)
            {
                // todo: talvez alguem tenha apagado esse Tipo de imagem. " mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }
            return View(tipoImagem);
        }

        // POST: TipoImagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoImagemId,Descricao")] TipoImagem tipoImagem)
        {
            if (id != tipoImagem.TipoImagemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoImagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoImagemExists(tipoImagem.TipoImagemId))
                    {
                        // todo: talvez alguem apagou esse Tipo de imagem
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
            return View(tipoImagem);
        }

        // GET: TipoImagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoImagem = await _context.TipoImagem
                .FirstOrDefaultAsync(m => m.TipoImagemId == id);
            if (tipoImagem == null)
            {
                // todo: talvez alguem apagou esse Tipo de imagem, informar o utilizador
                return NotFound();
            }

            return View(tipoImagem);
        }

        // POST: TipoImagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoImagem = await _context.TipoImagem.FindAsync(id);
            _context.TipoImagem.Remove(tipoImagem);
            await _context.SaveChangesAsync();

            // todo: informar o utilizador que o Tipo de imagem foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool TipoImagemExists(int id)
        {
            return _context.TipoImagem.Any(e => e.TipoImagemId == id);
        }
    }
}
