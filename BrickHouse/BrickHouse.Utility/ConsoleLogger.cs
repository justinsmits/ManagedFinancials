using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    public class ConsoleLogger : LoggerBase
    {
        public ConsoleLogger(String applicationName)
            : base(applicationName)
        {

        }

        protected override void LogMessageOverride(string message)
        {
            Console.WriteLine("{0}: {1}", base.ApplicationName, message);
        }

        protected override void LogExceptionOverride(Exception ex)
        {
            Console.WriteLine("EXCEPTION: {0} stack: {1}", base.ApplicationName, ex.ToString());
        }
    }
}
