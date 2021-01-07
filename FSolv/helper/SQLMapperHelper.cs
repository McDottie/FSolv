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
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace FSolv.helper
{
    //Delegate to MAP an entiry from a row
    public delegate T Mapper<T>(IDataRecord data);

    public static class SQLMapperHelper
    {
        public static int ExecuteNonQuery(SqlConnection con, string cmdtxt, IDbDataParameter[] dbDataParameters)
        {

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = cmdtxt;
                    cmd.Parameters.AddRange(dbDataParameters);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
        }

        public static T ExecuteScalar<T>(SqlConnection con, string cmdtxt, IDbDataParameter[] dbDataParameters)
        {
 
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = cmdtxt;
                    cmd.Parameters.AddRange(dbDataParameters);
                    con.Open();
                    return (T)cmd.ExecuteScalar();
                }
            
        }

        public static T ExecuteMapSingle<T>(SqlConnection con, string cmdtxt, IDbDataParameter[] dbDataParameters, Mapper<T> map )
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = cmdtxt;
                cmd.Parameters.AddRange(dbDataParameters);
                con.Open();
                return map(cmd.ExecuteReader());
            }
        }

        public static TCol ExecuteMapSet<T, TCol>(SqlConnection con, string cmdtxt, IDbDataParameter[] dbDataParameters, Mapper<T> map)
            where TCol : class, IList, new()
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = cmdtxt;
                cmd.Parameters.AddRange(dbDataParameters);
                con.Open();

                TCol list = new TCol(); 
                SqlDataReader sdr = cmd.ExecuteReader();
                
                while (sdr.Read())
                {
                    list.Add(map.Invoke(sdr));
                }
                return list;
            }
        }

    }
}