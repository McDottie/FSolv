using EntityF.implementations.context;
using FSolv;
using FSolv.IndentityMap;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF
{
    public partial class masterEntities : IContext
    {
        public masterEntities(string dummyConnectionString)
            : base("name=masterEntities")
        {
        }
        public SqlConnection Connection => throw new NotImplementedException();

        public IContribuinteRepository Contribuinte => new ContribuinteRepository(this);

        public IFaturaRepository Fatura => new FaturaRepository(this);

        public IItemRepository Item => new ItemRepository(this);

        public INotaCreditoRepository NotaCredito => new NotaCreditoRepository(this);

        public IProductRepository Produto => new ProdutoRepository(this);

        public IObjectPool Registry => throw new NotImplementedException();

        public void Dispose()
        {
        }
    }
}
