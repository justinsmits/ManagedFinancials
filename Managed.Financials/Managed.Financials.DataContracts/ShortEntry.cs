using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed.Financials.DataContracts
{
    public class ShortEntry
    {
        public Int64 ID { get; set; }
        public DateTime ShortDate { get; set; }
        public String Symbol { get; set; }
        public Int64 ShortVolume { get; set; }
        public Int64 ShortExemptVolume { get; set; }
        public Int64 TotalVolume { get; set; }
        public Double PercentShort { get; set; }

        public String Market { get; set; }

        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", ID, ShortDate.ToShortDateString(), Symbol, ShortVolume, TotalVolume, PercentShort, Market);
        }
    }
}
