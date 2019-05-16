using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.Agents
{

    class ProcessingAgent : AgentBase
    {

        private const String _baseUrl = "http://regsho.finra.org/";
        private static IEnumerable<DateTime> _holidates;
        private static Boolean _hasRun = false;
        public ProcessingAgent(AgentStartContext startContext)
            : base(startContext)
        {
            _interval = 5000;
            _holidates = GetHolidates();
        }

        Int32 _interval;
        public override void Execute()
        {
            if (_hasRun)
                return;
            //Process downloaded file
            IFileDownloader fd = new FileDownloader(base.Logger);
            IFileProcessor fp = new FileProcessor(base.Logger);
            API.IShortEntryService shortService = new API.ShortEntryService();
            //Get start date
            DateTime startDate = GetStartDate();
            DateTime dateToProcess = startDate;
            String formattedDate = null;
            Int32 recordCount;
            while (dateToProcess.Date < DateTime.Now.Date || (dateToProcess.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay >= new TimeSpan(16)))
            {
                recordCount = 0;
                //don't process the date if the market wasn't open
                if (!MarketOpenOnDate(dateToProcess))
                {
                    //advance the date and just move on
                    dateToProcess = dateToProcess.AddDays(1);
                    continue;
                }
                Logger.LogMessage("Pulling information for: " + dateToProcess.Date.ToShortDateString());
                formattedDate = String.Format("{0}{1}{2}", dateToProcess.Year, dateToProcess.Month.ToString("00"), dateToProcess.Day.ToString("00"));

                Logger.LogMessage("Pulling records for FNSQ");
                recordCount += DownloadAndProcess(formattedDate, "FNSQ", fd, fp, shortService, _baseUrl).Result;

                Logger.LogMessage("Pulling records for FNYX");
                recordCount += DownloadAndProcess(formattedDate, "FNYX", fd, fp, shortService, _baseUrl).Result;

                Logger.LogMessage("Pulling records for FORF");
                recordCount += DownloadAndProcess(formattedDate, "FORF", fd, fp, shortService, _baseUrl).Result;

                Logger.LogMessage(String.Format("All downloads complete for date: {0} Records Processed: {1}", dateToProcess.ToString(), recordCount));

                LogLastProcessedDate(dateToProcess, recordCount);

                //advance the date
                dateToProcess = dateToProcess.AddDays(1);
            }

            _hasRun = true;
        }

        private void LogLastProcessedDate(DateTime lastProcessedDate, Int32 recordCount)
        {

        }


        private async Task<Int32> DownloadAndProcess(String formattedDate, String marketPrefix, IFileDownloader fd, IFileProcessor fp, API.IShortEntryService shortService, String baseUrl)
        {
            Int32 retVal = 0;
            try
            {
                Uri downloadPath = GetDownloadPath(baseUrl, marketPrefix, formattedDate);
                String filePath = await fd.DownloadFile(downloadPath);
                List<DataContracts.ShortEntry> shorts = fp.GetDocData(filePath);
                await shortService.InsertAsync(shorts);
                fp.DeleteFile(filePath);
                retVal = shorts.Count();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ERROR processing file. Ex: " + ex.ToString());
            }
            return retVal;
        }

        private Uri GetDownloadPath(String baseUrl, String marketPrefix, String formattedDate)
        {
            return new Uri(String.Format("{0}{1}shvol{2}.txt", baseUrl, marketPrefix, formattedDate));
        }


        private DateTime GetStartDate()
        {
            return new DateTime(2015, 01, 01);
        }
        private Boolean MarketOpenOnDate(DateTime dateToCheck)
        {
            Boolean retVal = true;
            if (dateToCheck.DayOfWeek == DayOfWeek.Saturday || dateToCheck.DayOfWeek == DayOfWeek.Sunday || _holidates.Contains(dateToCheck))
            {
                retVal = false;
            }
            return retVal;
        }

        private static List<DateTime> GetHolidates()
        {
            List<DateTime> holiDates = new List<DateTime>();
            holiDates.Add(new DateTime(2016, 11, 24));
            holiDates.Add(new DateTime(2016, 12, 25));
            holiDates.Add(new DateTime(2017, 01, 02));
            return holiDates;
        }

        public override int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;

            }
        }

    }
}
