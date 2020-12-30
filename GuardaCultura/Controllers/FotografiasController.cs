using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuardaCultura.Data;
using GuardaCultura.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GuardaCultura.Controllers
{
    public class FotografiasController : Controller
    {
        private readonly GuardaCulturaContext _context;

        private static string auxordenar = "";
        private static int auxdirecaoordena = 0;
        private static int auxaprovacao = 0;

        public FotografiasController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        // GET: Fotografias
        public async Task<IActionResult> Index(int page = 1, string ordenar= "FotografiaId", int direcaoordena=1, int aprovacao=1)
        {
            if (direcaoordena == 0)
            {
                direcaoordena = auxdirecaoordena;
            }
            else
            {
                auxdirecaoordena = direcaoordena;
            }

            if (aprovacao == 0)
            {
                aprovacao = auxaprovacao;
            }
            else
            {
                auxaprovacao = aprovacao;
            }

            if (ordenar == "0" || ordenar == "")
            {
                ordenar = auxordenar;
            }
            else
            {
                auxordenar = ordenar;
            }

            if (aprovacao == 1)
            {
                var pagination = new PagingInfoFotografias
                {
                    CurrentPage = page,
                    PageSize = PagingInfoFotografias.TAM_PAGINA,
                    TotalItems = _context.Fotografia.Count()
                };

                if (direcaoordena == 1)
                {
                    return View(
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .OrderBy(p => EF.Property<Object>(p, ordenar))
                                .Skip((page - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
                else
                {
                    return View(
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .OrderByDescending(p => EF.Property<Object>(p, ordenar))
                                .Skip((page - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
            }
            else
            {
                bool auxaprovado;
                if (aprovacao == 2)
                { 
                    auxaprovado = true;
                }
                else
                {
                    auxaprovado = false;
                }

                var pagination = new PagingInfoFotografias
                {
                    CurrentPage = page,
                    PageSize = PagingInfoFotografias.TAM_PAGINA,
                    TotalItems = _context.Fotografia
                        .Where(p => p.Aprovada == auxaprovado)
                        .Count()
                };

                if (direcaoordena == 1)
                {
                    return View(
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .Where(p => p.Aprovada == auxaprovado)
                                .OrderBy(p => EF.Property<Object>(p, ordenar))
                                .Skip((page - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
                else
                {
                    return View(
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .Where(p => p.Aprovada == auxaprovado)
                                .OrderByDescending(p => EF.Property<Object>(p, ordenar))
                                .Skip((page - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
            }
            /*var guardaCulturaContext = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem);
            return View(await guardaCulturaContext.ToListAsync());*/
        }

        // GET: Fotografias/Details/5
        public async Task<IActionResult> Details(int? id)// recebe o id
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
                // todo: talvez alguem tenha apagado essa fotografia. "Mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }

            return View(fotografia);
        }

        // GET: Fotografias/Create
        public IActionResult Create()
        {
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao");
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Nome");
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Nome");
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao");
            return View();
        }

        // POST: Fotografias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FotografiaId,Nome,Data_imagem,Classificacao,EstacaoAnoId,PessoaId,MiradouroId,TipoImagemId")] Fotografia fotografia, List<IFormFile> Foto)//serve para evitar alguns ataques, só recebe campos que estejam no Bind
        {
            if (ModelState.IsValid)
            {
                //conversao da imagem para binario
                foreach (var item in Foto)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            fotografia.Foto = stream.ToArray();
                        }
                    }
                }

                int funcao = _context.Pessoa
                            .Find(fotografia.PessoaId)
                            .FuncaoId;

                if (funcao==1)
                {
                    fotografia.Aprovada = true;
                }
                // todo: validacoes adicionais antes de inserir a foto
                fotografia.Classificacao = 3.5f;
                _context.Add(fotografia);
                await _context.SaveChangesAsync();

                // todo: informar o utilizador, foto inserida com sucesso
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Nome", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Nome", fotografia.PessoaId);
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
                // todo: talvez alguem tenha apagado essa Hora. " mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Nome", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Nome", fotografia.PessoaId);
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
                        // todo: talvez alguem apagou essa Foto
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
            ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Nome", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Nome", fotografia.PessoaId);
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao", fotografia.TipoImagemId);

            // todo: informar o utilizador que a foto foi editada com sucesso
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
                // todo: talvez alguem apagou essa foto, informar o utilizador
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

            // todo: informar o utilizador que a foto foi apagada com sucesso
            return RedirectToAction(nameof(Index));
        }

        private bool FotografiaExists(int id)
        {
            return _context.Fotografia.Any(e => e.FotografiaId == id);
        }

        public async Task<IActionResult> Aprova(int? id, int page=1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotografia = await _context.Fotografia.FindAsync(id);
            if (fotografia == null)
            {
                // todo: talvez alguem tenha apagado essa Hora. " mostrar uma mensagem apropriada ao utilizador"
                return NotFound();
            }
            
            fotografia.Aprovada = !fotografia.Aprovada;

            try
            {
                _context.Update(fotografia);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotografiaExists(fotografia.FotografiaId))
                {
                    // todo: talvez alguem apagou essa Foto
                    // pergunta ao utilizador se quer criar um novo com os mesmos dados
                    return NotFound();
                }
                else
                {
                    // todo: mostrar o erro e perguntar se quer tentar outra vez
                    throw;
                }
            }
            //return RedirectToAction(nameof(Index(0,"0",0,0)));
            if (auxaprovacao == 1)
            {
                int tamanho = PagingInfoFotografias.TAM_PAGINA;
                int totalitems = _context.Fotografia.Count();
                int paginaatual;
                if((int)Math.Ceiling((double)totalitems / tamanho) < page)
                {
                    paginaatual = page - 1;
                }
                else
                {
                    paginaatual = page;
                }

                var pagination = new PagingInfoFotografias
                {
                    CurrentPage = paginaatual,
                    PageSize = tamanho,
                    TotalItems = totalitems
                };

                if (auxdirecaoordena == 1)
                {
                    return View("Index",
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .OrderBy(p => EF.Property<Object>(p, auxordenar))
                                .Skip((paginaatual - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
                else
                {
                    return View("Index",
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .OrderByDescending(p => EF.Property<Object>(p, auxordenar))
                                .Skip((paginaatual - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
            }
            else
            {
                bool auxaprovado;
                if (auxaprovacao == 2)
                {
                    auxaprovado = true;
                }
                else
                {
                    auxaprovado = false;
                }

                int tamanho = PagingInfoFotografias.TAM_PAGINA;
                int totalitems = _context.Fotografia
                        .Where(p => p.Aprovada == auxaprovado)
                        .Count();
                int paginaatual;
                if ((int)Math.Ceiling((double)totalitems / tamanho) < page)
                {
                    paginaatual = page - 1;
                }
                else
                {
                    paginaatual = page;
                }

                var pagination = new PagingInfoFotografias
                {
                    CurrentPage = paginaatual,
                    PageSize = tamanho,
                    TotalItems = totalitems
                };

                if (auxdirecaoordena == 1)
                {
                    return View("Index",
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .Where(p => p.Aprovada == auxaprovado)
                                .OrderBy(p => EF.Property<Object>(p, auxordenar))
                                .Skip((paginaatual - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
                else
                {
                    return View("Index",
                        new ListaFotografias
                        {
                            Fotografias = _context.Fotografia.Include(f => f.EstacaoAno).Include(f => f.Miradouro).Include(f => f.Pessoa).Include(f => f.TipoImagem)
                                .Where(p => p.Aprovada == auxaprovado)
                                .OrderByDescending(p => EF.Property<Object>(p, auxordenar))
                                .Skip((paginaatual - 1) * pagination.PageSize)
                                .Take(pagination.PageSize),
                            pagination = pagination
                        }
                    );
                }
            }

            /*ViewData["EstacaoAnoId"] = new SelectList(_context.EstacaoAno, "EstacaoAnoId", "Nome_estacao", fotografia.EstacaoAnoId);
            ViewData["MiradouroId"] = new SelectList(_context.Miradouro, "MiradouroId", "Nome", fotografia.MiradouroId);
            ViewData["PessoaId"] = new SelectList(_context.Set<Pessoa>(), "PessoaId", "Nome", fotografia.PessoaId);
            ViewData["TipoImagemId"] = new SelectList(_context.TipoImagem, "TipoImagemId", "Descricao", fotografia.TipoImagemId);
            */
        }
    }
}
