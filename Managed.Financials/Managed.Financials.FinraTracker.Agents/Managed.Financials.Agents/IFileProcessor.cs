using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.Agents
{
    public interface IFileProcessor
    {
       List<DataContracts.ShortEntry> GetDocData(String filePath);
       void DeleteFile(String filePath);
    }
}
