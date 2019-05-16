using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.DBTools;
using BrickHouse.Utility;

namespace Managed.Financials.Agents
{
    class CleanerAgent : AgentBase
    {
        public CleanerAgent(AgentStartContext startContext):base(startContext)
        {
        }
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override int Interval
        {
            get
            {
                return 24 * 60 * 60;
            }
            set
            {
                this.Interval = value;
            }
        }
    }
}
