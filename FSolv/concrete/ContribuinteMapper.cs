﻿/*
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
    class ContribuinteMapper : IContribuinteMapper
    {
        #region HELPER METHOD 
        internal List<IFatura> LoadFaturas(ContribuinteProxy contribuinteProxy)
        {
            throw new NotImplementedException();
        }
        #endregion

        private readonly IContext _ctx;
        private const string INS_CMD = "INSERT INTO TP1.Contribuinte (name,nif,morada) VALUES(@Name,@Nif,@Morada)";
        private const string SEL_ALL_CMD = "select name,nif,morada from TP1.Contribuinte";
        private const string SEL_CMD =  SEL_ALL_CMD + "where nif=@Nif";
        private const string UPD_CMD = "update TP1.Contribuinte set morada=@Morada where nif=@Nif";
        private const string DEL_CMD = "delete from TP1.Contribuinte where nif=@id";


        public ContribuinteMapper(IContext ctx)
        {
            this._ctx = ctx;
        }

        public IContribuinte Create(IContribuinte entity)
        {
            SqlParameter p1 = new SqlParameter("@Morada", entity.Morada);
            SqlParameter p2 = new SqlParameter("@Nif", entity.Nif);

            entity.Nif = SQLMapperHelper.ExecuteScalar<int>(_ctx.Connection,
                                                            INS_CMD,
                                                            new[] { p1, p2 });
            return new ContribuinteProxy(entity,_ctx);
        }

        public IContribuinte Update(IContribuinte entity)
        {
            SqlParameter p = new SqlParameter("@Name", entity.Name);
            SqlParameter p1 = new SqlParameter("@Morada", entity.Morada);
            SqlParameter p2 = new SqlParameter("@Nif", entity.Morada);

            int i = SQLMapperHelper.ExecuteNonQuery(_ctx.Connection,UPD_CMD,new[] { p, p1, p2 });
            return i != 1 ? null : new ContribuinteProxy(entity, _ctx);
        }

        public IContribuinte Read(int? id)
        {
            SqlParameter p = new SqlParameter("@Nif", id);

            Mapper<IContribuinte> map = (value) =>
            {
                Contribuinte entity = new Contribuinte();
                entity.Name = value.GetString(0);
                entity.Nif = id;
                return new ContribuinteProxy(entity, _ctx);
            };

            return SQLMapperHelper.ExecuteMapSingle<IContribuinte>(_ctx.Connection,
                                                    SEL_CMD,
                                                    new[] { p },
                                                    map
                                                    );

        }

        public List<IContribuinte> ReadAll()
        {
            Mapper<IContribuinte> map = (value) =>
            {
                Contribuinte entity = new Contribuinte();
                entity.Name = value.GetString(0);
                entity.Nif = value.GetInt32(1);
                entity.Morada = value.GetString(2);

                return new ContribuinteProxy(entity, _ctx); ;
            };

            return SQLMapperHelper.ExecuteMapSet<IContribuinte, List<IContribuinte>>(_ctx.Connection,
                                                    SEL_ALL_CMD,
                                                    new IDbDataParameter[] { },
                                                    map
                                                    );
        }

        public IContribuinte Delete(IContribuinte entity)
        {
            SqlParameter p = new SqlParameter("@Nif", entity.Nif);

            int i = SQLMapperHelper.ExecuteNonQuery(_ctx.Connection, DEL_CMD, new[] { p });
            return i != 1 ? null : new ContribuinteProxy(entity, _ctx);
        }

    }

}
