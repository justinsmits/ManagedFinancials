using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    public class Config : ConfigBase
    {
        public static Boolean LoggingEnabled
        {
            get
            {
                String loggingEnabledStr = GetGonfigValue("LoggingEnabled");
                Boolean loggingEnabled = false;
                Boolean.TryParse(loggingEnabledStr, out loggingEnabled);
                return loggingEnabled;
            }
        }

        public static LoggingType LoggerType
        {
            get
            {
                return LoggingType.Console;
            }

        }
    }

    public enum LoggingType
    {
        Console = 0,
        EventLog = 1
    }
}
