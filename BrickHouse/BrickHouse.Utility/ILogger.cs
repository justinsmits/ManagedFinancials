using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    public interface ILogger
    {
        void LogMessage(String message);
        void LogException(Exception ex);
    }
}
