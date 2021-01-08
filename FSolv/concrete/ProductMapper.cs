using System.Collections.Generic;
using FSolv.mapper;
using FSolv.model;
using FSolv.mapper.interfaces;
using System.Data;
using FSolv;
using FSolv.helper;
using System.Data.SqlClient;

namespace FSolv.mapper.concrete
{

    class ProductMapper : IProductMapper
    {

        public IContext ctx;
        public ProductMapper(IContext ctx) { this.ctx = ctx; }

        private const string SEL_ALL_CMD = "select sku, descricao, valor, iva from TP1.Produto";

        private const string SEL_CMD = SEL_ALL_CMD + " where sku=@sku";

        private const string UPD_CMD = "update TP1.Produto set valor=@valor where sku=@sku";

        private const string DEL_CMD = "delete from TP1.Produto where sku=@sku";

        private const string INS_CMD = "insert into TP1.Produto(sku, valor, descricao, iva) values(@sku, @valor, @descricao, @iva)";

        public IProduct Create(IProduct product)
        {
            SqlParameter sku = new SqlParameter("@sku", product.Sku);
            SqlParameter valor = new SqlParameter("@valor", product.Valor);
            SqlParameter descricao = new SqlParameter("@descricao", product.Descricao);
            SqlParameter iva = new SqlParameter("@iva", product.Iva);

            product.Sku = SQLMapperHelper.ExecuteScalar<int>(ctx.Connection, INS_CMD, new[] { sku, valor, descricao, iva });

            return new ProductProxy(product, ctx);
        }

        public IProduct Read(int? id)
        {
            SqlParameter sku = new SqlParameter("@sku", id);

            Mapper<IProduct> map = (value) =>
            {
                Product p = new Product();
                p.Sku = id;
                p.Descricao = value.GetString(1);
                p.Valor = value.GetInt32(2);
                p.Iva = value.GetInt32(3);
                return new ProductProxy(p, ctx);
            };

            return SQLMapperHelper.ExecuteMapSingle<IProduct>(ctx.Connection, SEL_CMD, new[] { sku }, map);
        }

        public List<IProduct> ReadAll()
        {
            Mapper<IProduct> map = (value) =>
            {
                Product p = new Product();
                p.Sku = value.GetInt32(0);
                p.Descricao = value.GetString(1);
                p.Valor = value.GetDouble(2);
                p.Iva = value.GetDouble(3);
                return new ProductProxy(p, ctx);
            };

            return SQLMapperHelper.ExecuteMapSet<IProduct, List<IProduct>>(ctx.Connection, SEL_ALL_CMD, new IDbDataParameter[] { }, map);
        }

        public IProduct Update(IProduct product)
        {
            SqlParameter sku = new SqlParameter("@sku", product.Sku);
            SqlParameter valor = new SqlParameter("@valor", product.Valor);

            int res = SQLMapperHelper.ExecuteNonQuery(ctx.Connection, UPD_CMD, new[] { sku, valor });
            return res != 1 ? null : new ProductProxy(product, ctx);
        }

        public IProduct Delete(IProduct product)
        {
            SqlParameter sku = new SqlParameter("@sku", product.Sku);

            int res = SQLMapperHelper.ExecuteNonQuery(ctx.Connection, DEL_CMD, new[] { sku });
            return res != 1 ? null : new ProductProxy(product, ctx);
        }
    }
}
