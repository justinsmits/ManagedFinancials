using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.DBTools;

namespace Managed.Financials.Agents
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Starting App");
            AgentManager agentMgr = new AgentManager();
            agentMgr.StartAgents();
            Console.WriteLine("Agents Started");

            //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            //client.BaseAddress = new Uri("http://regsho.finra.org/");
            //Console.WriteLine(client.GetAsync("FNYXshvol20140919.txt").Result.Content.ReadAsStringAsync().Result.ToString());

            Console.ReadKey();
        }

    }
}
