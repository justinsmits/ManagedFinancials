using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    public class LoggerFactory
    {
        public static ILogger GetLogger(String applicationName)
        {
            ILogger retVal = null;
            switch (Config.LoggerType)
            {
                case LoggingType.Console:
                    retVal = new ConsoleLogger(applicationName);
                    break;
                case LoggingType.EventLog:
                    retVal = new EventLogger(applicationName);
                    break;
                default:
                    retVal = new ConsoleLogger(applicationName);
                    break;
            }
            return retVal;
        }
    }
}
