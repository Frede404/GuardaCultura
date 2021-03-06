﻿using System;
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
    public class FuncaosController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public FuncaosController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: Funcaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcao.ToListAsync());
        }

        // GET: Funcaos/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcao = await _context.Funcao
                .FirstOrDefaultAsync(m => m.FuncaoId == id);
            if (funcao == null)
            {
                return NotFound();
            }

            return View(funcao);
        }

        // GET: Funcaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncaoId,FuncaoDesempenhar")] Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                // todo: validacoes adicionais antes de inserir a Funcao
                _context.Add(funcao);
                await _context.SaveChangesAsync();

                // todo: informar o utilizador, Funcao criada com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(funcao);
        }

        // GET: Funcaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcao = await _context.Funcao.FindAsync(id);
            if (funcao == null)
            {
                // todo: talvez alguem tenha apagado essa Funcao. " mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }
            return View(funcao);
        }

        // POST: Funcaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncaoId,FuncaoDesempenhar")] Funcao funcao)
        {
            if (id != funcao.FuncaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncaoExists(funcao.FuncaoId))
                    {
                        // todo: talvez alguem apagou essa Funcao
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
            // todo: informar o utilizador que a Funcao foi editada com sucesso
            return View(funcao);
        }

        // GET: Funcaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcao = await _context.Funcao
                .FirstOrDefaultAsync(m => m.FuncaoId == id);
            if (funcao == null)
            {
                // todo: talvez alguem apagou essa Funcao, informar o utilizador
                return NotFound();
            }

            return View(funcao);
        }

        // POST: Funcaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcao = await _context.Funcao.FindAsync(id);
            _context.Funcao.Remove(funcao);
            await _context.SaveChangesAsync();

            // todo: informar o utilizador que a Funcao foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool FuncaoExists(int id)
        {
            return _context.Funcao.Any(e => e.FuncaoId == id);
        }
    }
}
