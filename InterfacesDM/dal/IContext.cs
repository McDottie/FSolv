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
using System;
using System.Transactions;
using System.Data.SqlClient;
using FSolv.IndentityMap;

namespace FSolv
{
    public interface IContext: IDisposable
    {
        SqlConnection Connection { get; }

        IContribuinteRepository Contribuinte { get; }
        IFaturaRepository Fatura { get; }
        IItemRepository Item { get; }
        INotaCreditoRepository NotaCredito { get; }
        IProductRepository Produto { get; }
        IObjectPool Registry { get; }
    }
}
