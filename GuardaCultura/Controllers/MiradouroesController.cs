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
        public async Task<IActionResult> Create([Bind("MiradouroId,Nome,Localizacao,Terreno,E_Miradouro,Condicoes,Ocupacao_maxima,Descricao")] Miradouro miradouro,
                                                        string grausLat ="0", string minutoLat ="0", string segundosLat ="0", char DirNS='N', string grausLong ="0", string minutosLong ="0", string segundosLong ="0", char DirWE='E', string Coordenadas="DD", string latiDD ="0", string longDD ="0" )//serve para evitar alguns ataques, só recebe campos que estejam no Bind
        {
            
            if (Coordenadas == "DMS")
            {   
                miradouro.Latitude_DMS = string.Concat(grausLat, "º", minutoLat ,"'", segundosLat ,"''", DirNS);
                miradouro.Latitude_DMS = miradouro.Latitude_DMS.Replace(",", ".");

                miradouro.Longitude_DMS = string.Concat(grausLong, "º", minutosLong, "'", segundosLong, "''", DirWE);
                miradouro.Longitude_DMS = miradouro.Longitude_DMS.Replace(",", ".");

                //latitude DD
                int grausLatitude = Int32.Parse(grausLat);
                int minutosLatitude = Int32.Parse(minutoLat);
                float segundosLatitude = Convert.ToSingle(segundosLat);
                double LatDD = grausLatitude + (minutosLatitude / 60.0) + (segundosLatitude / 3600.0);
                if (DirNS == 'S')
                {
                    LatDD = LatDD * -1;                    
                }
                LatDD = Math.Round(LatDD, 6);
                miradouro.Latitude_DD = "" + LatDD;
                miradouro.Latitude_DD = miradouro.Latitude_DD.Replace(",", ".");

                //longitude DD
                int grausLongitude = Int32.Parse(grausLong);
                int minutosLongitude = Int32.Parse(minutosLong);
                float segundosLongitude = Convert.ToSingle(segundosLong);
                double LongDD = grausLongitude + (minutosLongitude / 60.0) + (segundosLongitude / 3600.0);

                if (DirWE == 'W')
                {
                    LongDD = LongDD * -1;
                }
                LongDD = Math.Round(LongDD, 6);
                miradouro.Longitude_DD = "" + LongDD;
                miradouro.Longitude_DD = miradouro.Longitude_DD.Replace(",", ".");
                
            }
            
            if (Coordenadas == "DD")
            {
                miradouro.Latitude_DD = "" + latiDD;
                miradouro.Latitude_DD = miradouro.Latitude_DD.Replace(",", ".");

                miradouro.Longitude_DD = "" + longDD;
                miradouro.Longitude_DD = miradouro.Longitude_DD.Replace(",", ".");

                double latDoubleDMS = Convert.ToDouble(latiDD);
                char dirNS;

                if (latDoubleDMS < 0)
                {
                    latDoubleDMS = latDoubleDMS * -1;
                    dirNS = 'S';
                }
                else
                {
                    dirNS = 'N';
                }
                
                int grausFinaLatlDMS = (int)latDoubleDMS;
                float auxminutosLat = (float) ((latDoubleDMS - grausFinaLatlDMS) * 60.0);
                int minutosFinalLatDMS = (int) auxminutosLat;
                float auxsegundosLat = (float) ((auxminutosLat - minutosFinalLatDMS) * 60.0);
                float segundosFinalLatDMS = (float) Math.Round(auxsegundosLat, 3);
                 
                miradouro.Latitude_DMS = "" + grausFinaLatlDMS + "º" + minutosFinalLatDMS + "'" + segundosFinalLatDMS + "''" + dirNS;
                miradouro.Latitude_DMS = miradouro.Latitude_DMS.Replace(",", ".");

                double longDoubleDMS = Convert.ToDouble(longDD);
                char dirWE;

                if (longDoubleDMS < 0)
                {
                    longDoubleDMS = longDoubleDMS * -1;
                    dirWE = 'W';
                }
                else
                {
                    dirWE = 'E';
                }
                
                int grausFinalLongDMS = (int) longDoubleDMS;
                float auxminutosLong = (float) ((longDoubleDMS - grausFinalLongDMS) * 60.0);
                int minutosFinalLongDMS = (int)auxminutosLong;
                float auxsegundosLong = (float)((auxminutosLong - minutosFinalLongDMS) * 60.0);
                float segundosFinalLongDMS = (float)Math.Round(auxsegundosLong, 3);

                miradouro.Longitude_DMS = "" + grausFinalLongDMS + "º" + minutosFinalLongDMS + "'" + segundosFinalLongDMS + "''" + dirWE;
                miradouro.Longitude_DMS = miradouro.Longitude_DMS.Replace(",", ".");
            }

            miradouro.Disponibilidade = true;
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
                //ViewData["sucess"] = "Miradouro criado com sucesso.";
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
