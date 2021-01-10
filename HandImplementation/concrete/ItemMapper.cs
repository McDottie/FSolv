using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using FSolv.model;
using FSolv.mapper.interfaces;
using FSolv.helper;
using Interfaces;

namespace FSolv.mapper.concrete
{

    class ItemMapper : IItemMapper
    {
        #region HELPER METHODS  
        internal IFatura LoadFatura(IItem entity)
        {
            Mapper<IFatura> map = (value) =>
            {
                Fatura f = new Fatura();
                f.Id = value.GetString(0);
                f.DataEmissao = value.GetDateTime(1);
                f.State = value.GetString(2);
                f.Iva = value.GetDecimal(3);
                f.Total = value.GetDecimal(4);

                return new FaturaProxy(f, ctx);
            };

            IFatura lst;

            SqlParameter p = new SqlParameter("@id", entity.Id);

            lst = SQLMapperHelper.ExecuteMapSingle<IFatura>(ctx.Connection,
                                  "select id_fatura, dt_emissao, estado, iva, valor_total from TP1.Fatura_item " +
                                  "join TP1.Fatura on TP1.Fatura.id = id_fatura " +
                                  "where cod_item = @id",
                                    new[] { p }, map);

            return lst;
        }

        internal INotaCredito LoadNotaCredito(IItem entity)
        {
            Mapper<INotaCredito> map = (value) =>
            {
                NotaCredito n = new NotaCredito();
                n.Id = value.GetString(0);
                n.State = value.GetString(1);

                return new NotaCreditoProxy(n, ctx);
            };

            INotaCredito lst;

            SqlParameter p = new SqlParameter("@id", entity.Id);

            lst = SQLMapperHelper.ExecuteMapSingle<INotaCredito>(ctx.Connection,
                                  "select id, estado from TP1.NC_item " +
                                  "join TP1.Nota_Credito on TP1.Nota_Credito.id = id_nc " +
                                  "where id = @id",
                                    new[] { p }, map);

            return lst;
        }

        internal IProduto LoadProduct(IItem entity)
        {
            Mapper<IProduto> map = (value) =>
            {
                Produto prod = new Produto();
                prod.Sku = value.GetInt32(0);
                prod.Descricao = value.GetString(1);
                prod.Valor = value.GetDecimal(2);
                prod.Iva = value.GetDecimal(3);
                return prod;
            };

            IProduto prd;

            SqlParameter p = new SqlParameter("@id", entity.Id);

            prd = SQLMapperHelper.ExecuteMapSingle<IProduto>(ctx.Connection,
                                  "select sku, descricao, valor, iva from TP1.Produto join TP1.Item on cod_prod = sku where id = @id",
                                    new[] { p }, map);

            return prd;
        }
        #endregion

        public IContext ctx;
        public ItemMapper(IContext ctx) { this.ctx = ctx; }

        private const string DEL_CMD = "delete from TP1.Item where id=@id";

        private const string INS_CMD = "insert into TP1.Item(quantidade, desconto, cod_prod) output INSERTED.id values(@quantidade, @desconto, @cod_prod); ";

        private const string SEL_ALL_CMD = "select id, quantidade, desconto from TP1.Item";

        private const string SEL_CMD = SEL_ALL_CMD + " where id=@id";

        private const string UPD_CMD = "update TP1.Item set desconto=@desconto, quantidade=@quantidade where id=@id";

        private const string INS_NC = "exec TP1.addItem_NC (@nc_cod, @item, @qnt)";

        private const string INS_FT = "exec TP1.addItem_Fatura (@cod_fatura, @sku, @qnt, @desconto, ouput @id)";
        

        public IItem Create(IItem item)
        {
            if (item.Id != null)
            {
                return InsertItemInNC(item);
            } else
            {
                SqlParameter cod_ft = new SqlParameter("@cod_fatura", item.Fatura.Id);
                SqlParameter cod_prod = new SqlParameter("@cod_prod", item.Produto.Sku);
                SqlParameter quantidade = new SqlParameter("@quantidade", item.Qnt);
                SqlParameter desconto = new SqlParameter("@desconto", item.Desconto);
                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                
                id.Direction = ParameterDirection.Output;


                item.Id = SQLMapperHelper.ExecuteScalar<int>(ctx.Connection, INS_CMD, new[] { cod_ft, cod_prod, quantidade, desconto, id });

                return new ItemProxy(item, ctx);

            }

        }

        private IItem InsertItemInNC(IItem item)
        {
            INotaCredito nc = item.NotaCredito;

            SqlParameter nc_cod = new SqlParameter("@nc_cod", nc.Id);
            SqlParameter id = new SqlParameter("@item", item.Id);
            SqlParameter quantidade = new SqlParameter("@qnt", item.Qnt);

            int res = SQLMapperHelper.ExecuteNonQuery(ctx.Connection, INS_NC, new[] { nc_cod, id,quantidade });
            return res != 1 ? null : new ItemProxy(item, ctx);
        }

        public IItem Read(int? id)
        {
            SqlParameter i = new SqlParameter("@id", id);

            Mapper<IItem> map = (value) =>
            {
                Item item = new Item();
                item.Id = id;
                item.Qnt = value.GetInt32(2);
                item.Desconto = value.GetDecimal(3);
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
                i.Qnt = value.GetInt32(1);
                i.Desconto = value.GetDecimal(2);
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
