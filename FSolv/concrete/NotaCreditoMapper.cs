using FSolv.mapper.interfaces;
using FSolv.model;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace FSolv.mapper.concrete
{
    class NotaCreditoMapper : AbstracMapper<NotaCredito, int?, List<NotaCredito>>, INotaCreditoMapper
    {
        #region HELPER METHODS 
        internal List<Item> LoadItems(NotaCreditoProxy notaCreditoProxy)
        {
            throw new NotImplementedException();
        }
        internal Fatura Loadfatura(NotaCreditoProxy notaCreditoProxy)
        {
            throw new NotImplementedException();
        }
        #endregion
        public NotaCreditoMapper(IContext ctx) : base(ctx)
        {
        }
        protected override string SelectAllCommandText => throw new NotImplementedException();

        protected override string SelectCommandText => throw new NotImplementedException();

        protected override string UpdateCommandText => throw new NotImplementedException();

        protected override string DeleteCommandText => throw new NotImplementedException();

        protected override string InsertCommandText => throw new NotImplementedException();

        protected override void DeleteParameters(IDbCommand command, NotaCredito e)
        {
            throw new NotImplementedException();
        }

        protected override void InsertParameters(IDbCommand command, NotaCredito e)
        {
            throw new NotImplementedException();
        }

        protected override NotaCredito Map(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            throw new NotImplementedException();
        }

        protected override NotaCredito UpdateEntityID(IDbCommand cmd, NotaCredito e)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateParameters(IDbCommand command, NotaCredito e)
        {
            throw new NotImplementedException();
        }
    }
}
