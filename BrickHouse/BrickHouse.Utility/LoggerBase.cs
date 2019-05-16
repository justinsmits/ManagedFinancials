using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    public abstract class LoggerBase : ILogger
    {
        public String ApplicationName { get; private set; }
        public LoggerBase(String applicationName)
        {
            this.ApplicationName = applicationName;
        }

        public void LogMessage(string message)
        {
            if (Config.LoggingEnabled)
            {
                LogMessageOverride(message);
            }
        }

        public void LogException(Exception ex)
        {
            LogExceptionOverride(ex);
        }

        protected abstract void LogMessageOverride(String message);
        protected abstract void LogExceptionOverride(Exception ex);
    }
}
