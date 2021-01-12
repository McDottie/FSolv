using FSolv;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityF.implementations.context
{
    class ItemRepository : IItemRepository
    {
        public void Add(IItem entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IItem entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> Find(Func<IItem, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(IItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
