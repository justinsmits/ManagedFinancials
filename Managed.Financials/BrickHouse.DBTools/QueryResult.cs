using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickHouse.DBTools
{
    public class QueryResult<TResult> : IQueryResult<TResult>
    {
        public QueryResult()
        {
            this.Results = new List<TResult>();

        }
        public IList<TResult> Results
        { get; set; }

        public string Message
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }

    }
}
