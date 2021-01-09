<<<<<<< HEAD
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
using FSolv;
using FSolv.model;
using System.Data;
using System.Collections.Generic;
using Interfaces;

namespace FSolv.mapper.concrete
{
    class ItemProxy : Item
    {
        private IContext context;
        
        public ItemProxy(IItem s, IContext ctx):base()
        {
            base.Desconto = s.Desconto;
            base.Id = s.Id;
            base.Fatura= null;
            base.NotaCredito = null;
            base.Product = null;

            context = ctx;
        }

        public override IFatura Fatura
        {
            get
            {
                if (base.Fatura == null) //lazy load
                {
                    ItemMapper sm = new ItemMapper(context);
                    base.Fatura = sm.LoadFatura(this);
                }
                return base.Fatura;
            }

            set
            {
                base.Fatura = value;
            }
        }

        public override INotaCredito NotaCredito
        {
            get
            {
                if (base.NotaCredito == null)
                {
                    ItemMapper sm = new ItemMapper(context);
                    base.NotaCredito = sm.LoadNotaCredito(this);
                }
                return base.NotaCredito;
            }

            set
            {
                base.NotaCredito = value;
            }
        }

        public override IProduto Product
        {
            get
            {
                if (base.Product == null)
                {
                    ItemMapper sm = new ItemMapper(context);
                    base.Product = sm.LoadProduct(this);
                }
                return base.Product;
            }

            set
            {
                base.Product = value;
            }
        }

    }
}
