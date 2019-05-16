using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using Managed.Financials.DataContracts;
using BrickHouse.DBTools;


namespace Managed.Financials.API
{
    public class ShortEntryService : IShortEntryService
    {
        private const String SHORT_ENTRY_TABLE = "ShortEntry";

        public async Task<IAPIResult> InsertAsync(List<ShortEntry> shortEntries)
        {
            IAPIResult retVal = null;
            IDBConnector dbConnector = null;
            try
            {
                dbConnector = DBConnectorFactory.GetConnector();
                String insertSQL = @"
                                  INSERT INTO [ShortEntry] (ShortDate, Symbol, ShortVolume, ShortExemptVolume, TotalVolume, PercentShort, Market)
                                  VALUES (@shortDate, @symbol, @shortVolume, @shortExemptVolume, @totalVolume, @percentShort, @market)
                ";
                foreach (ShortEntry se in shortEntries)
                {
                    SqlParameter shortDateParam = new SqlParameter("@shortDate", SqlDbType.DateTime) { Value = se.ShortDate };
                    SqlParameter symbolParam = new SqlParameter("@symbol", SqlDbType.NChar, 10) { Value = se.Symbol };
                    SqlParameter shortVolume = new SqlParameter("@shortVolume", SqlDbType.BigInt) { Value = se.ShortVolume };
                    SqlParameter shortExemptVolume = new SqlParameter("@shortExemptVolume", SqlDbType.BigInt) { Value = se.ShortExemptVolume };
                    SqlParameter totalVolume = new SqlParameter("@totalVolume", SqlDbType.BigInt) { Value = se.TotalVolume };
                    SqlParameter percentShort = new SqlParameter("@percentShort", SqlDbType.Float) { Value = se.PercentShort };
                    SqlParameter market = new SqlParameter("@market", SqlDbType.NChar, 10) { Value = se.Market };
                    dbConnector.ExecuteNonQuery(insertSQL, new SqlParameter[] { shortDateParam, symbolParam, shortVolume, shortExemptVolume, totalVolume, percentShort, market });
                }
                //dbConnector.Insert<ShortEntry>(shortEntries, SHORT_ENTRY_TABLE);
                
                retVal = new APIResult(true, String.Format("Inserted {0} instances of {1}", shortEntries.Count, SHORT_ENTRY_TABLE));
            }
            catch (Exception ex)
            {
                retVal = new APIResult(false, String.Format("Exception during {0} insert. {1}", SHORT_ENTRY_TABLE, ex.ToString()));
            }
            return retVal;
        }


    }
}
