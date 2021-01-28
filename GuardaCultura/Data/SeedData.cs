using GuardaCultura.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/*Instrucoes
 * drop-database -Context GuardaCulturaContext
 * update-database -Context GuardaCulturaContext
 * 
 * UTILIZADORES
 * drop-Database -Context ApplicationDbContext
 * Update-Database -Context ApplicationDbContext
 * 
 * Add-Migration Nome -Context GuardaCulturaContext
*/

namespace GuardaCultura.Data
{
    public class SeedData//proposito de inserir na base de dados
    {
        private static int nomefoto=0;

        internal static void Populate(GuardaCulturaContext dbContext)
        {
            PopulateHoras(dbContext);
            PopulateFuncao(dbContext);
            //PopulatePessoa(dbContext);//linha94
            PopulateEstacaoAno(dbContext);
            PopulateTipoImagem(dbContext);
            PopulateMiradouro(dbContext);
            PopulateFotografias(dbContext, true);
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

        /*private static void PopulatePessoa(GuardaCulturaContext dbContext)
        {
            if (dbContext.Pessoa.Any())//ve se ja ha Horas na base de dados
            {
                return;
            }

            //dbContext.Products.Add//insere 1 unico item
            //introduzir 1 a 1
            dbContext.Pessoa.AddRange(
                new Pessoa
                {
                    Nome = "Fred",
                    Email = "Fred@mail.com",
                    //Password = "123",
                    Fiabilidade = 10,
                    bloqueio=false
                    //FuncaoId = 1,
                }
                );
            dbContext.SaveChanges();//so fica valido se salvarmos
            dbContext.Pessoa.AddRange(
                new Pessoa
                {
                    Nome = "Leandro",
                    Email = "Leandro@mail.com",
                    //Password = "321",
                    Fiabilidade = 10,
                    bloqueio=false
                    //FuncaoId = 2,
                }
                );
            dbContext.SaveChanges();//so fica valido se salvarmos
            dbContext.Pessoa.AddRange(
                new Pessoa
                {
                    Nome = "Turista",
                    Email = "Turista@mail.com",
                    //Password = "turista",
                    Fiabilidade = 0,
                    bloqueio=false
                    //FuncaoId = 3,
                }
                );

            dbContext.SaveChanges();//so fica valido se salvarmos
        }*/

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
                    Descricao = "Campo"
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
            string latitude_dd = "40.531592";
            string longitude_dd = "-7.330919";
            string latitude_dms = "40º31'53.7''N";
            string longitude_dms = "7º19'51.3''W";
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
                            Latitude_DD = latitude_dd,
                            Longitude_DD = longitude_dd,
                            Latitude_DMS = latitude_dms,
                            Longitude_DMS = longitude_dms,
                            Terreno = "Montanha",
                            E_Miradouro = true,
                            Ocupacao_maxima = ocupacaomax,
                            Ativo = true
                        }
                        );

                        dbContext.SaveChanges();//so fica valido se salvarmos
                        int Miradoruro_ID = dbContext.Miradouro
                                                .OrderByDescending(p => p.MiradouroId)
                                                .Select(p => p.MiradouroId).First();

