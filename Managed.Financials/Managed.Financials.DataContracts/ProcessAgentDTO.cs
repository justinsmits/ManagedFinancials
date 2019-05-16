using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.Agents.DataContract
{
    public class ProcessAgentDTO
    {
        public String AgentID { get; set; }
        public DateTime LastProcessedDate { get; set; }
        public Int64 RecordsProcessed { get; set; }
    }
}
