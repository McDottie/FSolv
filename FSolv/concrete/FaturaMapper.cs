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

    class FaturaMapper : AbstracMapper<Fatura, int?, List<Fatura>>, IFaturaMapper
    {
        #region HELPER METHODS  
        internal List<Item> LoadItems(Fatura c)
        {
            List<Item> lst = new List<Item>();

            ItemMapper sm = new ItemMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.Id));
            using (IDataReader rd = ExecuteReader("select studentId from studentcourse where courseid=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(sm.Read(key));
                }
            }
            return lst;
        }

        internal Contribuinte LoadContribuinte(Fatura c)
        {
            throw new NotImplementedException();
        }
            #endregion
            public FaturaMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Course where courseId=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Course (Name) VALUES(@Name); select @id = scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select courseId,name from Course";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0}  where courseId=@id", SelectAllCommandText);
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Course set name=@name where courseId=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Fatura e)
        {
            SelectParameters(cmd, e.Id);
        }

        protected override void InsertParameters(IDbCommand cmd, Fatura e)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@name", e.Name);

            p1.Direction = ParameterDirection.InputOutput;
            if (e.Id != null)
                p1.Value = e.Id;
            else
                p1.Value = DBNull.Value;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

        }

        protected override Fatura Map(IDataRecord record)
        {
            Fatura c = new Fatura();
            c.Id = record.GetInt32(0);
            c.Name = record.GetString(1);
            return new FaturaProxy(c,context);
        }

        public override Fatura Create(Fatura entity)
        {
            return new FaturaProxy(base.Create(entity),context);
        }

        public override Fatura Update(Fatura entity)
        {
            return new FaturaProxy(base.Update(entity), context);
        }
        
        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Fatura UpdateEntityID(IDbCommand cmd, Fatura e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.Id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Fatura e)
        {
            InsertParameters(command, e);
        }
    }
}