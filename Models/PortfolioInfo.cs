using System;
using System.Collections.Generic;

namespace ScraperDb.Models
{
    public class PortfolioInfo
    {
        
        public int ID { get; set; }
        public DateTime DatePulled { get; set; }
        public Double NetWorth { get; set; }
        public Double DayGain{ get; set; }
        public Double DayGainPercentage{ get; set; }
        public Double TotalGain{ get; set; }
        public Double TotalGainPercentage{ get; set; }
        public virtual List<StockInfo> StockInfo { get; set; }

        
    }
}