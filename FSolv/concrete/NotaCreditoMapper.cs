using FSolv.mapper.interfaces;
using FSolv.model;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using FSolv.helper;


namespace FSolv.mapper.concrete
{
    class NotaCreditoMapper : INotaCreditoMapper
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
        
        private readonly IContext _ctx;
        private const string INS_CMD = "exec TP1.criaNotaCredito (@id_fatura, @id)";
        private const string SEL_ALL_CMD = "select id,estado,id_fatura from TP1.Nota_Credito";
        private const string SEL_CMD = SEL_ALL_CMD + "where id=@id";
        private const string UPD_CMD = "update TP1.Nota_credito set id=@id, estado=@estado, id_fatura=@id_fatura  WHERE id=@id";
        private const string DEL_CMD = "delete from TP1.Nota_Credito where id=@id";

        public NotaCreditoMapper(IContext ctx)
        {
            this._ctx=ctx;
        }

        public INotaCredito Create(INotaCredito entity)
        {
            SqlParameter p1 = new SqlParameter("@id_fatura",entity.Fatura.Id);
            SqlParameter p2 = new SqlParameter("@id", SqlDbType.VarChar);

            //p2.Direction = ParameterDirection.Output; ??
            if(entity.Id != null)
                p2.Value = entity.Id;
            else
                p2.Value = DBNull.Value;

            entity.Id = SQLMapperHelper.ExecuteScalar<string>(_ctx.Connection,
                                                            INS_CMD,
                                                            new[] { p1, p2 });

        }

        public INotaCredito Update(INotaCredito entity)
        {
            SqlParameter p = new SqlParameter("@id", entity.Id);
            SqlParameter p1 = new SqlParameter("@estado", entity.State);
            SqlParameter p2 = new SqlParameter("@id_fatura", entity.Fatura);

            int i = SQLMapperHelper.ExecuteNonQuery(_ctx.Connection,UPD_CMD,new[] { p, p1, p2 });
            return i != 1 ? null : new NotaCreditoProxy(entity, _ctx);
        }

        public INotaCredito Read(string id)
        {
            SqlParameter p = new SqlParameter("@id", id);

            Mapper<INotaCredito> map = (value) =>
            {
                NotaCredito c = new NotaCredito();
                c.Id = value.GetString(0);
                c.DataEmissao = value.GetDateTime(1);
                c.State = value.GetString(2);
                c.Fatura = null;
                c.Items = null;

                return new NotaCreditoProxy(c, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSingle<INotaCredito>(_ctx.Connection,
                                                    SEL_CMD,
                                                    new[] { p },
                                                    map
                                                    );

        }

        public List<INotaCredito> ReadAll()
        {
            Mapper<INotaCredito> map = (value) =>
            {
                NotaCredito c = new NotaCredito();
                c.Id = value.GetString(0);
                c.DataEmissao = value.GetDateTime(1);
                c.State = value.GetString(2);
                c.Fatura = null;
                c.Items = null;

                return new NotaCreditoProxy(c, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSet<IFatura, List<INotaCredito>>(_ctx.Connection,
                                                    SEL_ALL_CMD,
                                                    new IDbDataParameter[] { },
                                                    map
                                                    );
        }

        public INotaCredito Delete(INotaCredito entity)
        {
            SqlParameter p = new SqlParameter("@id", entity.Id);

            int i = SQLMapperHelper.ExecuteNonQuery(_ctx.Connection, DEL_CMD, new[] { p });
            return i != 1 ? null : new NotaCreditoProxy(entity, _ctx);
        }
    }
}
