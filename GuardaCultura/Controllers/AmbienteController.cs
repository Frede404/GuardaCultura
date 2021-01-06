using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuardaCultura.Data;
using GuardaCultura.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuardaCultura.Controllers
{
    public class AmbienteController : Controller
    {
        private readonly GuardaCulturaContext _context;

        public AmbienteController(GuardaCulturaContext context)// recebe a bd
        {
            _context = context;
        }

        public IActionResult Ambiente()
        {
            return View();
        }
        public IActionResult QualidadeAr()
        {
            return View();
        }
        public IActionResult Paisagens(int page = 1)
        {
            var paginacao = new PagingInfoPaginaMiradouros
            {
                CurrentPage = page,
                PageSize = PagingInfoPaginaMiradouros.TAM_PAGINA,
                TotalItems = _context.Miradouro
                .Where(p => p.Ativo == true)
                .Where(p => p.E_Miradouro == false).Count()
            };

            var miradouroquery = _context.Miradouro
                .Join(_context.Fotografia.Where(p=>p.Aprovada==true), miradouro => miradouro.MiradouroId, foto => foto.MiradouroId, (miradouro, foto) =>
                new
                {
                    MiradouroId = miradouro.MiradouroId,
                    Nome = miradouro.Nome,
                    Localizacao = miradouro.Localizacao,
                    Coordenadas_gps = miradouro.Coordenadas_gps,
                    Terreno = miradouro.Terreno,
                    E_Miradouro = miradouro.E_Miradouro,
                    Condicoes = miradouro.Condicoes,
                    Ocupacao_maxima = miradouro.Ocupacao_maxima,
                    Descricao = miradouro.Descricao,
                    Ativo = miradouro.Ativo,
                    Foto = foto.Foto,
                    FotografiaId = foto.FotografiaId,
                    Classificacao = foto.Classificacao,
                })
                .Where(p => p.E_Miradouro == false)
                .Where(p => p.Ativo == true)
                .OrderBy(p => p.MiradouroId)
                .ThenByDescending(p => p.Classificacao).ToList();

            List<MiradouroFoto> FotoApresentar = new List<MiradouroFoto>();
            List<MiradouroFoto> PaisagemApresentar = new List<MiradouroFoto>();
            int i = 0;
            int j = 1;
            foreach (var item in miradouroquery)
            {
                if (item.MiradouroId > i)
                {
                    i = item.MiradouroId;
                    j = 1;
                    PaisagemApresentar.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            ContagemRepetido = j,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_gps = item.Coordenadas_gps,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao
                        });

                    FotoApresentar.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            ContagemRepetido = j,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_gps = item.Coordenadas_gps,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao
                        });
                }
                else if (item.MiradouroId == i && j < 6)
                {
                    j++;
                    FotoApresentar.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            ContagemRepetido = j,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_gps = item.Coordenadas_gps,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao
                        });
                }
            }

            return View(
                new ListaPaginaMiradouros
                {
                    MiradouroPaisagem = PaisagemApresentar
                    //.Where(p => p.ContagemRepetido == 1)
                    .OrderBy(p=>p.MiradouroId)
                    .Skip((page - 1) * paginacao.PageSize)
                    .Take(paginacao.PageSize),

                    Fotografias = FotoApresentar
                    .OrderBy(p => p.MiradouroId)
                    .ThenByDescending(p => p.Classificacao),

                    pagination = paginacao
                }
                );
            //return View();
        }

        public IActionResult Miradouros(int page = 1)
        {
            var paginacao = new PagingInfoPaginaMiradouros
            {
                CurrentPage = page,
                PageSize = PagingInfoPaginaMiradouros.TAM_PAGINA,
                TotalItems = _context.Miradouro
                .Where(p => p.Ativo == true)
                .Where(p => p.E_Miradouro == true).Count()
            };

            var Maxclassificacao = from foto in _context.Fotografia
                                   where foto.Aprovada == true
                                   group foto by foto.MiradouroId into fotogroup
                                   orderby fotogroup.Key
                                   select new
                                   {
                                       miradouroId = fotogroup.Key,
                                       melhorclassificacao = fotogroup.Max(p => p.Classificacao)
                                   };

            var FotosMax = (from foto in _context.Fotografia
                            join fotomax in Maxclassificacao on new { a = foto.MiradouroId, b = foto.Classificacao } equals new { a = fotomax.miradouroId, b = fotomax.melhorclassificacao }
                            where foto.Aprovada == true
                            orderby foto.MiradouroId
                            select new
                            {
                                Nome = foto.Nome,
                                MiradoutoId = foto.MiradouroId,
                                Foto = foto.Foto,
                                Classificacao = foto.Classificacao
                            });

            var query = "select * from(select Miradouro.* Foto, Classificacao, row_number() over(partition by Miradouro.miradouroid order by Miradouro.miradouroid, classificacao desc) rn from Miradouro, Fotografia where Miradouro.MiradouroId = Fotografia.MiradouroId)b where rn = 1";
            var testar = (from miradouro in _context.Miradouro
                        join fotoapresentar in _context.Fotografia on miradouro.MiradouroId equals fotoapresentar.MiradouroId
                        select new
                        {
                            MiradouroId = miradouro.MiradouroId,
                            Nome = miradouro.Nome,
                            Localizacao = miradouro.Localizacao,
                            Coordenadas_gps = miradouro.Coordenadas_gps,
                            Terreno = miradouro.Terreno,
                            E_Miradouro = miradouro.E_Miradouro,
                            Condicoes = miradouro.Condicoes,
                            Ocupacao_maxima = miradouro.Ocupacao_maxima,
                            Descricao = miradouro.Descricao,
                            Ativo = miradouro.Ativo,
                            Foto = fotoapresentar.Foto,
                            Classificacao = fotoapresentar.Classificacao,
                        });

            var teste2 = testar.OrderBy(p => p.MiradouroId)
                         .Select((t, i) => new { 
                             MiradouroId = t.MiradouroId,
                             Nome = t.Nome,
                             Localizacao = t.Localizacao,
                             Coordenadas_gps = t.Coordenadas_gps,
                             Terreno = t.Terreno,
                             E_Miradouro = t.E_Miradouro,
                             Condicoes = t.Condicoes,
                             Ocupacao_maxima = t.Ocupacao_maxima,
                             Descricao = t.Descricao,
                             Ativo = t.Ativo,
                             Foto = t.Foto,
                             Classificacao = t.Classificacao,
                             rn= i
                         });
            //aqui funca
            var fds = _context.Miradouro
                .Join(_context.Fotografia, miradouro => miradouro.MiradouroId, foto => foto.MiradouroId, (miradouro, foto) =>
                new
                {
                    MiradouroId = miradouro.MiradouroId,
                    Nome = miradouro.Nome,
                    Localizacao = miradouro.Localizacao,
                    Coordenadas_gps = miradouro.Coordenadas_gps,
                    Terreno = miradouro.Terreno,
                    E_Miradouro = miradouro.E_Miradouro,
                    Condicoes = miradouro.Condicoes,
                    Ocupacao_maxima = miradouro.Ocupacao_maxima,
                    Descricao = miradouro.Descricao,
                    Ativo = miradouro.Ativo,
                    Foto = foto.Foto,
                    FotografiaId = foto.FotografiaId,
                    Classificacao = foto.Classificacao,
                    Aprovada = foto.Aprovada
                })
                .Where(p => p.E_Miradouro == false)
                .OrderBy(p => p.MiradouroId).ThenByDescending(p => p.Classificacao);

            List<object> fd = new List<object>();
            int k = 0;
            foreach(var item in fds)
            {
                if (item.MiradouroId > k)
                {
                    k = item.MiradouroId;
                    fd.Add(item);
                }
            }
            List<MiradouroFoto> tes = new List<MiradouroFoto>();
            int i=0;
            int j=1;
            foreach (var item in fds)
            {
                if (item.MiradouroId > i)
                {
                    i = item.MiradouroId;
                    j = 1;
                    tes.Add(
                        new MiradouroFoto { 
                            MiradouroId = item.MiradouroId,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_gps = item.Coordenadas_gps,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                        });
                }
                else if (item.MiradouroId == i && j<6)
                {
                    j++;
                    tes.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_gps = item.Coordenadas_gps,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                        });
                }
            }

            return View(
                new ListaPaginaMiradouros
                {
                    Miradouros = _context.Miradouro
                    .OrderBy(p => p.MiradouroId)
                    .Where(p => p.Ativo == true)
                    .Where(p => p.E_Miradouro == true)
                    .Skip((page - 1) * paginacao.PageSize)
                    .Take(paginacao.PageSize),

                    MiradouroPaisagem=tes.Where(p => p.Ativo == true),

                    fotoapresentacao= FotosMax.Select(p => new Fotografia
                    {
                        Nome = p.Nome,
                        Foto = p.Foto,
                        MiradouroId = p.MiradoutoId,
                        Classificacao = p.Classificacao
                    }),

                    Fotografiasantigas= _context.Fotografia
                    .OrderBy(p => p.MiradouroId)
                    .OrderByDescending(p => p.Classificacao)
                    .Where(p => p.Aprovada == true),

                    pagination = paginacao
                }
                );
            //return View(await _context.Miradouro.ToListAsync());
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}
