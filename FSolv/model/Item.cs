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

namespace FSolv.model
{
    public class Item
    {
        public int? Id { get; set; }
        public double Desconto { get; set; }
        public int Qnt { get; set; }
        public virtual Product Product { get; set; }
        public virtual Fatura Fatura { get; set; }
        public virtual NotaCredito NotaCredito { get; set; }

    }
}

