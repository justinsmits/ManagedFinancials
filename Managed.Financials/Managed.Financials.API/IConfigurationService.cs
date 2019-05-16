using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Managed.Financials.DataContracts;

namespace Managed.Financials.API
{
    public interface IConfigurationService
    {
        Task<Configuration> ReadAsync(String name);
    }
}