                        for (int j = 0; j < 5; j++)
                        {
                            nomefoto++;
                            int Estacao_ID = rnd.Next(1, 5);
                            int Tipo_ID = rnd.Next(1, 4);
                            int Pessoa_ID = rnd.Next(1, 4);
                            //int foto_nome = rnd.Next(1, 20);
                            int foto_nome = rnd.Next(50, 61);
                            float classificacao = (float)rnd.Next(0, 1001) / 100;
                            int n_votos = rnd.Next(2, 101);
                            byte[] fotogafia = File.ReadAllBytes("./Fotos_FCMusic/" + foto_nome + ".jpg");
                            dbContext.Fotografia.Add(
                            new Fotografia
                            {
                                Nome = "Foto" + nomefoto,//(j + 1),
                                PessoaId = Pessoa_ID,
                                EstacaoAnoId = Estacao_ID,
                                MiradouroId = Miradoruro_ID,
                                TipoImagemId = Tipo_ID,
                                Aprovada = true,
                                Foto = fotogafia,
                                Classificacao = classificacao,
                                N_Votos = n_votos
                            });
                        }
                    }
                    else
                    {
                        dbContext.Miradouro.Add(
                        new Miradouro
                        {
                            Nome = "miradouro" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Latitude_DD = latitude_dd,
                            Longitude_DD = longitude_dd,
                            Latitude_DMS = latitude_dms,
                            Longitude_DMS = longitude_dms,
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
                            Nome = "paisagem" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Latitude_DD = latitude_dd,
                            Longitude_DD = longitude_dd,
                            Latitude_DMS = latitude_dms,
                            Longitude_DMS = longitude_dms,
                            Terreno = "Montanha",
                            E_Miradouro = false,
                            Ocupacao_maxima = -1,
                            Ativo = true
                        }
                        );

                        dbContext.SaveChanges();//so fica valido se salvarmos
                        int Miradoruro_ID = dbContext.Miradouro
                                                .OrderByDescending(p => p.MiradouroId)
                                                .Select(p=>p.MiradouroId).First();
                        
                        for (int j = 0; j < 5; j++)
                        {
                            nomefoto++;
                            int Estacao_ID = rnd.Next(1, 5);
                            int Tipo_ID = rnd.Next(1, 4);
                            int Pessoa_ID = rnd.Next(1, 4);
                            //int foto_nome = rnd.Next(1, 20);
                            int foto_nome = rnd.Next(50, 61);
                            float classificacao = (float)rnd.Next(0, 1001) / 100;
                            int n_votos = rnd.Next(2, 101);
                            byte[] fotogafia = File.ReadAllBytes("./Fotos_FCMusic/" + foto_nome + ".jpg");
                            dbContext.Fotografia.Add(
                            new Fotografia
                            {
                                Nome = "Foto" + nomefoto,//(j + 1),
                                PessoaId = Pessoa_ID,
                                EstacaoAnoId = Estacao_ID,
                                MiradouroId = Miradoruro_ID,
                                TipoImagemId = Tipo_ID,
                                Aprovada = true,
                                Foto = fotogafia,
                                Classificacao = classificacao,
                                N_Votos = n_votos
                            });
                        }
                    }
                    else
                    {
                        dbContext.Miradouro.Add(
                        new Miradouro
                        {
                            Nome = "paisagem" + (i + 1),
                            Localizacao = "localizacao" + (i + 1),
                            Latitude_DD = latitude_dd,
                            Longitude_DD = longitude_dd,
                            Latitude_DMS = latitude_dms,
                            Longitude_DMS = longitude_dms,
                            Terreno = "Planicie",
                            E_Miradouro = false,
                            Ocupacao_maxima = -1,
                            Ativo = false
                        }
                        );
                    }
                }
                dbContext.SaveChanges();//so fica valido se salvarmos
            }
            PopulateFotografias(dbContext, false);
        }

        private static void PopulateFotografias(GuardaCulturaContext dbContext, bool fonte)
        {
            if (fonte)
            {
                if (dbContext.Fotografia.Any())//ve se ja ha fotografias na base de dados
                {
                    return;
                }
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
            int Miradoruro_ID = 0;
            int qntd = 1000 - dbContext.Fotografia.Count();
            int qntdmiradouro = dbContext.Miradouro.Count();
            for (int i = 0; i < qntd; i++)
            {
                nomefoto++;
                Miradoruro_ID++;
                int Estacao_ID = rnd.Next(1, 5);
                int Tipo_ID = rnd.Next(1, 4);
                int Pessoa_ID = rnd.Next(1, 4);
                //int foto_nome = rnd.Next(1, 20);
                int foto_nome = rnd.Next(50, 61);
                float classificacao = (float)rnd.Next(0, 1001) / 100;
                int n_votos = rnd.Next(2, 101);
                byte[] fotogafia = File.ReadAllBytes("./Fotos_FCMusic/" + foto_nome + ".jpg");

                if (Pessoa_ID == 2)
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        Pessoa_ID = 1;
                    }
                    else
                    {
                        Pessoa_ID = 3;
                    }
                }

                if (Pessoa_ID == 3)
                {
                    if (rnd.Next(1, 100) > 50)
                    {
                        dbContext.Fotografia.Add(
                        new Fotografia
                        {
                            Nome = "Foto" + nomefoto,//(i + 1),
                            PessoaId = Pessoa_ID,
                            EstacaoAnoId = Estacao_ID,
                            MiradouroId = Miradoruro_ID,
                            TipoImagemId = Tipo_ID,
                            Aprovada = true,
                            Foto = fotogafia,
                            Classificacao = classificacao,
                            N_Votos = n_votos
                        }
                        );
                    }
                    else
                    {
                        dbContext.Fotografia.Add(
                        new Fotografia
                        {
                            Nome = "Foto" + nomefoto,//(i + 1),
                            PessoaId = Pessoa_ID,
                            EstacaoAnoId = Estacao_ID,
                            MiradouroId = Miradoruro_ID,
                            TipoImagemId = Tipo_ID,
                            Aprovada = false,
                            Foto = fotogafia,
                            Classificacao = classificacao,
                            N_Votos = n_votos
                        }
                        );
                    }
                }
                else
                {
                    dbContext.Fotografia.Add(
                        new Fotografia
                        {
                            Nome = "Foto" + nomefoto,//(i + 1),
                            PessoaId = Pessoa_ID,
                            EstacaoAnoId = Estacao_ID,
                            MiradouroId = Miradoruro_ID,
                            TipoImagemId = Tipo_ID,
                            Aprovada = true,
                            Foto = fotogafia,
                            Classificacao = classificacao,
                            N_Votos = n_votos
                        }
                        );
                }
                //ver no inicio da janela as instrucoes
                dbContext.SaveChanges();//so fica valido se salvarmos
                if (Miradoruro_ID == qntdmiradouro)
                {
                    Miradoruro_ID = 0;
                }
            }
        }

        //utilizadores

        //Update-Database -Context ApplicationDbContext
        private const string DEFAULT_ADMIN_USER = "admin@ipg.pt";
        private const string DEFAULT_ADMIN_PASS = "Admin123!";
        private const string DEFAULT_Turista_USER = "turista@ipg.pt";
        private const string DEFAULT_Turista_PASS = "Turista123!";
        private const string DEFAULT_Controlador_USER = "controlador@ipg.pt";
        private const string DEFAULT_Controlador_PASS = "Controlador123!";
        private const string ROLE_ADMIN = "Administrador";
        private const string ROLE_CONTROLADOR = "Controlador";
        private const string ROLE_TURISTA = "Turista";
     
        internal static async Task SeedDefaultAdminAsync(UserManager<IdentityUser> userManager)
        {
            await CriarUtilizador(userManager, DEFAULT_ADMIN_USER, DEFAULT_ADMIN_PASS, ROLE_ADMIN);
        }

        //verifica se utilizador foi criado
        private static async Task CriarUtilizador(UserManager<IdentityUser> userManager, string username, string password, string role)
        {
            IdentityUser user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                user = new IdentityUser(username);
                await userManager.CreateAsync(user, password);
            }
            
            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }

        internal static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await CriarRole(roleManager, ROLE_ADMIN);
            await CriarRole(roleManager, ROLE_CONTROLADOR);
            await CriarRole(roleManager, ROLE_TURISTA);
        }
        
        //verifica se o role foi criado
        private static async Task CriarRole(RoleManager<IdentityRole> roleManager, string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        internal static async Task SeedDevUsersAsync(UserManager<IdentityUser> userManager)
        {
            await CriarUtilizador(userManager, DEFAULT_Turista_USER, DEFAULT_Turista_PASS, ROLE_TURISTA);
            await CriarUtilizador(userManager, DEFAULT_Controlador_USER, DEFAULT_Controlador_PASS, ROLE_CONTROLADOR);
        }

        internal static void SeedDevData(GuardaCulturaContext dbContext)
        {
            if (dbContext.Pessoa.Any())
            {
                return;
            }

            dbContext.Pessoa.Add(new Pessoa
            {
                Nome = "Nome Turista",
                Email = "turista@ipg.pt",
                Fiabilidade = 0,
                Bloqueio = false
            });
            dbContext.Pessoa.Add(new Pessoa
            {
                Nome = "Nome Turista2",
                Email = "turista2@ipg.pt",
                Fiabilidade = 0,
                Bloqueio = false
            });

            dbContext.Pessoa.Add(new Pessoa
            {
                Nome = "Nome Controlador",
                Email = "controlador@ipg.pt",
                Fiabilidade = 0,
                Bloqueio = false
            });

            dbContext.SaveChanges();
        }
    }
}
