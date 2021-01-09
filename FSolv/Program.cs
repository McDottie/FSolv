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
            Console.WriteLine("TESTAR CONTRIBUINTE");

            using (Context ctx = new Context(connectionString))
            {
                IContribuinteRepository crepo = ctx.Contribuinte;

                foreach (var contribuinte in crepo.FindAll())
                {
                    Console.WriteLine("Country: {0}-{1}-{2}", contribuinte.Nif, contribuinte.Name, contribuinte.Morada);
                }

            }

            
            
        }
    }
}
