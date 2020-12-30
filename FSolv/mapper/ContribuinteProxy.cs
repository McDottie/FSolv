using FSolv.mapper.concrete;
using FSolv.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSolv.mapper
{
    class ContribuinteProxy : Contribuinte
    {
        private IContext context;
        public ContribuinteProxy(Contribuinte c, IContext ctx) : base()
        {
            context = ctx;
            base.Nif = c.Nif;
            base.Name = c.Name;
            base.Morada = c.Morada;

            base.Faturas = null;
        }
        public override List<Fatura> Faturas
        {
            get
            {
                if (base.Faturas == null)//lazy load
                {
                    ContribuinteMapper cm = new ContribuinteMapper(context);
                    base.Faturas = cm.LoadFaturas(this);
                }
                return base.Faturas;
            }

            set
            {
                base.Faturas = value;
            }
        }
    }
}
