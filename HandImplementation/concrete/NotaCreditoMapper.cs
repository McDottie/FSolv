using FSolv.mapper.interfaces;
using FSolv.model;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using FSolv.helper;
using Interfaces;

namespace FSolv.mapper.concrete
{
    class NotaCreditoMapper : INotaCreditoMapper
    {
        #region HELPER METHODS 
        internal List<IItem> LoadItems(INotaCredito entity)
        {
            Mapper<IItem> map = (value) =>
            {
                Item i = new Item();
                i.Id = value.GetInt32(0);
                i.Qnt = value.GetInt32(1);
                i.Desconto = value.GetDecimal(2);

                return new ItemProxy(i, _ctx);
            };

            List<IItem> lst;

            SqlParameter p = new SqlParameter("@id", entity.Id);

            lst = SQLMapperHelper.ExecuteMapSet<IItem, List<IItem>>(_ctx.Connection,
                                  "select id, quantidade, desconto from TP1.Item " +
                                  "join TP1.NC_item on Item.id = NC_item.cod_item " +
                                  "where id_nc = @id",
                                    new[] { p }, map);

            return lst;
        }
        internal IFatura Loadfatura(INotaCredito entity)
        {
            Mapper<IFatura> map = (value) =>
            {
                Fatura f = new Fatura();
                f.Id = value.GetString(0);
                f.DataEmissao = value.GetDateTime(1);
                f.State = value.GetString(2);
                f.Iva = value.GetDecimal(3);
                f.Total = value.GetDecimal(4);

                return new FaturaProxy(f, _ctx);
            };

            IFatura lst;

            SqlParameter p = new SqlParameter("@id", entity.Id);

            lst = SQLMapperHelper.ExecuteMapSingle<IFatura>(_ctx.Connection,
                                  "select id, dt_emissao, estado, iva, valor_total from TP1.NotaCredito " +
                                  "join TP1.Fatura on TP1.Fatura.id = id_fatura " +
                                  "where id = @id",
                                    new[] { p }, map);

            return lst;
        }
        #endregion

        private readonly IContext _ctx;
        private const string INS_CMD = "exec TP1.criaNotaCredito @id_fatura, @id output";
        private const string SEL_ALL_CMD = "select id,estado,id_fatura from TP1.Nota_Credito";
        private const string SEL_CMD = SEL_ALL_CMD + "where id=@id";
        private const string UPD_CMD = "update TP1.Nota_credito set id=@id, estado=@estado, id_fatura=@id_fatura  WHERE id=@id";
        private const string DEL_CMD = "delete from TP1.Nota_Credito where id=@id";
        private const string ADD_ITEM = "exec  TP1.addItem_NC @cod_nc, @item, @qnt";
        private const string LIST_NC = "select id,estado,id_fatura from TP1.ListNC(@date)";

        public NotaCreditoMapper(IContext ctx)
        {
            this._ctx=ctx;
        }

        public INotaCredito Create(INotaCredito entity)
        {
            SqlParameter p1 = new SqlParameter("@id_fatura",entity.Fatura.Id);
            SqlParameter p2 = new SqlParameter("@id", SqlDbType.VarChar, 12);

            p2.Direction = ParameterDirection.Output;
            if(entity.Id != null)
                p2.Value = entity.Id;
            else
                p2.Value = DBNull.Value;

            SQLMapperHelper.ExecuteNonQuery(_ctx.Connection,
                                                            INS_CMD,
                                                            new[] { p1, p2 });
            entity.Id = (string)p2.Value;
            return entity;

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
                c.State = value.GetString(1);
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
                c.State = value.GetString(1);
                c.Fatura = null;
                c.Items = null;

                return new NotaCreditoProxy(c, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSet<INotaCredito, List<INotaCredito>>(_ctx.Connection,
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

        public void addItem(INotaCredito nc, IItem item)
        {
            SqlParameter p1 = new SqlParameter("@cod_nc", nc.Id);
            SqlParameter p2 = new SqlParameter("@item", item.Id);
            SqlParameter p3 = new SqlParameter("@qnt", item.Qnt);

            SQLMapperHelper.ExecuteNonQuery(_ctx.Connection,
                                                            ADD_ITEM,
                                                            new[] { p1, p2, p3});
        }

        public IEnumerable<INotaCredito> ListNCFromYear(DateTime dateTime)
        {
            SqlParameter p1 = new SqlParameter("@date", dateTime);

            Mapper<INotaCredito> map = (value) =>
            {
                NotaCredito c = new NotaCredito();
                c.Id = value.GetString(0);
                c.State = value.GetString(1);
                c.Fatura = null;
                c.Items = null;

                return new NotaCreditoProxy(c, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSet<INotaCredito, List<INotaCredito>>(_ctx.Connection,
                                                    LIST_NC,
                                                    new IDbDataParameter[] { p1},
                                                    map
                                                    );

        }
    }
}
