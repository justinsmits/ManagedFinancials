using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.API
{
    public interface IShortEntryService
    {
        Task<IAPIResult> InsertAsync(List<DataContracts.ShortEntry> shortEntries);
    }
}
