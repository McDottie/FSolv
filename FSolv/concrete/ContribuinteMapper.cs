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
        public ContribuinteMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Country where countryId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                 return "INSERT INTO Country (Name) VALUES(@Name); select @id=scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select countryId,name from Country";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where countryId=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Country set name=@name where countryId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Contribuinte e)
        {

            SqlParameter p1 = new SqlParameter("@id", e.Id);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Contribuinte e)
        {
            SqlParameter p = new SqlParameter("@Name", e.Name);
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            p1.Direction = ParameterDirection.InputOutput;

            if (e.Id != null)
                p1.Value = e.Id;
            else
                p1.Value = DBNull.Value;

            cmd.Parameters.Add(p);
            cmd.Parameters.Add(p1);
        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Contribuinte UpdateEntityID(IDbCommand cmd, Contribuinte e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Id = int.Parse(param.Value.ToString());
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Contribuinte e)
        {
            InsertParameters(cmd, e);
        }

        protected override Contribuinte Map(IDataRecord record)
        {
            Contribuinte c = new Contribuinte();
            c.Id = record.GetInt32(0);
            c.Name = record.GetString(1);
            return c;
        }
    }
}
