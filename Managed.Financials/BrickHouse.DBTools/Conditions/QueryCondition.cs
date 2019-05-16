using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.DBTools.Conditions
{
    public class QueryCondition<ConditionType>
    {
        public String FieldName { get; set; }
        public ConditionOperator Operator {get;set;}
        public ConditionType Value {get;set;}
        
    }
}
