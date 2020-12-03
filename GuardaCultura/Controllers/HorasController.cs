using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Data;

namespace GuardaCultura.Models
{
    public class HorasController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public HorasController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: Horas
        public async Task<IActionResult> Index()// lista dos Horas
        {
            return View(await _context.Hora.ToListAsync());
        }

        // GET: Horas/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
        {
            if (id == null)
            {
                return NotFound();
            }

            var hora = await _context.Hora
                .FirstOrDefaultAsync(m => m.HoraId == id);// um registo ou o default que é null
            if (hora == null)
            {
                // todo: talvez alguem tenha apagado essa Hora. "Mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }

            return View(hora);
        }

        // GET: Horas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Horas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]// validacao de seguranca
        public async Task<IActionResult> Create([Bind("HoraId,Horas")] Hora hora)//serve para evitar alguns ataques,
                                                                                 // só recebe campos que estejam no Bind
        {
            if (ModelState.IsValid)
            {
                // todo: validacoes adicionais antes de inserir a Hora
                _context.Add(hora);
                await _context.SaveChangesAsync();
                // todo: informar o utilizador, Hora criada com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(hora);
        }

        // GET: Horas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hora = await _context.Hora.FindAsync(id);
            if (hora == null)
            {
                // todo: talvez alguem tenha apagado essa Hora. " mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }
            return View(hora);
        }

        // POST: Horas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoraId,Horas")] Hora hora)
        {
            if (id != hora.HoraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoraExists(hora.HoraId))
                    {
                        // todo: talvez alguem apagou essa Hora
                        // pergunta ao utilizador se quer criar um novo com os mesmos dados
                        return NotFound();
                    }
                    else
                    {
                        // todo: mostrar o erro e perguntar se quer tentar outra vez
                        throw;
                    }
                }
                // todo: informar o utilizador que a Hora foi editada com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(hora);
        }

        // GET: Horas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hora = await _context.Hora
                .FirstOrDefaultAsync(m => m.HoraId == id);
            if (hora == null)
            {
                // todo: talvez alguem apagou essa Hora, informar o utilizador
                return NotFound();
            }

            return View(hora);
        }

        // POST: Horas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hora = await _context.Hora.FindAsync(id);
            _context.Hora.Remove(hora);
            await _context.SaveChangesAsync();
            // todo: informar o utilizador que a Hora foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool HoraExists(int id)
        {
            return _context.Hora.Any(e => e.HoraId == id);
        }
    }
}
