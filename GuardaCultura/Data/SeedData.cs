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
            }

            dbContext.SaveChanges();//so fica valido se salvarmos

        }
    }
}
