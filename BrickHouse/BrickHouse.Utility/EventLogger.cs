using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    class EventLogger : LoggerBase
    {
        public EventLogger(String applicationName)
            : base(applicationName)
        {

        }

        protected override void LogMessageOverride(string message)
        {
            System.Diagnostics.EventLog.WriteEntry(base.ApplicationName, message);
        }

        protected override void LogExceptionOverride(Exception ex)
        {
            System.Diagnostics.EventLog.WriteEntry(base.ApplicationName, ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
        }
    }
}
