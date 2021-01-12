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
        public SqlConnection Connection => throw new NotImplementedException();

        public IContribuinteRepository Contribuinte => new ContribuinteRepository(this);

        public IFaturaRepository Fatura => throw new NotImplementedException();

        public IItemRepository Item => throw new NotImplementedException();

        public INotaCreditoRepository NotaCredito => throw new NotImplementedException();

        public IProductRepository Produto => throw new NotImplementedException();

        public IObjectPool Registry => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
