using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrickHouse.Utility;
using Managed.Financials.DataContracts;

namespace Managed.Financials.Agents
{
    class FileProcessor : IFileProcessor
    {
        private ILogger Logger { get; set; }

        public FileProcessor(ILogger logger)
        {
            this.Logger = logger;
        }

        public List<ShortEntry> GetDocData(String filePath)
        {
            List<ShortEntry> shortEntries = new List<ShortEntry>();
            String lineData = null;
            Boolean firstLine = true;
            System.IO.StreamReader sr = null;
            try
            {
                sr = new System.IO.StreamReader(filePath);
                while (!(sr.Peek() == -1))
                {
                    lineData = sr.ReadLine();
                    if (!firstLine)
                    {
                        ShortEntry se = ParseLine(lineData);
                        if (se != null)
                        {
                            shortEntries.Add(se);
                        }
                    }
                    else
                    {
                        firstLine = false;
                    }

                }
                sr.Close();
               // System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                this.Logger.LogException(ex);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Dispose();
                }
            }

            return shortEntries;
        }

        private ShortEntry ParseLine(String line)
        {
            ShortEntry se = new ShortEntry();
            String[] columns = line.Split('|');
            if (columns.Count() == 1)
            {
                return null;
            }
            Int32 year = Convert.ToInt32(columns[0].Substring(0, 4));
            Int32 month = Convert.ToInt32(columns[0].Substring(4, 2));
            Int32 day = Convert.ToInt32(columns[0].Substring(6, 2));

            se.ShortDate = new DateTime(year, month, day);
            se.Symbol = columns[1];
            se.ShortVolume = Convert.ToInt64(columns[2]);
            se.ShortExemptVolume = Convert.ToInt64(columns[3]);
            se.TotalVolume = Convert.ToInt64(columns[4]);
            se.PercentShort = (Double)((Decimal)se.ShortVolume / (Decimal)se.TotalVolume);
            se.Market = columns[5];
            return se;
        }




        public void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
