using System;
using HtmlAgilityPack;
using ScraperDb.Models;

namespace ScraperDb
{
    public class GetTicker
    {
        public static TickerInfo RetrieveTickerData(){

            var currentTicker = new TickerInfo();
        
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load("https://finance.yahoo.com");
            HtmlNodeCollection tickerNodes = doc.DocumentNode.SelectNodes("//span[@class='Trsdu(0.3s) Fz(s) Mt(4px) Mb(0px) Fw(b) D(ib)']");
            currentTicker.DatePulled = DateTime.UtcNow;
            currentTicker.SPValue = tickerNodes[0].InnerHtml.ToString();
            currentTicker.DowValue = tickerNodes[1].InnerHtml.ToString();
            currentTicker.NasdaqValue = tickerNodes[2].InnerHtml.ToString();

            return currentTicker;
        }
    }
}