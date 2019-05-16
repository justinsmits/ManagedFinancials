using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using BrickHouse.Utility;

namespace Managed.Financials.Agents
{
    class FileDownloader : IFileDownloader
    {

        HttpClient _httpClient = null;
        public ILogger Logger { get; private set; }
        public FileDownloader(ILogger logger)
        {
            _httpClient = new HttpClient();
            // Increase the max buffer size for the response so we don't get an exception with so many web sites
            _httpClient.MaxResponseContentBufferSize = 44256000;
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
            this.Logger = logger;
        }

        public async Task<String> DownloadFile(Uri filePath)
        {
           //return "C:\\ShortTest.txt";

            string responseBodyAsText;
            String tempFilePath = System.IO.Path.GetTempFileName();
            this.Logger.LogMessage("Downloading file: " + filePath.ToString());
            HttpResponseMessage response = await _httpClient.GetAsync(filePath);
            response.EnsureSuccessStatusCode();
            responseBodyAsText = await response.Content.ReadAsStringAsync();
            this.Logger.LogMessage("Writing contents to disk. File Path: " + tempFilePath);
            WriteTextToTempFile(tempFilePath, responseBodyAsText);
            return tempFilePath;
        }

        private void WriteTextToTempFile(String filePath, String text)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                sw = new System.IO.StreamWriter(filePath);
                sw.Write(text);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }
        }
    }
}
