using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.DBTools;

namespace Managed.Financials.API
{
   public class AgentQueueService
    {
       private const String TABLE_NAME = "ProcessAgentQueue";

       public AgentQueueService()
       {

       }

       private async Task<DateTime> GetLastProcessedDate(IDBConnector dbConnector)
       {
           return await new Task<DateTime>(() => DateTime.Now);
       }

    }
}
