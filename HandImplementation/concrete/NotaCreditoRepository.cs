using System;
using System.Collections.Generic;
using System.Linq;

using FSolv.model;
using FSolv.mapper.concrete;
using Interfaces;

namespace FSolv.concrete
{
    public class NotaCreditoRepository : INotaCreditoRepository
    {
        private IContext context;
        public NotaCreditoRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<INotaCredito> Find(Func<INotaCredito, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<INotaCredito> FindAll()
        {
            return new NotaCreditoMapper(context).ReadAll();
        }
    }
}
