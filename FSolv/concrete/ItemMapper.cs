using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using FSolv.model;
using FSolv.mapper.interfaces;
using FSolv.helper;

namespace FSolv.mapper.concrete
{

    class ItemMapper : IItemMapper
    {
        #region HELPER METHODS  
        internal Fatura LoadFatura(Item s)
        {
            throw new NotImplementedException();
        }

        internal NotaCredito LoadNotaCredito(Item s)
        {
            throw new NotImplementedException();
        }

        internal Product LoadProduct(Item s)
        {
            throw new NotImplementedException();
        }
        #endregion

        public IContext ctx;
        public ItemMapper(IContext ctx) { this.ctx = ctx; }

        private const string DEL_CMD = "delete from TP1.Item where id=@id";

        private const string INS_CMD = "insert into TP1.Item(quantidade, desconto, cod_prod) values(@quantidade, @desconto, @cod_prod)";

        private const string SEL_ALL_CMD = "select id, cod_prod, quantidade, desconto from TP1.Item";

        private const string SEL_CMD = SEL_ALL_CMD + " where id=@id";

        private const string UPD_CMD = "update TP1.Item set desconto=@desconto, quantidade=@quantidade where id=@id";

        public IItem Create(IItem item)
        {
            SqlParameter quantidade = new SqlParameter("@quantidade", item.Qnt);
            SqlParameter desconto = new SqlParameter("@desconto", item.Desconto);

            // falta aqui o cod_prod
            item.Id = SQLMapperHelper.ExecuteScalar<int>(ctx.Connection, INS_CMD, new[] { quantidade, desconto });

            return new ItemProxy(item, ctx);
        }

        public IItem Read(int? id)
        {
            SqlParameter i = new SqlParameter("@id", id);

            Mapper<IItem> map = (value) =>
            {
                Item item = new Item();
                item.Id = id;
                item.Qnt = value.GetInt32(2);
                item.Desconto = value.GetDouble(3);
                // falta o produto
                return new ItemProxy(item, ctx);
            };

            return SQLMapperHelper.ExecuteMapSingle<IItem>(ctx.Connection, SEL_CMD, new[] { i }, map);
        }

        public List<IItem> ReadAll()
        {
            Mapper<IItem> map = (value) =>
            {
                Item i = new Item();
                i.Id = value.GetInt32(0);
                i.Desconto = value.GetDouble(1);
                i.Qnt = value.GetInt32(2);
                // falta o produto
                return new ItemProxy(i, ctx);
            };

            return SQLMapperHelper.ExecuteMapSet<IItem, List<IItem>>(ctx.Connection, SEL_ALL_CMD, new IDbDataParameter[] { }, map);
        }

        public IItem Update(IItem item)
        {
            SqlParameter desconto = new SqlParameter("@desconto", item.Desconto);
            SqlParameter quantidade = new SqlParameter("@quantidade", item.Qnt);
            SqlParameter id = new SqlParameter("@id", item.Id);

            int res = SQLMapperHelper.ExecuteNonQuery(ctx.Connection, UPD_CMD, new[] { desconto, quantidade, id });
            return res != 1 ? null : new ItemProxy(item, ctx);
        }

        public IItem Delete(IItem item)
        {
            SqlParameter id = new SqlParameter("@id", item.Id);

            int res = SQLMapperHelper.ExecuteNonQuery(ctx.Connection, DEL_CMD, new[] { id });
            return res != 1 ? null : new ItemProxy(item, ctx);
        }
    }
}
