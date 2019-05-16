using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace BrickHouse.DBTools
{
    public class SQLDBConnector : IDBConnector
    {
        public SQLDBConnector() { }
        public void Insert<T>(List<T> objects, string objectName)
        {
            throw new NotImplementedException();
        }

        public IQueryResult<TResult> Query<TResult, ConditionType>(string objectName, Conditions.QueryCondition<ConditionType> conditions) where TResult : new()
        {
            throw new NotImplementedException();
        }

        public void ExecuteNonQuery(String sql, IEnumerable<SqlParameter> parameters)
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ShortTrackerConn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

        }

        public DataTable ExecuteAsDataTable(string sql, IEnumerable<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ShortTrackerConn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }
    }
}
