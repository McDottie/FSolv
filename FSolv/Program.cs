/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*
*Problemas Encontrados:
*   - Não há registo de quais as entidades que foram carregadas da BD e quais as entidades que foram criadas programaticamente.
*   - Problemas de estrutura do código
*   - AbstractMapper não é adequado à solução actual
*   - Faltam interfaces para os tipos de domínio
*   - Má implementação do padrão Query Object
*/
using System;
using System.Configuration;
using FSolv.concrete;
using FSolv.model;
using FSolv.mapper.concrete;

namespace FSolv
{

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;

            /** TESTAR CONTRIBUINTE*/
            Console.WriteLine(" TESTAR CONTRIBUINTE");

            using (Context ctx = new Context(connectionString))
            {

                Contribuinte c = new Contribuinte();
                c.Name = "Maria";
                c.Morada = "Rua rua n rua";


                ContribuinteMapper contribuinteMap = new ContribuinteMapper(ctx);
                c = contribuinteMap.Create(c);
                Contribuinte c1 = contribuinteMap.Read(c.Nif);
                Console.WriteLine("Contribuinte: {0}-{1}", c1.Nif, c1.Name);

                c1.Name = "Jorge";
                contribuinteMap.Update(c1);
                c1 = contribuinteMap.Read(c1.Nif);
                Console.WriteLine("Country: {0}-{1}", c1.Nif, c1.Name);

                Contribuinte c2 = new Contribuinte();
                c2.Name = "Manuel";
                contribuinteMap.Create(c2);

                Console.WriteLine("FindAll");
                foreach (var contribuinte in ctx.Countries.FindAll())
                {
                    Console.WriteLine("Country: {0}-{1}", contribuinte.Nif, contribuinte.Name);
                }
                Console.WriteLine("Find");
                foreach (var country in ctx.Countries.Find(ct => ct.Name.Equals("Portugal")))
                {
                    Console.WriteLine("Country: {0}-{1}", country.Nif, country.Name);
                }

                Console.WriteLine("ReadAll");
                foreach (var contribuinte in contribuinteMap.ReadAll())
                {
                    Console.WriteLine("Country: {0}-{1}", contribuinte.Nif, contribuinte.Name);
                    contribuinteMap.Delete(contribuinte);
                }


                foreach (var contribuinte in contribuinteMap.ReadAll())
                {
                    Console.WriteLine("Country: {0}-{1}", contribuinte.Nif, contribuinte.Name);
                }

            }

            
            
        }
    }
}
