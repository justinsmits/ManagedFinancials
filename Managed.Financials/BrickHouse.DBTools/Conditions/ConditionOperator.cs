using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.DBTools.Conditions
{
    public enum ConditionOperator
    {
        LessThan = 0,
        LessThanEqualTo = 1,
        GreaterThan = 2,
        GreaterThanEqualTo = 3,
        EqualTo = 4,
        In = 5
    }
}
