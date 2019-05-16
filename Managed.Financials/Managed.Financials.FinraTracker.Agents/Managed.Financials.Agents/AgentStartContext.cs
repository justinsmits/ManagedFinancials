using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.Utility;

namespace Managed.Financials.Agents
{
    public class AgentStartContext
    {
        public String AgentID { get; set; }
        public ILogger Logger { get; set; }
    }
}
