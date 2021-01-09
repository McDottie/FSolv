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

namespace FSolv.model
{
    public class Item : IItem
    {
        public int? Id { get; set; }
        public double Desconto { get; set; }
        public int Qnt { get; set; }
        public virtual IProduto Product { get; set; }
        public virtual IFatura Fatura { get; set; }
        public virtual INotaCredito NotaCredito { get; set; }

    }
}

