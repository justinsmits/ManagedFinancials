using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.Utility;

namespace Managed.Financials.Agents
{
    public abstract class AgentBase
    {
        public AgentBase(AgentStartContext agentStartContext)
        {

            Cancelled = false;
            Interval = 10000;
            this.AgentID = agentStartContext.AgentID;
            this.Logger = agentStartContext.Logger;
        }

        public ILogger Logger { get; private set; }
        public String AgentID { get; private set; }

        public void StartAgent()
        {
            while (!Cancelled)
            {
                Execute();
                System.Threading.Thread.Sleep(Interval);
            }
        }
        public abstract void Execute();

        public virtual Int32 Interval { get; set; }

        public Boolean Cancelled
        {
            get;
            set;
        }
    }
}
