using FSolv.dal;
using FSolv.mapper.concrete;
using FSolv.model;
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
        public IEnumerable<Product> Find(Func<Product, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Product> FindAll()
        {
            return new ProductMapper(context).ReadAll();
        }
    }
}
