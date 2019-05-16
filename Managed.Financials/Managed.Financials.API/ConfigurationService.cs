using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

using BrickHouse.DBTools;
using Managed.Financials.DataContracts;

namespace Managed.Financials.API
{
    class ConfigurationService : IConfigurationService
    {
        public async Task<Configuration> ReadAsync(string name)
        {
            IDBConnector dbConnector = null;
            Configuration retval = null;
            try
            {
                dbConnector = DBConnectorFactory.GetConnector();
                String readSQL = @"
                                  SELECT [Name], [Value]
                                  FROM  [Configuration]
                                    WHERE [Name] = @name
                ";

                SqlParameter nameParam = new SqlParameter("@name", System.Data.SqlDbType.NVarChar) { Value = name };

                System.Data.DataTable dt = dbConnector.ExecuteAsDataTable(readSQL, new SqlParameter[]{ nameParam });

                retval = TransformFromDT(dt);
            }
            catch (Exception ex)
            {
                //retVal = new APIResult(false, String.Format("Exception during {0} insert. {1}", SHORT_ENTRY_TABLE, ex.ToString()));
            }
            return retval;
        }

        private Configuration TransformFromDT(System.Data.DataTable dt)
        {
            if (dt == null)
                throw new ArgumentNullException("dt");

            if (dt.Rows.Count != 1)
                throw new ArgumentOutOfRangeException("Incorrect number of records");

            System.Data.DataRow dr = dt.Rows[0];
            Configuration retVal = new Configuration();
            retVal.Name = dr["Name"].ToString();
            retVal.Value = dr["Value"].ToString();

            return retVal;
        }

    }
}
