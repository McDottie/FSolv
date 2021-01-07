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

using System.Collections.Generic;

namespace FSolv.model

{
    public class Contribuinte : IContribuinte
    {
        public virtual int? Nif { get; set; }
        public virtual string Name { get; set; }
        public virtual string Morada { get; set; }
        public virtual List<Fatura> Faturas { get; set; }

    }
}

