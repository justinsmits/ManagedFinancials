using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.Agents
{
    class Config
    {
       public static String ApplicationName
        {
            get
            {
                String appName = GetGonfigValue("ApplicationName");
                return appName;
            }
        }

        private static String GetGonfigValue(String key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

    }
}
