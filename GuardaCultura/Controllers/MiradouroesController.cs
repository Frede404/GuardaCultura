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

        private static int auxEmiradouro=0;
        private static int auxEstado=0;

        public MiradouroesController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }


        // GET: Miradouroes
        public async Task<IActionResult> Index(int page = 1, int e_miradouro = 1, int estado = 1)// lista dos Miradouros
        {
            //return View(await _context.Miradouro.ToListAsync());// alteracoes assincronas
            
            Boolean ativo;
            Boolean miradouroPaisagem;

            if (e_miradouro == 0)
            {
                e_miradouro = auxEmiradouro;
            }
            else
            {
                auxEmiradouro = e_miradouro;
            }

            if (estado == 0)
            {
                estado = auxEstado;
            }
            else
            {
                auxEstado = estado;
            }

            if (e_miradouro==1)
            {
                if (estado == 1)//mostra tudo
                {
                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = page,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro.Count()
                    };

                    return View(
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            .Skip((page - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao
                        }
                        );
                }
                else
                {
                    if (estado == 2)
                    {
                        ativo = true;//todos ativos
                    }
                    else
                    {
                        ativo = false;//todos desativos
                    }

                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = page,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro
                        .Where(p => p.Ativo == ativo)
                        .Count()
                    };

                    return View(
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            .Where(p => p.Ativo == ativo)
                            .Skip((page - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao,
                        }
                        );

                }
            }
            else//ou miradouro ou paisagem
            {
                if (e_miradouro == 2)
                {
                    miradouroPaisagem = true;
                }
                else
                {
                    miradouroPaisagem = false;
                }

                if (estado == 1)//ativos e desativos
                {
                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = page,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro
                        //.Where(p => p.Ativo ==)
                        .Where(p => p.E_Miradouro == miradouroPaisagem)
                        .Count()
                    };

                    return View(
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            //.Where(p => p.Ativo ==)
                            .Where(p => p.E_Miradouro == miradouroPaisagem)//miradouros
                            //.Where(p => p.E_Miradouro==false)//so paisagens
                            .Skip((page - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao
                        }
                        );
                }
                else
                {
                    if (estado == 2)
                    {
                        ativo = true;//paisagens ou miradouros ativos
                    }
                    else
                    {
                        ativo = false;//paisagens ou miradouros desativos
                    }

                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = page,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro
                        .Where(p => p.Ativo == ativo)
                        .Where(p => p.E_Miradouro == miradouroPaisagem)
                        .Count()
                    };

                    return View(
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            .Where(p => p.Ativo == ativo)
                            .Where(p => p.E_Miradouro == miradouroPaisagem)//miradouros
                                                                     //.Where(p => p.E_Miradouro==false)//so paisagens
                            .Skip((page - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao
                        }
                        );
                }
            }
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
        public async Task<IActionResult> Create([Bind("MiradouroId,Nome,Localizacao,Latitude_DD,Longitude_DD,Latitude_DMS,Longitude_DMS,Terreno,E_Miradouro,Condicoes,Ocupacao_maxima,Descricao")] Miradouro miradouro,
                                                        int grausLat, float minutoLat, float segundosLat, char DirNS, int grausLong, float minutosLong, float segundosLong, char DirWE, string Coordenadas, double latiDD, double longDD )//serve para evitar alguns ataques, só recebe campos que estejam no Bind
        {

            
            if (Coordenadas == "DMS")
            {   
                miradouro.Latitude_DMS = string.Concat(grausLat, "º", minutoLat ,"'", segundosLat ,"''", DirNS);
                miradouro.Longitude_DMS = string.Concat(grausLong, "º", minutosLong, "'", segundosLong, "''", DirWE);
                
                //latitude DD
                int grausLatitude = (int) grausLat;
                int minutosLatitude = (int) minutoLat;
                float segundosLatitude = (float) segundosLat;
                double LatDD = grausLatitude + (minutosLatitude / 60) + (segundosLatitude / 3600);
                if (DirNS == 'S')
                {
                    LatDD = LatDD * -1;

                    LatDD = Math.Round(LatDD, 6);
                    
                }
                miradouro.Latitude_DD = "" + LatDD;
                
                //longitude DD
                int grausLongitude = (int)grausLong;
                int minutosLongitude = (int)minutosLong;
                float segundosLongitude = (float)segundosLong;
                double LongDD = grausLongitude + (minutosLongitude / 60) + (segundosLongitude / 3600);
                if (DirWE == 'W')
                {
                    LongDD = LongDD * -1;
                    LongDD = Math.Round(LongDD, 6);
                }
                miradouro.Longitude_DD = "" + LongDD;
                 
            }

            
            if (Coordenadas == "DD")
            {
                double latDoubleDMS = (double)latiDD;
                int grausFinalDMS = (int) Math.Truncate(latiDD);
                //float auxminutos = (latDoubleDMS - grausFinalDMS) * 60);
                int minutosFinalDMS = (int) Math.Truncate((latDoubleDMS - grausFinalDMS) * 60);
                float segundosFinalDMS = (float) ((latDoubleDMS - grausFinalDMS )* 60)  % 100;
                //segundosFinalDMS = (float)Math.Truncate((latDoubleDMS - grausFinalDMS * 60) - minutosFinalDMS);
                char dirNS;
                if(latDoubleDMS < 0)
                {
                    dirNS = 'S';
                }
                else
                {
                    dirNS = 'N';
                }
                miradouro.Latitude_DMS = "" + grausFinalDMS + "º" + minutosFinalDMS + "'" + segundosFinalDMS + "''" + dirNS;


                double longDMS = (double) longDD;


                //converteDD();
            }

            if (ModelState.IsValid)
            {
                // todo: validacoes adicionais antes de inserir o miradouro
                //var coordenadas = formataCoordenadas(miradouro.Coordenadas_gps);

                if (!miradouro.E_Miradouro)
                {
                    miradouro.Ocupacao_maxima = -1;
                }

                miradouro.Ativo = true;

                _context.Add(miradouro);
                await _context.SaveChangesAsync();

                // todo: informar o utilizador, miradouro criado com sucesso
                ViewData["sucess"] = "Miradouro criado com sucesso.";
                return RedirectToAction(nameof(Index));
                
            }
            //ViewData["error"] = "Falha ao criar o miradouro!";
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

            if (!miradouro.E_Miradouro)
            {
                miradouro.Ocupacao_maxima = -1;
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
            /*_context.Miradouro.Remove(miradouro);
            await _context.SaveChangesAsync();

            // todo: informar o utilizador que o miradouro foi apagado com sucesso
            return RedirectToAction(nameof(Index));*/

            if (id != miradouro.MiradouroId)
            {
                return NotFound();
            }

            if (miradouro.Ativo)
            {
                miradouro.Ativo = false;
            }
            else
            {
                miradouro.Ativo = true;
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

        private bool MiradouroExists(int id)
        {
            return _context.Miradouro.Any(e => e.MiradouroId == id);
        }

        public async Task<IActionResult> Ativar(int? id, int page=1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miradouro = await _context.Miradouro.FindAsync(id);
            if (miradouro == null)
            {
                return NotFound();
            }

            miradouro.Ativo = !miradouro.Ativo;

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
            //return RedirectToAction(nameof(Index));

            Boolean ativo;
            Boolean miradouroPaisagem;
            if (auxEmiradouro == 1)
            {
                if (auxEstado == 1)//mostra tudo
                {
                    int tamanho = PagingInfoFotografias.TAM_PAGINA;
                    int totalitems = _context.Miradouro.Count();
                    int paginaatual;
                    if ((int)Math.Ceiling((double)totalitems / tamanho) < page)
                    {
                        paginaatual = page - 1;
                    }
                    else
                    {
                        paginaatual = page;
                    }

                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = paginaatual,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro.Count()
                    };

                    return View("Index",
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            .Skip((paginaatual - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao
                        }
                        );
                }
                else
                {
                    if (auxEstado == 2)
                    {
                        ativo = true;//todos ativos
                    }
                    else
                    {
                        ativo = false;//todos desativos
                    }

                    int tamanho = PagingInfoFotografias.TAM_PAGINA;
                    int totalitems = _context.Miradouro
                        .Where(p => p.Ativo == ativo).Count();
                    int paginaatual;
                    if ((int)Math.Ceiling((double)totalitems / tamanho) < page)
                    {
                        paginaatual = page - 1;
                    }
                    else
                    {
                        paginaatual = page;
                    }

                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = paginaatual,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro
                        .Where(p => p.Ativo == ativo)
                        .Count()
                    };

                    return View("Index",
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            .Where(p => p.Ativo == ativo)
                            .Skip((paginaatual - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao,
                        }
                        );

                }
            }
            else//ou miradouro ou paisagem
            {
                if (auxEmiradouro == 2)
                {
                    miradouroPaisagem = true;
                }
                else
                {
                    miradouroPaisagem = false;
                }


                if (auxEstado == 1)//ativos e desativos
                {
                    int tamanho = PagingInfoFotografias.TAM_PAGINA;
                    int totalitems = _context.Miradouro
                        .Where(p => p.E_Miradouro == miradouroPaisagem).Count();
                    int paginaatual;
                    if ((int)Math.Ceiling((double)totalitems / tamanho) < page)
                    {
                        paginaatual = page - 1;
                    }
                    else
                    {
                        paginaatual = page;
                    }

                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = paginaatual,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro
                        //.Where(p => p.Ativo ==)
                        .Where(p => p.E_Miradouro == miradouroPaisagem)
                        .Count()
                    };

                    return View("Index",
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            //.Where(p => p.Ativo ==)
                            .Where(p => p.E_Miradouro == miradouroPaisagem)//miradouros
                            //.Where(p => p.E_Miradouro==false)//so paisagens
                            .Skip((paginaatual - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao
                        }
                        );
                }
                else
                {
                    if (auxEstado == 2)
                    {
                        ativo = true;//paisagens ou miradouros ativos
                    }
                    else
                    {
                        ativo = false;//paisagens ou miradouros desativos
                    }

                    int tamanho = PagingInfoFotografias.TAM_PAGINA;
                    int totalitems = _context.Miradouro
                        .Where(p => p.Ativo == ativo)
                        .Where(p => p.E_Miradouro == miradouroPaisagem).Count();
                    int paginaatual;
                    if ((int)Math.Ceiling((double)totalitems / tamanho) < page)
                    {
                        paginaatual = page - 1;
                    }
                    else
                    {
                        paginaatual = page;
                    }

                    var paginacao = new PagingInfoMiradouro
                    {
                        CurrentPage = paginaatual,
                        PageSize = PagingInfoMiradouro.TAM_PAGINA,
                        TotalItems = _context.Miradouro
                        .Where(p => p.Ativo == ativo)
                        .Where(p => p.E_Miradouro == miradouroPaisagem)
                        .Count()
                    };

                    return View("Index",
                        new ListaMiradouro
                        {
                            Miradouros = _context.Miradouro
                            .OrderBy(p => p.MiradouroId)
                            .Where(p => p.Ativo == ativo)
                            .Where(p => p.E_Miradouro == miradouroPaisagem)//miradouros
                                                                           //.Where(p => p.E_Miradouro==false)//so paisagens
                            .Skip((paginaatual - 1) * paginacao.PageSize)
                            .Take(paginacao.PageSize),
                            pagination = paginacao
                        }
                        );
                }
            }


        }
    }
}
