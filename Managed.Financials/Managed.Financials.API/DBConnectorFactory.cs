using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.DBTools;

namespace Managed.Financials.API
{
    class DBConnectorFactory
    {
        internal static IDBConnector GetConnector()
        {
            return new SQLDBConnector();
        }
    }
}
