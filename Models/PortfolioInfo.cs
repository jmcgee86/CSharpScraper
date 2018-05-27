using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScraperDb.Models
{
    public class PortfolioInfo
    {
        
        public int ID { get; set; }
        
        [Display(Name = "Date Pulled")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:MM\/dd\/yyyy, HH:mm}")]
        public DateTime DatePulled { get; set; }
                
        [Display(Name = "Net Worth")]
        [DataType(DataType.Currency)]
        public Double NetWorth { get; set; }
    
        [Display(Name = "Day Gain")]
        [DataType(DataType.Currency)]
        public Double DayGain{ get; set; }
        
        [Display(Name = "Day Gain %")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public Double DayGainPercentage{ get; set; }
        
        [Display(Name = "Total Gain")]
        [DataType(DataType.Currency)]
        public Double TotalGain{ get; set; }
        
        [Display(Name = "Total Gain %")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public Double TotalGainPercentage{ get; set; }
        public virtual List<StockInfo> StockInfo { get; set; }

        
    }
}