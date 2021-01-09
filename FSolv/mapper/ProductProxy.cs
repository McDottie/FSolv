using FSolv.model;

namespace FSolv.mapper
{
    class ProductProxy : Product
    {
        private IContext context;
        public ProductProxy(IProduct p, IContext ctx) : base()
        {
            context = ctx;

            base.Sku = p.Sku;
            base.Descricao = p.Descricao;
            base.Valor = p.Valor;
            base.Iva = p.Iva;
        }
    }
}
