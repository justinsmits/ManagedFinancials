using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.Utility
{
    public abstract class ConfigBase
    {
        public static String GetGonfigValue(String key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
