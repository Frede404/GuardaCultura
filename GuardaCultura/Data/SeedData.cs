using GuardaCultura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCultura.Data
{
    public class SeedData//proposito de inserir na base de dados
    {
        internal static void Populate(GuardaCulturaContext dbContext)
        {
            PopulateHoras(dbContext);
            PopulateFuncao(dbContext);
            PopulateEstacaoAno(dbContext);
            PopulateTipoImagem(dbContext);
        }

        private static void PopulateHoras(GuardaCulturaContext dbContext)
        {
            if (dbContext.Hora.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            //dbContext.Products.Add//insere 1 unico item
            //introduzir 1 a 1
            /*dbContext.Hora.AddRange(
                new Hora
                {
                    Horas = 0
                },
                new Hora
                {
                    Horas = 1
                }
                );*/

            for (int i = 0; i < 24; i++)
            {
                dbContext.Hora.Add(
                    new Hora
                    {
                        Horas = i
                    }
                    );
                dbContext.SaveChanges();//so fica valido se salvarmos
            }

           
        }
        
        private static void PopulateFuncao(GuardaCulturaContext dbContext)
        {
            /*if (dbContext.Funcao.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            dbContext.Funcao.AddRange(
                new Funcao
                {
                    FuncaoId = 1,
                    FuncaoDesempenhar = "Administrador"
                },
                new Funcao
                {
                    FuncaoId = 2,
                    FuncaoDesempenhar = "Controlador"
                },
                new Funcao
                {
                    FuncaoId = 3,
                    FuncaoDesempenhar = "Utilizador"
                }
                );

            dbContext.SaveChanges();//so fica valido se salvarmos*/
        }

        private static void PopulateEstacaoAno(GuardaCulturaContext dbContext)
        {
            if (dbContext.EstacaoAno.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            dbContext.EstacaoAno.AddRange(
                new EstacaoAno
                {
                    Nome_estacao = "Primavera"
                },
                new EstacaoAno
                {
                    Nome_estacao = "Verão"
                },
                new EstacaoAno
                {
                    Nome_estacao = "Outono"
                },
                new EstacaoAno
                {
                    Nome_estacao = "Inverno"
                }
                );

            dbContext.SaveChanges();//so fica valido se salvarmos
        }

        private static void PopulateTipoImagem(GuardaCulturaContext dbContext)
        {
            if (dbContext.TipoImagem.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            dbContext.TipoImagem.AddRange(
                new TipoImagem
                {
                    Descricao = "Cidade"
                },
                new TipoImagem
                {
                    Descricao = "Paisagem"
                },
                new TipoImagem
                {
                    Descricao = "Serra"
                }
                );

            dbContext.SaveChanges();//so fica valido se salvarmos
        }
    }
}
