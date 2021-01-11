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

            var paisagemquery = _context.Miradouro
                .Join(_context.Fotografia, miradouro => miradouro.MiradouroId, foto => foto.MiradouroId, (miradouro, foto) =>
                new
                {
                    MiradouroId = miradouro.MiradouroId,
                    Nome = miradouro.Nome,
                    Localizacao = miradouro.Localizacao,
                    Coordenadas_DD = miradouro.Latitude_DD+"," + miradouro.Longitude_DD,
                    Coordenadas_DMS = miradouro.Latitude_DMS+","+miradouro.Longitude_DMS,
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
                .Where(p => p.Ativo == true)
                .Where(p => p.Aprovada == true)
                .OrderBy(p => p.MiradouroId)
                .ThenByDescending(p => p.Classificacao).ToList();

            List<MiradouroFoto> FotoApresentar = new List<MiradouroFoto>();
            List<MiradouroFoto> PaisagemApresentar = new List<MiradouroFoto>();
            int i = 0;
            int j = 1;
            foreach (var item in paisagemquery)
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
                            Coordenadas_DD = item.Coordenadas_DD,
                            Coordenadas_DMS = item.Coordenadas_DMS,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao,
                            Aprovada = item.Aprovada
                        });

                    FotoApresentar.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            ContagemRepetido = j,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_DD = item.Coordenadas_DD,
                            Coordenadas_DMS = item.Coordenadas_DMS,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao,
                            Aprovada = item.Aprovada
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
                            Coordenadas_DD = item.Coordenadas_DD,
                            Coordenadas_DMS = item.Coordenadas_DMS,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao,
                            Aprovada = item.Aprovada
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
                .Where(p => p.E_Miradouro == false).Count()
            };

            var miradouroquery = _context.Miradouro
                .Join(_context.Fotografia, miradouro => miradouro.MiradouroId, foto => foto.MiradouroId, (miradouro, foto) =>
                    new
                    {
                        MiradouroId = miradouro.MiradouroId,
                        Nome = miradouro.Nome,
                        Localizacao = miradouro.Localizacao,
                        Coordenadas_DD = miradouro.Latitude_DD + "," + miradouro.Longitude_DD,
                        Coordenadas_DMS = miradouro.Latitude_DMS + "," + miradouro.Longitude_DMS,
                        Terreno = miradouro.Terreno,
                        E_Miradouro = miradouro.E_Miradouro,
                        Condicoes = miradouro.Condicoes,
                        Ocupacao_maxima = miradouro.Ocupacao_maxima,
                        Descricao = miradouro.Descricao,
                        Ativo = miradouro.Ativo,
                        Foto = foto.Foto,
                        FotografiaId = foto.FotografiaId,
                        Classificacao = foto.Classificacao,
                        Aprovada = foto.Aprovada,
                    })
                .Where(p => p.E_Miradouro == true)
                .Where(p => p.Ativo == true)
                .Where(p => p.Aprovada == true)
                .OrderBy(p => p.MiradouroId)
                .ThenByDescending(p => p.Classificacao).ToList();

            List<MiradouroFoto> FotoApresentar = new List<MiradouroFoto>();
            List<MiradouroFoto> miradouroApresentar = new List<MiradouroFoto>();
            int i = 0;
            int j = 1;
            foreach (var item in miradouroquery)
            {
                if (item.MiradouroId > i)
                {
                    i = item.MiradouroId;
                    j = 1;
                    miradouroApresentar.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            ContagemRepetido = j,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_DD = item.Coordenadas_DD,
                            Coordenadas_DMS = item.Coordenadas_DMS,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao,
                            Aprovada = item.Aprovada
                        });

                    FotoApresentar.Add(
                        new MiradouroFoto
                        {
                            MiradouroId = item.MiradouroId,
                            ContagemRepetido = j,
                            Nome = item.Nome,
                            Localizacao = item.Localizacao,
                            Coordenadas_DD = item.Coordenadas_DD,
                            Coordenadas_DMS = item.Coordenadas_DMS,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao,
                            Aprovada = item.Aprovada
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
                            Coordenadas_DD = item.Coordenadas_DD,
                            Coordenadas_DMS = item.Coordenadas_DMS,
                            Terreno = item.Terreno,
                            E_Miradouro = item.E_Miradouro,
                            Condicoes = item.Condicoes,
                            Ocupacao_maxima = item.Ocupacao_maxima,
                            Descricao = item.Descricao,
                            Ativo = item.Ativo,
                            Fotografia = item.Foto,
                            FotoId = item.FotografiaId,
                            Classificacao = item.Classificacao,
                            Aprovada = item.Aprovada
                        });
                }
            }

            return View(
                new ListaPaginaMiradouros
                {
                    MiradouroPaisagem = miradouroApresentar
                    //.Where(p => p.ContagemRepetido == 1)
                    .OrderBy(p => p.MiradouroId)
                    .Skip((page - 1) * paginacao.PageSize)
                    .Take(paginacao.PageSize),

                    Fotografias = FotoApresentar
                    .OrderBy(p => p.MiradouroId)
                    .ThenByDescending(p => p.Classificacao),

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
