﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScraperDb.Models;
using System.Threading;
using ScraperDb.DataRetrieval;


namespace ScraperDb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var ticker = GetTicker.RetrieveTickerData();
            var news = News.GetNews();
            var model = new TickerAndNewsModels {NewsArticles = news, TickerInfo = ticker};

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
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
