using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Data;
using GuardaCultura.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GuardaCultura.Controllers
{
    public class PessoasController : Controller
    {
        private readonly GuardaCulturaContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PessoasController(GuardaCulturaContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pessoas
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            //var guardaCulturaContext = _context.Pessoa.Include(p => p.Funcao);
            return View(await _context.Pessoa.ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                //.Include(p => p.Funcao)
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Registar()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registar(RegistarPessoaViewModel PessoaInfo)
        {
            if (!ModelState.IsValid)//caso haja erro fica na mesma pagina
            {
                return View(PessoaInfo);
            }

            //cria utilizador
            string username = PessoaInfo.Email;
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user != null)//se o email ja existe
            {
                ModelState.AddModelError("Email", "Este Email ja esta registado");
                return View(PessoaInfo);
            }
            
            user = new IdentityUser(username);
            await _userManager.CreateAsync(user, PessoaInfo.Password);
            await _userManager.CreateAsync(user, "Turista");

            //cria Pessoa
            Pessoa pessoa = new Pessoa
            {
                Nome = PessoaInfo.Nome,
                Email = PessoaInfo.Email,
                Data_Nasc = PessoaInfo.Data_Nasc,
                Sexo = PessoaInfo.Sexo,
                Nacionalidade = PessoaInfo.Nacionalidade,
                Fiabilidade = 0
            };
            
            _context.Add(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AmbienteController.Ambiente),"Ambiente");
            
            //ViewData["FuncaoId"] = new SelectList(_context.Funcao, "FuncaoId", "FuncaoDesempenhar", pessoa.FuncaoId);
            //return View(pessoaInfo);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            //ViewData["FuncaoId"] = new SelectList(_context.Funcao, "FuncaoId", "FuncaoDesempenhar", pessoa.FuncaoId);
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaId,Nome,Email,Ultima_Lingua,Data_Nasc,Sexo,Nacionalidade,Fiabilidade,FuncaoId")] Pessoa pessoa)
        {
            if (id != pessoa.PessoaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaId))
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
            //ViewData["FuncaoId"] = new SelectList(_context.Funcao, "FuncaoId", "FuncaoDesempenhar", pessoa.FuncaoId);
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                //.Include(p => p.Funcao)
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistarApresentacao(RegistarPessoaViewModel PessoaInfo)
        {
            if (!ModelState.IsValid)//caso haja erro fica na mesma pagina
            {
                ModelState.AddModelError("Email", "Este Email ja esta registado");
                return RedirectToAction("Index", "Home", new { erro = "Registar" });
            }

            //cria utilizador
            string username = PessoaInfo.Email;
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user != null)//se o email ja existe
            {
                ModelState.AddModelError("Email", "Este Email ja esta registado");
                return RedirectToAction("Index", "Home", new { erro = "Registar" });
                //return View(PessoaInfo);
            }

            user = new IdentityUser(username);
            await _userManager.CreateAsync(user, PessoaInfo.Password);
            await _userManager.CreateAsync(user, "Turista");

            //cria Pessoa
            Pessoa pessoa = new Pessoa
            {
                Nome = PessoaInfo.Nome,
                Email = PessoaInfo.Email,
                Data_Nasc = PessoaInfo.Data_Nasc,
                Sexo = PessoaInfo.Sexo,
                Nacionalidade = PessoaInfo.Nacionalidade,
                Fiabilidade = 0
            };

            _context.Add(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AmbienteController.Ambiente), "Ambiente");

            //ViewData["FuncaoId"] = new SelectList(_context.Funcao, "FuncaoId", "FuncaoDesempenhar", pessoa.FuncaoId);
            //return View(pessoaInfo);
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.PessoaId == id);
        }
    }
}
