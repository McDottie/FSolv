﻿/*
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
*/

using Interfaces;

namespace FSolv
{
    public interface IFaturaRepository:IRepository<IFatura>
    {
        void AddItemToFatura(IFatura fatura, IItem item);

        void ExchangeNif(IFatura fatura1, IFatura fatura2);
    }
}
