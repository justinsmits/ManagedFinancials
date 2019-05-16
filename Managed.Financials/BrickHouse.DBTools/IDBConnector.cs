using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
namespace BrickHouse.DBTools
{
    public interface IDBConnector
    {
        void Insert<T>(List<T> objects, String objectName);

        IQueryResult<TResult> Query<TResult, ConditionType>(String objectName, Conditions.QueryCondition<ConditionType> conditions) where TResult : new();
        void ExecuteNonQuery(String sql, IEnumerable<SqlParameter> parameters);

        System.Data.DataTable ExecuteAsDataTable(String sql, IEnumerable<SqlParameter> parameters);
    }
}
