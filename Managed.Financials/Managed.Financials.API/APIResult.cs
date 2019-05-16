using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.API
{
    public class APIResult : IAPIResult
    {
        private String _message;
        private Boolean _success;

        public APIResult(Boolean success, String message)
        {
            _message = (!String.IsNullOrWhiteSpace(message)) ? message : String.Empty;
            _success = success;
        }
        public bool Success
        {
            get { return _success; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
