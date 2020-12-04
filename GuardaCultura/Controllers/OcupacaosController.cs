﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Data;

namespace GuardaCultura.Models
{
    public class OcupacaosController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public OcupacaosController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: Ocupacaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ocupacao.ToListAsync());
        }

        // GET: Ocupacaos/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocupacao = await _context.Ocupacao
                .FirstOrDefaultAsync(m => m.OcupacaoId == id);
            if (ocupacao == null)
            {
                return NotFound();
            }

            return View(ocupacao);
        }

        // GET: Ocupacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ocupacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OcupacaoId,Numero_pessoas,Data,MiradouroId,HoraId")] Ocupacao ocupacao)
        {
            if (ModelState.IsValid)
            {
                // todo: validacoes adicionais antes de inserir a ocupacao
                _context.Add(ocupacao);
                await _context.SaveChangesAsync();
                // todo: informar o utilizador, ocupacao criada com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(ocupacao);
        }

        // GET: Ocupacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocupacao = await _context.Ocupacao.FindAsync(id);
            if (ocupacao == null)
            {
                // todo: talvez alguem tenha apagado essa ocupacao. " mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }
            return View(ocupacao);
        }

        // POST: Ocupacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OcupacaoId,Numero_pessoas,Data,MiradouroId,HoraId")] Ocupacao ocupacao)
        {
            if (id != ocupacao.OcupacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocupacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcupacaoExists(ocupacao.OcupacaoId))
                    {
                        // todo: talvez alguem apagou essa ocupacao
                        // pergunta ao utilizador se quer criar uma nova com os mesmos dados
                        return NotFound();
                    }
                    else
                    {
                        // todo: mostrar o erro e perguntar se quer tentar outra vez
                        throw;
                    }
                }
                // todo: informar o utilizador que a ocupacao foi editada com sucesso
                return RedirectToAction(nameof(Index));
            }
            return View(ocupacao);
        }

        // GET: Ocupacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocupacao = await _context.Ocupacao
                .FirstOrDefaultAsync(m => m.OcupacaoId == id);
            if (ocupacao == null)
            {
                // todo: talvez alguem apagou essa ocupacao, informar o utilizador
                return NotFound();
            }

            return View(ocupacao);
        }

        // POST: Ocupacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocupacao = await _context.Ocupacao.FindAsync(id);
            _context.Ocupacao.Remove(ocupacao);
            await _context.SaveChangesAsync();
            // todo: informar o utilizador que a ocupacao foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool OcupacaoExists(int id)
        {
            return _context.Ocupacao.Any(e => e.OcupacaoId == id);
        }
    }
}