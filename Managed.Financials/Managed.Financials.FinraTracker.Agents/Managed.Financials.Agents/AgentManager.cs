using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.Utility;

namespace Managed.Financials.Agents
{
    class AgentManager
    {
        public void StartAgents()
        {
            Int32 i = 0;
            TaskFactory tf = new TaskFactory();
          
            for (i = 0; i < 1; i++)
            {
                AgentStartContext startContext = new AgentStartContext();
                startContext.AgentID = DateTime.Now.ToFileTime().ToString();
                startContext.Logger = LoggerFactory.GetLogger(Config.ApplicationName);
                ProcessingAgent downAgent = new ProcessingAgent(startContext);
                tf.StartNew(downAgent.StartAgent);
            }
          
            Console.WriteLine("Agents started up");
        }
    }
}
