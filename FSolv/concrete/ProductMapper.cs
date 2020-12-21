using System.Collections.Generic;
using FSolv.mapper;
using FSolv.model;
using FSolv.mapper.interfaces;
using System.Data;
using FSolv;

namespace FSolv.mapper.concrete
{

    class ProductMapper : AbstracMapper<Product, int?, List<Product>>, IProductMapper
    {

        public ProductMapper(IContext ctx) : base(ctx) { }

        protected override string SelectAllCommandText => throw new System.NotImplementedException();

        protected override string SelectCommandText => throw new System.NotImplementedException();

        protected override string UpdateCommandText => throw new System.NotImplementedException();

        protected override string DeleteCommandText => throw new System.NotImplementedException();

        protected override string InsertCommandText => throw new System.NotImplementedException();

        protected override void DeleteParameters(IDbCommand command, Product e)
        {
            throw new System.NotImplementedException();
        }

        protected override void InsertParameters(IDbCommand command, Product e)
        {
            throw new System.NotImplementedException();
        }

        protected override Product Map(IDataRecord record)
        {
            throw new System.NotImplementedException();
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            throw new System.NotImplementedException();
        }

        protected override Product UpdateEntityID(IDbCommand cmd, Product e)
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateParameters(IDbCommand command, Product e)
        {
            throw new System.NotImplementedException();
        }
    }
}
