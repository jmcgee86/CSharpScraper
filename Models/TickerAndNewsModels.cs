using System;
using System.Collections.Generic;

namespace ScraperDb.Models
{
    public class TickerAndNewsModels
    {
        public virtual List<NewsInfo> NewsArticles { get; set; }
        public virtual TickerInfo TickerInfo {get; set;}

    }
}