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

namespace FSolv.mapper.concrete
{
    class ContribuinteMapper : AbstracMapper<Contribuinte, int?, List<Contribuinte>>, IContribuinteMapper
    {
        #region HELPER METHOD 
        internal List<Fatura> LoadFaturas(ContribuinteProxy contribuinteProxy)
        {
            throw new NotImplementedException();
        }
        #endregion

        public IContext ctx;
        protected override string DeleteCommandText
        {
            get
            {
                return "delete from TP1.Contribuinte where nif=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                 return "INSERT INTO TP1.Contribuinte (name,nif,morada) VALUES(@Name,@Nif,@Morada);";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select name,nif,morada from TP1.Contribuinte";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where nif=@Nif", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update TP1.Contribuinte set morada=@Morada where nif=@Nif";
            }
        }

        public ContribuinteMapper(IContext ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        protected override void DeleteParameters(IDbCommand cmd, Contribuinte e)
        {

            SqlParameter p1 = new SqlParameter("@Nif", e.Nif);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Contribuinte e)
        {
            SqlParameter p = new SqlParameter("@Name", e.Name);
            SqlParameter p1= new SqlParameter("@Morada", e.Morada);
            SqlParameter p2 = new SqlParameter("@Nif", e.Morada);

            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@Nif", k);
            cmd.Parameters.Add(p1);
        }

        protected override Contribuinte UpdateEntityID(IDbCommand cmd, Contribuinte c)
        {
            var param = cmd.Parameters["@Nif"] as SqlParameter;
            c.Nif = int.Parse(param.Value.ToString());
            return new ContribuinteProxy(c,ctx);
        }

        protected override void UpdateParameters(IDbCommand cmd, Contribuinte e)
        {
            InsertParameters(cmd, e);
        }

        protected override Contribuinte Map(IDataRecord record)
        {
            Contribuinte c = new Contribuinte();
            c.Name = record.GetString(0);
            c.Nif = record.GetInt32(1);
            c.Morada = record.GetString(2);

            return new ContribuinteProxy(c,ctx);
        }


        public override Contribuinte Create(Contribuinte entity)
        {
            return new ContribuinteProxy(base.Create(entity), context);
        }

        public override Contribuinte Update(Contribuinte entity)
        {
            return new ContribuinteProxy(base.Update(entity), context);
        }
    }
}
