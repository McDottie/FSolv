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
                i.Desconto = value.GetDouble(2);

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
                entity.Morada = value.GetString(1);
                entity.Nif = value.GetInt32(2);
                return new ContribuinteProxy(entity, _ctx);
            };

            IContribuinte c;

            SqlParameter p = new SqlParameter("@id", fatura.Id);

            c = SQLMapperHelper.ExecuteMapSingle<IContribuinte>(_ctx.Connection,
                                  "SELECT name,morada,nif from TP1.Contribuinte JOIN TP1.Fatura where id_fatura = @id",
                                    new[] { p }, map);

            return c;
        }
        #endregion

        private readonly IContext _ctx;
        private const string INS_CMD = "exec TP1.p_criaFactura (@nif, output @id)";
        private const string SEL_ALL_CMD = "select id,dt_emissao,estado,iva,valor_total from TP1.Fatura";
        private const string SEL_CMD = SEL_ALL_CMD + "where id=@id";
        private const string UPD_CMD = "exec TP1.alt_estado_fatura (@id, @estado)";
        private const string DEL_CMD = "delete from TP1.Fatura where id=@id";

        public FaturaMapper(IContext ctx)
        {
            _ctx = ctx;

        }

        public IFatura Create(IFatura entity)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.VarChar);
            SqlParameter p2 = new SqlParameter("@nif", entity.Contribuinte.Nif);

            p1.Direction = ParameterDirection.Output;
            if (entity.Id != null)
                p1.Value = entity.Id;
            else
                p1.Value = DBNull.Value;

            entity.Id = SQLMapperHelper.ExecuteScalar<string>(_ctx.Connection,
                                                            INS_CMD,
                                                            new[] { p1, p2 });
            return new FaturaProxy(entity, _ctx);
        }

        public IFatura Update(IFatura entity)
        {
            SqlParameter p = new SqlParameter("@id", entity.Id);
            SqlParameter p1 = new SqlParameter("@nif", entity.State);

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
                c.Iva = value.GetDouble(3);
                c.Total = value.GetDouble(4);
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
                c.DataEmissao = value.GetDateTime(1);
                c.State = value.GetString(2);
                c.Iva = value.GetDouble(3);
                c.Total = value.GetDouble(4);
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
    }
}