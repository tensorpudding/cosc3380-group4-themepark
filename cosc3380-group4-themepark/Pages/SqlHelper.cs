using System;
using System.Data;
using Microsoft.Data.SqlClient;

/* SqlHelper.cs
 *
 * This class provides helper methods for handling the creation of SqlClient connections and returning rows
 * This is a thin wrapper on functionality provided in the documentation of SqlClient
 */

namespace cosc3380_group4_themepark
{
    public class SqlHelper
    {
        /* ExecuteProcNonQuery
         *
         * Execute a stored procedure of the given name, and return the number of rows changed, rather than row output
         * This is intended to be used for stored procedures that "return void", such as INSERT
         */
        public static Int32 ExecuteProcNonQuery(String procname, params SqlParameter[] parameters)
        {
            try
            {
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

        /* ExecuteProcReader
         *
         * This method executes the provided stored procedure with the given arguments, and
         * returns a SqlDataReader object which contains the rows returned.
         * This is to be used stored procedures that generate rows.
         */
        public static SqlDataReader ExecuteProcReader(String procname, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(Credentials.getConnectionString());

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }

        /* ExecuteQueryReader
         *
         * This simple method execute raw SQL command strings with the given parameters,
         * returning an SqlDataReader object containing the rows returned by the given SELECT statement.
         */
        public static SqlDataReader ExecuteQueryReader(String queryString, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(Credentials.getConnectionString());

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = queryString;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }
    }

}