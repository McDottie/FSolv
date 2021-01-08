using FSolv.model;

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

        public override Fatura Fatura
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

        public override NotaCredito NotaCredito
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

        public override Product Product
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
