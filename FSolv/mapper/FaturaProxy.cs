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
*/
using FSolv;
using FSolv.model;
using System.Data;
using System.Collections.Generic;

namespace FSolv.mapper.concrete
{
    class FaturaProxy: Fatura
    {
        private IContext context;
        public FaturaProxy(Fatura c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.Iva = c.Iva;
            base.State = c.State;
            base.Total = c.Total;
            base.DataEmissao = c.DataEmissao;
            base.Contribuinte = null;
            base.Items = null;        
        }
        public override List<Item> Items
        {
            get
            {
                if (base.Items == null)//lazy load
                {
                    FaturaMapper cm = new FaturaMapper(context);
                    base.Items = cm.LoadItems(this);
                }
                return base.Items;
            }

            set
            {
                base.Items = value;
            }
        }

        public override Contribuinte Contribuinte
        {
            get
            {
                if (base.Contribuinte == null)//lazy load
                {
                    FaturaMapper cm = new FaturaMapper(context);
                    base.Contribuinte = cm.LoadContribuinte(this);
                }
                return base.Contribuinte;
            }

            set
            {
                base.Contribuinte = value;
            }
        }
    }
}
