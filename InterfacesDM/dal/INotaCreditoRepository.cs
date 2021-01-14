using System;
using System.Collections.Generic;
using Interfaces;

namespace FSolv
{
    public interface INotaCreditoRepository : IRepository<INotaCredito>
    {
        void AddItemToNC(INotaCredito nc, IItem item);
        IEnumerable<INotaCredito> ListNCFromYear(DateTime dateTime);

    }
}
