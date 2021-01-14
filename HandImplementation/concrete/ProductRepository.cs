using FSolv.mapper.concrete;
using FSolv.model;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSolv.concrete
{
    public class ProductRepository : IProductRepository
    {
        private IContext context;
        public ProductRepository(IContext ctx)
        {
            context = ctx;
        }

        public void Add(IProduto entity)
        {
          new ProductMapper(context).Create(entity);
        }

        public void Delete(IProduto entity)
        {
            new ProductMapper(context).Delete(entity);
        }

        public IEnumerable<IProduto> Find(Func<IProduto, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<IProduto> FindAll()
        {
            return new ProductMapper(context).ReadAll();
        }

        public bool Save()
        {
            return true;
        }

        public void Update(IProduto entity)
        {
            new ProductMapper(context).Update(entity);
        }
    }
}
