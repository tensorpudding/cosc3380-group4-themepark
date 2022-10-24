using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark
{
    public class SqlHelper
    {
        public static Int32 ExecuteProc(String procname, SqlParameter[] parameters)
        {
            try {
                Int32 rows_affected = 0;
                using (SqlConnection conn = new SqlConnection(Credentials.getConnectionString()))
                {
                    conn.Open();
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = conn;
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.CommandText = procname;
                        comm.Parameters.AddRange(parameters);
                        rows_affected = comm.ExecuteNonQuery();
                    }
                }
                return rows_affected;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
    }

}