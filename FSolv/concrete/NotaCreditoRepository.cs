using System;
using System.Collections.Generic;
using System.Linq;

using FSolv.model;
using FSolv.mapper.concrete;

namespace FSolv.concrete
{
    public class NotaCreditoRepository
    {
        private IContext context;
        public NotaCreditoRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<NotaCredito> Find(Func<NotaCredito, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<NotaCredito> FindAll()
        {
            return new NotaCreditoMapper(context).ReadAll();
        }
    }
}
