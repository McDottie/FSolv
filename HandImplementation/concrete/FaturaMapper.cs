/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*/

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

    class FaturaMapper : IFaturaMapper
    {
        #region HELPER METHODS  
        internal List<IItem> LoadItems(IFatura entity)
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

            lst = SQLMapperHelper.ExecuteMapSet<IItem,List<IItem>> (_ctx.Connection,
                                  "select id, quantidade, desconto, cod_prod from TP1.Item " +
                                  "join TP1.Fatura_item on Item.id = Fatura_item.cod_item " +
                                  "where id_fatura = @id",
                                    new[] { p }, map);
            
            return lst;
        }

        internal IContribuinte LoadContribuinte(IFatura fatura)
        {
            Mapper<IContribuinte> map = (value) =>
            {
                Contribuinte entity = new Contribuinte();
                entity.Name = value.GetString(0);
                if (!value.IsDBNull(1))
                    entity.Morada = value.GetString(1);
                entity.Nif = value.GetInt64(2);
                return new ContribuinteProxy(entity, _ctx);
            };

            IContribuinte c;

            SqlParameter p = new SqlParameter("@id", fatura.Id);

            c = SQLMapperHelper.ExecuteMapSingle<IContribuinte>(_ctx.Connection,
                                  "SELECT nome,morada,C.nif from TP1.Contribuinte C JOIN TP1.Fatura F on C.nif = F.nif where F.id = @id",
                                    new[] { p }, map);

            return c;
        }
        #endregion
        private object monitor;
        private readonly IContext _ctx;
        private const string INS_CMD = "insert into TP1.Fatura (id, nif, dt_emissao, estado) values (@id,@nif,getdate(),'Em atualizacao')";
        //private const string INS_CMD = "exec TP1.p_criaFactura @nif, @id output";
        private const string SEL_ALL_CMD = "select id,dt_emissao,estado,iva,valor_total from TP1.Fatura";
        private const string SEL_CMD = SEL_ALL_CMD + "where id=@id";
        private const string UPD_CMD = "exec TP1.alt_estado_fatura @id, @estado";
        private const string DEL_CMD = "delete from TP1.Fatura where id=@id";
        private const string ADD_ITEM = "exec  TP1.addItem_Fatura @id, @sku, @qnt, @desconto, @item_id output";

        public FaturaMapper(IContext ctx)
        {
            _ctx = ctx;
            monitor = new object();
        }

        public IFatura Create(IFatura entity)
        {
            string id = CreateId();
            SqlParameter p1 = new SqlParameter("@id", id);
            SqlParameter p2 = new SqlParameter("@nif", entity.Contribuinte.Nif);
            //SqlParameter p2 = new SqlParameter("@id", SqlDbType.VarChar,12);
            //p2.Direction = ParameterDirection.Output;


            SQLMapperHelper.ExecuteNonQuery(_ctx.Connection,
                                                            INS_CMD,
                                                            new[] { p1, p2 });
            entity.Id = id;
            return new FaturaProxy(entity, _ctx);
        }

        public IFatura Update(IFatura entity)
        {
            SqlParameter p = new SqlParameter("@id", entity.Id);
            SqlParameter p1 = new SqlParameter("@estado", entity.State);

            int i = SQLMapperHelper.ExecuteNonQuery(_ctx.Connection, UPD_CMD, new[] { p, p1 });
            return i != 1 ? null : new FaturaProxy(entity, _ctx);
        }

        public IFatura Read(string id)
        {
            SqlParameter p = new SqlParameter("@id", id);

            Mapper<IFatura> map = (value) =>
            {
                Fatura c = new Fatura();
                c.Id = value.GetString(0);
                c.DataEmissao = value.GetDateTime(1);
                c.State = value.GetString(2);
                c.Iva = value.GetDecimal(3);
                c.Total = value.GetDecimal(4);
                c.Contribuinte = null;
                c.Items = null;

                return new FaturaProxy(c, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSingle<IFatura>(_ctx.Connection,
                                                    SEL_CMD,
                                                    new[] { p },
                                                    map
                                                    );

        }

        public List<IFatura> ReadAll()
        {
            Mapper<IFatura> map = (value) =>
            {
                Fatura c = new Fatura();
                c.Id = value.GetString(0);
                if (!value.IsDBNull(1))
                    c.DataEmissao = value.GetDateTime(1);
                c.State = value.GetString(2);
                if(!value.IsDBNull(3))
                    c.Iva = value.GetDecimal(3);
                if (!value.IsDBNull(4))
                    c.Total = value.GetDecimal(4);
                c.Contribuinte = null;
                c.Items = null;

                return new FaturaProxy(c, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSet<IFatura, List<IFatura>>(_ctx.Connection,
                                                    SEL_ALL_CMD,
                                                    new IDbDataParameter[] { },
                                                    map
                                                    );
        }

        public IFatura Delete(IFatura entity)
        {
            SqlParameter p = new SqlParameter("@id", entity.Id);

            int i = SQLMapperHelper.ExecuteNonQuery(_ctx.Connection, DEL_CMD, new[] { p });
            return i != 1 ? null : new FaturaProxy(entity, _ctx);
        }

        public IItem addItem(IFatura fatura, IItem item)
        {
            SqlParameter p1 = new SqlParameter("@id", fatura.Id);
            SqlParameter p2 = new SqlParameter("@sku", item.ProdutoI.Sku);
            SqlParameter p3 = new SqlParameter("@qnt", item.Qnt);
            SqlParameter p4 = new SqlParameter("@Desconto", item.Desconto);
            SqlParameter p5 = new SqlParameter("@item_id", SqlDbType.Int);

            p5.Direction = ParameterDirection.Output;


            SQLMapperHelper.ExecuteNonQuery(_ctx.Connection,
                                                            ADD_ITEM,
                                                            new[] { p1, p2, p3, p4, p5 });
            item.Id = (int)p5.Value;
            return new ItemProxy(item, _ctx);
        }

        public string CreateId()
        {
            string createdCode = "";
            lock (monitor)
            {
                int code = 0;
                foreach (IFatura fatura in this.ReadAll())
                {
                    int curr = Convert.ToInt32(fatura.Id.Substring(7));
                    code = code > curr ? code : curr + 1;
                }

                createdCode = "FT" + DateTime.Now.Year + "-" + String.Format("{0,0:D5}", code);
            }
            return createdCode;
        }
    }
}