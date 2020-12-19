using GuardaCultura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*Instrucoes
 * drop-database -Context GuardaCulturaContext
 * update-database -Context GuardaCulturaContext
 * criar na tabela pessoa
 * alterar o Pessoa_ID
*/

namespace GuardaCultura.Data
{
    public class SeedData//proposito de inserir na base de dados
    {
        static int Pessoa_ID = 2;
        internal static void Populate(GuardaCulturaContext dbContext)
        {
            PopulateHoras(dbContext);
            PopulateFuncao(dbContext);
            PopulateEstacaoAno(dbContext);
            PopulateTipoImagem(dbContext);
            PopulateMiradouro(dbContext);
            PopulateFotografias(dbContext);
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
            if (dbContext.Funcao.Any())//ve se ja ha Funcoes na base de dados
            {
                return;
            }

            dbContext.Funcao.AddRange(
                new Funcao
                {
                    FuncaoDesempenhar = "Administrador"
                },
                new Funcao
                {
                    FuncaoDesempenhar = "Controlador"
                },
                new Funcao
                {
                    FuncaoDesempenhar = "Utilizador"
                }
                );

            dbContext.SaveChanges();//so fica valido se salvarmos
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

        private static void PopulateMiradouro(GuardaCulturaContext dbContext)
        {
            if (dbContext.Miradouro.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            //dbContext.Products.Add//insere 1 unico item
            //introduzir 1 a 1
            /*dbContext.Miradouro.AddRange(
                new Miradouro
                {
                    Nome = "miradouro1",
                    Localizacao="localizacao1",
                    Coordenadas_gps="coordenada1",
                    Terreno="Cidade",
                    E_Miradouro=true,
                    Ocupacao_maxima=5,
                    Ativo=true
                },
                new Miradouro
                {
                    Nome = "miradouro2",
                    Localizacao = "localizacao2",
                    Coordenadas_gps = "coordenada2",
                    Terreno = "planicie",
                    E_Miradouro = false,
                    Ocupacao_maxima = -1,
                    Ativo = false
                }
                ) ;
            dbContext.SaveChanges();//so fica valido se salvarmos
            */

            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int ocupacaomax = rnd.Next(0, 10);

                if (rnd.Next(1, 100) > 50)
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        dbContext.Miradouro.Add(
                        new Miradouro
                        {
                            Nome = "miradouro" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Coordenadas_gps = "coordenada" + (i + 1),
                            Terreno = "Cidade",
                            E_Miradouro = true,
                            Ocupacao_maxima = ocupacaomax,
                            Ativo = true
                        }
                        );
                    }
                    else
                    {
                        dbContext.Miradouro.Add(
                        new Miradouro
                        {
                            Nome = "miradouro" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Coordenadas_gps = "coordenada" + (i + 1),
                            Terreno = "Cidade",
                            E_Miradouro = true,
                            Ocupacao_maxima = ocupacaomax,
                            Ativo = false
                        }
                        );
                    }
                }
                else
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        dbContext.Miradouro.Add(
                        new Miradouro
                        {
                            Nome = "miradouro" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Coordenadas_gps = "coordenada" + (i + 1),
                            Terreno = "Cidade",
                            E_Miradouro = false,
                            Ocupacao_maxima = -1,
                            Ativo = true
                        }
                        );
                    }
                    else
                    {
                        dbContext.Miradouro.Add(
                        new Miradouro
                        {
                            Nome = "miradouro" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Coordenadas_gps = "coordenada" + (i + 1),
                            Terreno = "Cidade",
                            E_Miradouro = false,
                            Ocupacao_maxima = -1,
                            Ativo = false
                        }
                        );
                    }
                }
                dbContext.SaveChanges();//so fica valido se salvarmos
            }
        }

        private static void PopulateFotografias(GuardaCulturaContext dbContext)
        {
            if (dbContext.Fotografia.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            //dbContext.Products.Add//insere 1 unico item
            //introduzir 1 a 1
            /*dbContext.Fotografia.AddRange(
                new Fotografia
                {
                    Nome = "Foto1",
                    PessoaId = Pessoa_ID,
                    EstacaoAnoId=1,
                    MiradouroId=2,
                    TipoImagemId=2,
                    Aprovada=true

                },
                new Fotografia
                {
                    Nome = "Foto2",
                    PessoaId = Pessoa_ID,
                    EstacaoAnoId = 2,
                    MiradouroId = 3,
                    TipoImagemId = 3,
                    Aprovada = false
                }
                ) ;
            dbContext.SaveChanges();//so fica valido se salvarmos
            */

            
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int Miradoruro_ID = rnd.Next(1, 100);
                int Estacao_ID = rnd.Next(1, 4);
                int Tipo_ID = rnd.Next(1, 3);
                if (rnd.Next(1, 100) > 50)
                {
                    dbContext.Fotografia.Add(
                    new Fotografia
                    {
                        Nome = "Foto"+(i+1),
                        PessoaId = Pessoa_ID,
                        EstacaoAnoId = Estacao_ID,
                        MiradouroId = Miradoruro_ID,
                        TipoImagemId = Tipo_ID,
                        Aprovada = true
                    }
                    );
                }
                else
                {

                    dbContext.Fotografia.Add(
                    new Fotografia
                    {
                        Nome = "Foto"+(i+1),
                        PessoaId = Pessoa_ID,
                        EstacaoAnoId = Estacao_ID,
                        MiradouroId = Miradoruro_ID,
                        TipoImagemId = Tipo_ID,
                        Aprovada = true
                    }
                    );
                }
                //ver no inicio da janela as instrucoes
                dbContext.SaveChanges();//so fica valido se salvarmos
            }
        }
    }
}
