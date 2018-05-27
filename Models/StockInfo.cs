using System.ComponentModel.DataAnnotations;
using System;

namespace ScraperDb.Models
{
    public class StockInfo
    {
        [Display(Name = "Stock Symbol")]
        public string StockSymbol { get; set; }

        [Display(Name = "Current Price")]
        [DataType(DataType.Currency)]
        public double CurrentPrice { get; set; }

        [Display(Name = "Price Change")]
        [DataType(DataType.Currency)]
        public double PriceChange { get; set; }

        [Display(Name = "Price Change %")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double PriceChangePercentage { get; set; }

        public double Shares { get; set; }

        [Display(Name = "Cost Basis")]
        [DataType(DataType.Currency)]
        public double CostBasis { get; set; }

        [Display(Name = "Market Value")]
        [DataType(DataType.Currency)]
        public double MarketValue { get; set; }

        [Display(Name = "Day Gain")]
        [DataType(DataType.Currency)]
        public double DayGain { get; set; }

        [Display(Name = "Day Gain %")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double DayGainPercentage { get; set; }

        [Display(Name = "Total Gain")]
        [DataType(DataType.Currency)]
        public double TotalGain { get; set; }

        [Display(Name = "Total Gain %")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double TotalGainPercentage { get; set; }
        public int Lots { get; set; }
        public string Notes { get; set; }

        public int ID { get; set; }
        public virtual PortfolioInfo PortfolioInfo { get; set;}

    }
}