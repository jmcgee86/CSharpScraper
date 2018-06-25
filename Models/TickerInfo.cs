using System.ComponentModel.DataAnnotations;
using System;

namespace ScraperDb.Models
{
    public class TickerInfo
    {
        [Display(Name = "Date Pulled")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:MM\/dd\/yyyy, HH:mm}")]
        public DateTime DatePulled { get; set; }
        public string SPValue { get; set; }
        public string DowValue { get; set; }
        public string NasdaqValue { get; set; }
    }
}