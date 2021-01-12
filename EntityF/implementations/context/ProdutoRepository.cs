using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    class ProdutoRepository : IProductRepository
    {
        public void Add(IProduto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IProduto entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProduto> Find(Func<IProduto, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProduto> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(IProduto entity)
        {
            throw new NotImplementedException();
        }
    }
}
