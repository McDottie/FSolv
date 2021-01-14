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

        public void Add(INotaCredito entity)
        {
            new NotaCreditoMapper(context).Create(entity);
        }

        public void AddItemToNC(INotaCredito nc, IItem item)
        {
            new NotaCreditoMapper(context).addItem(nc, item);
        }

        public void Delete(INotaCredito entity)
        {
            new NotaCreditoMapper(context).Delete(entity);
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

        public IEnumerable<INotaCredito> ListNCFromYear(DateTime dateTime)
        {
            return new NotaCreditoMapper(context).ListNCFromYear(dateTime);
        }

        public bool Save()
        {
            return true;
        }

        public void Update(INotaCredito entity)
        {
            new NotaCreditoMapper(context).Update(entity);
        }
    }
}
