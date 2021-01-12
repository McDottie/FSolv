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

using FSolv.model;
using Interfaces;
using System;

namespace FSolv.model
{
    public class Item : IItem
    {
        public int? Id { get; set; }
        public Nullable<decimal> Desconto { get; set; }
        public Nullable<int> Qnt { get; set; }
        public virtual IProduto Produto { get; set; }
        public virtual IFatura Fatura { get; set; }
        public virtual INotaCredito NotaCredito { get; set; }

    }
}

