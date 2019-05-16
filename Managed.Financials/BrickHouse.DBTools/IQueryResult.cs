using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.DBTools
{
    public interface IQueryResult<ResultType>
    {
        IList<ResultType> Results { get; set; }
        String Message { get; set; }
        Boolean Success { get; set; }
    }
}
