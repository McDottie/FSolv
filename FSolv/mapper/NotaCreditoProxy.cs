using FSolv.mapper.concrete;
using FSolv.model;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSolv.mapper
{
    public class NotaCreditoProxy : NotaCredito
    {
        private IContext context;
        public NotaCreditoProxy(INotaCredito c, IContext ctx) : base()
        {
            context = ctx;

            base.Id = c.Id;
            base.State = c.State;
            base.DataEmissao = c.DataEmissao;
            base.Fatura = null;
            base.Items = null;
        }
        public override List<IItem> Items
        {
            get
            {
                if (base.Items == null)//lazy load
                {
                    NotaCreditoMapper cm = new NotaCreditoMapper(context);
                    base.Items = cm.LoadItems(this);
                }
                return base.Items;
            }

            set
            {
                base.Items = value;
            }
        }

        public override IFatura Fatura
        {
            get
            {
                if (base.Fatura == null)//lazy load
                {
                    NotaCreditoMapper cm = new NotaCreditoMapper(context);
                    base.Fatura = cm.Loadfatura(this);
                }
                return base.Fatura;
            }

            set
            {
                base.Fatura = value;
            }
        }
    }
}
