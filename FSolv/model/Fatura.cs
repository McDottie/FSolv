/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material did?tico para apoio 
*   ? unidade curricular de 
*   Sistemas de Informa??o II
*
*	Os exemplos podem n?o ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedag?gicos
*	Eventuais incorrec??es s?o alvo de discuss?o
*	nas aulas.
*/
using System;
using System.Collections.Generic;

namespace FSolv.model

{
    public class Fatura
    {
        public int? Id { get; set; }
        public DateTime DataEmissao { get; set; }
        
        public string State { get; set; }

        public virtual Contribuinte Contribuinte { get; set; }

        public double Iva { get; set; }
        
        public double Total { get; set; }

        public virtual List<Item> Items { get; set; }

    }
}

