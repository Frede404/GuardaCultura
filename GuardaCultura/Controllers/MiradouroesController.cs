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

        public MiradouroesController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: Miradouroes
        public async Task<IActionResult> Index()// lista dos Miradouros
        {
            return View(await _context.Miradouro.ToListAsync());// alteracoes assincronas
        }

        // GET: Miradouroes/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
        {
            if (id == null)
            {
                return NotFound();
            }

            var miradouro = await _context.Miradouro
                .FirstOrDefaultAsync(m => m.MiradouroId == id);// um registo ou o default que é null
            if (miradouro == null)
            {
                // todo: talvez alguem tenha apagado esse miradouro. "Mostrar uma mensagem apropriada ao utilizador"
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
        public async Task<IActionResult> Create([Bind("MiradouroId,Nome,Localizacao,Coordenadas_gps,Terreno,E_Miradouro,Condicoes,Ocupacao_maxima,Descricao")] Miradouro miradouro)//serve para evitar alguns ataques, só recebe campos que estejam no Bind
        {
            if (ModelState.IsValid)
            {
                // todo: validacoes adicionais antes de inserir o miradouro
                if (!miradouro.E_Miradouro)
                {
                    miradouro.Ocupacao_maxima = 100;
                }
                _context.Add(miradouro);
                await _context.SaveChangesAsync();

                // todo: informar o utilizador, miradouro criado com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(miradouro);
        }

        // GET: Miradouroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // todo: talvez alguem tenha apagado esse miradouro. " mostrar uma mensagem apropriada ao utilizador"
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
                        // todo: talvez alguem apagou esse miradouro
                        // pergunta ao utilizador se quer criar um novo com os mesmos dados
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
            // todo: informar o utilizador que o miradouro foi editado com sucesso
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
                // todo: talvez alguem apagou esse miradouro, informar o utilizador
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

            // todo: informar o utilizador que o miradouro foi apagado com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool MiradouroExists(int id)
        {
            return _context.Miradouro.Any(e => e.MiradouroId == id);
        }
    }
}
