using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.Agents
{
    public interface IFileDownloader
    {
       Task<String> DownloadFile(Uri filePath);
    }
}
