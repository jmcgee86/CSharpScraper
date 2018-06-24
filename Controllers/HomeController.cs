using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScraperDb.Models;
using System.Threading;

namespace ScraperDb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = GetTicker.RetrieveTickerData();
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            // TickerInfo tickerInfo = new TickerInfo();
            var ticker = GetTicker.RetrieveTickerData();
            return View(ticker);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
