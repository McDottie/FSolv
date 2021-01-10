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

*   - Não há registo de quais as entidades que foram carregadas da BD e quais as entidades que foram criadas programaticamente.
*   - Problemas de estrutura do código
*   - AbstractMapper não é adequado à solução actual
*   - Faltam interfaces para os tipos de domínio
*   - Má implementação do padrão Query Object
*/
using System;
using System.Configuration;

using Interfaces;
using System.Collections.Generic;
using System.Reflection;
using System.Data;

namespace FSolv
{

    class Program
    {

        /* static void Main(string[] args)
         {
             string connectionString = ConfigurationManager.ConnectionStrings["FSolv"].ConnectionString;

             /** TESTAR CONTRIBUINTE*/
        /*Console.WriteLine("TESTAR CONTRIBUINTE");

        using (Context ctx = new Context(connectionString))
        {
            IContribuinteRepository crepo = ctx.Contribuinte;
            IContribuinte c = null;
            int i = 0;
            foreach (var contribuinte in crepo.FindAll())
            {
                if(i == 2)
                    c = contribuinte;
                i++;
                Console.WriteLine("Contribuinte: {0}-{1}-{2}", contribuinte.Nif, contribuinte.Name, contribuinte.Morada);
            }

            foreach (var contribuinte in c.Faturas)
            {
                Console.WriteLine("Fatura: {0}-{1}-{2}", contribuinte.Id, contribuinte.Total, contribuinte.DataEmissao);
            } 

        }*/

        /** TESTAR FATURA*/
        /*Console.WriteLine("TESTAR CONTRIBUINTE");

        using (Context ctx = new Context(connectionString))
        {
            IFaturaRepository crepo = ctx.Fatura;
            IFatura f = null;
            int i = 0;
            foreach (var fatura in crepo.FindAll())
            {
                if (i == 1)
                    f = fatura;
                i++;
                Console.WriteLine("Fatura: {0}, {1}, {2}, {3}", fatura.Id, fatura.Iva, fatura.Total, fatura.DataEmissao);
            }

            foreach (var item in f.Items)
            {
                Console.WriteLine("Items: {0}-{1}-{2}", item.Id, item.Qnt, item.Desconto);
            }

            IContribuinte cf = f.Contribuinte;
            Console.WriteLine("Contribuinte: {0}-{1}-{2}", cf.Name, cf.Nif, cf.Morada);
        }*/

        /** TESTAR NOTA CREDITO*/
        /*Console.WriteLine("TESTAR CONTRIBUINTE");

        using (Context ctx = new Context(connectionString))
        {
            INotaCreditoRepository crepo = ctx.NotaCredito;
            INotaCredito nc = null;
            int i = 0;
            foreach (var notcred in crepo.FindAll())
            {
                if (i == 0)
                    nc = notcred;
                i++;
                Console.WriteLine("Nota De Credito: {0}, {1}", notcred.Id, notcred.State);
            }

            foreach (var item in nc.Items)
            {
                Console.WriteLine("Items: {0}-{1}-{2}", item.Id, item.Qnt, item.Desconto);
            }

        }*/

        /** TESTAR ITEM*/
        /*Console.WriteLine("TESTAR ITEM");

        using (Context ctx = new Context(connectionString))
        {
            IItemRepository crepo = ctx.Item;
            IItem it = null;
            int i = 0;
            foreach (var item in crepo.FindAll())
            {
                if (i == 0)
                    it = item;
                i++;
                Console.WriteLine("Item: {0}, {1}, {2}", item.Id, item.Desconto, item.Qnt);
            }

            IFatura fatura = it.Fatura;
            Console.WriteLine("Fatura: {0}, {1}, {2}, {3}, {4}", fatura.Id, fatura.Iva, fatura.State, fatura.DataEmissao, fatura.Total); 

            IProduto prod = it.Produto;
            Console.WriteLine("Contribuinte: {0}-{1}-{2}, {3}", prod.Sku, prod.Descricao, prod.Iva, prod.Valor);

        }*/

        /** TESTAR ITEM*/
        /*Console.WriteLine("TESTAR PRODUTO");

        using (Context ctx = new Context(connectionString))
        {
            IProductRepository crepo = ctx.Produto;
            IProduto it = null;
            int i = 0;
            foreach (var item in crepo.FindAll())
            {
                if (i == 0)
                    it = item;
                i++;
                Console.WriteLine("Item: {0}, {1}, {2}, {3}", item.Sku, item.Descricao, item.Iva, item.Valor);
            }
        }
    }*/
        private static void printResults(IDataReader dr)
        {
            int col = dr.FieldCount;
            for (int i = 0; i < col; ++i)
            {
                Console.Write(String.Format("{0,-27}",dr.GetName(i)) + " |");
            }
            Console.WriteLine();
            for(int i = 0; i<col; ++i)
            {
                Console.Write("--------------------");
            }
            while (dr.Read())
            {
                for (int i = 0; i < col; ++i)
                {
                    Console.Write(dr.GetValue(i) + "\t\t|");
                    Console.WriteLine();
                }
            }
        }

    }
}
