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
using Interfaces;
using System;
using System.Collections.Generic;

namespace FSolv.model

{
    public class Fatura : IFatura
    {
        public string Id { get; set; }
        public Nullable<DateTime> DataEmissao { get; set; }
        
        public string State { get; set; }

        public virtual IContribuinte Contribuinte { get; set; }

        public Nullable<decimal> Iva { get; set; }
        
        public Nullable<decimal> Total { get; set; }

        public virtual List<IItem> Items { get; set; }

    }
}

