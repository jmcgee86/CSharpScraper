using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScraperDb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text; 
using System.Drawing;
using ScraperDb.Controllers;
using System.Globalization;
using ScraperDb.Models.Auth;
using ScraperDb.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace ScraperDb.Controllers
{
    [Authorize]
    public class StocksController : Controller
    {
        private readonly FinanceDbContext _context;
        //private readonly PortfolioInfo _snapshot;

        public StocksController(FinanceDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Portfolio.ToListAsync());
        // }

        public async Task<IActionResult> Index(string sortOrder)
        {
                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["NetworthParm"] = sortOrder == "networth_desc" ? "Networth" : "networth_desc";
                ViewData["DaygainParm"] = sortOrder == "daygain_desc" ? "Daygain" : "daygain_desc";
                ViewData["DaygainPercentageParm"] = sortOrder == "daygainpercentage_desc" ? "DaygainPercentage" : "daygainpercentage_desc";
                ViewData["TotalgainParm"] = sortOrder == "totalgain_desc" ? "Totalgain" : "totalgain_desc";
                ViewData["TotalgainPercentageParm"] = sortOrder == "totalgainpercentage_desc" ? "TotalgainPercentage" : "totalgainpercentage_desc";

                var portfolioSnapshots = from s in _context.Portfolio
                                            select s;
                switch (sortOrder)
                {
                    case "Date":
                        portfolioSnapshots = portfolioSnapshots.OrderBy(s => s.DatePulled);
                        break;
                    case "date_desc":
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.DatePulled);
                        break;
                    case "Networth":
                        portfolioSnapshots = portfolioSnapshots.OrderBy(s => s.NetWorth);
                        break;
                    case "networth_desc":
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.NetWorth);
                        break;
                    case "Daygain":
                        portfolioSnapshots = portfolioSnapshots.OrderBy(s => s.DayGain);
                        break;
                    case "daygain_desc":
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.DayGain);
                        break;
                    case "DaygainPercentage":
                        portfolioSnapshots = portfolioSnapshots.OrderBy(s => s.DayGainPercentage);
                        break;
                    case "daygainpercentage_desc":
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.DayGainPercentage);
                        break;
                    case "TotalGain":
                        portfolioSnapshots = portfolioSnapshots.OrderBy(s => s.TotalGain);
                        break;
                    case "totalgain_desc":
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.TotalGain);
                        break;
                     case "TotalGainPercentage":
                        portfolioSnapshots = portfolioSnapshots.OrderBy(s => s.TotalGainPercentage);
                        break;
                    case "totalgainpercentage_desc":
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.TotalGainPercentage);
                        break;
                    default:
                        portfolioSnapshots = portfolioSnapshots.OrderByDescending(s => s.DatePulled);
                        break;
                }
                return View(await portfolioSnapshots.AsNoTracking().ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id, string sortOrder)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            ViewData["SymbolParm"] = sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["CurrentPriceParm"] = sortOrder == "currentprice_desc" ? "CurrentPrice" : "currentprice_desc";
            ViewData["PriceChangeParm"] = sortOrder == "pricechange_desc" ? "PriceChange" : "pricechange_desc";
            ViewData["PriceChangePercentageParm"] = sortOrder == "pricechangepercentage_desc" ? "PriceChangePercentage" : "pricechangepercentage_desc";
            ViewData["SharesParm"] = sortOrder == "shares_desc" ? "Shares" : "shares_desc";
            ViewData["CostBasisParm"] = sortOrder == "costbasis_desc" ? "CostBasis" : "costbasis_desc";
            ViewData["MarketValueParm"] = sortOrder == "marketvalue_desc" ? "MarketValue" : "marketvalue_desc";
            ViewData["DayGainParm"] = sortOrder == "daygain_desc" ? "DayGain" : "daygain_desc";
            ViewData["DayGainPercentageParm"] = sortOrder == "daygainpercentage_desc" ? "DayGainPercentage" : "daygainpercentage_desc";
            ViewData["TotalGainParm"] = sortOrder == "totalgain_desc" ? "TotalGain" : "totalgain_desc";
            ViewData["TotalPercentageParm"] = sortOrder == "totalgainpercentage_desc" ? "TotalGainPercentage" : "totalgainpercentage_desc";
            ViewData["LotsParm"] = sortOrder == "lots_desc" ? "Lots" : "lots_desc";
            ViewData["NotesParm"] = sortOrder == "notes_desc" ? "Notes" : "notes_desc";

            var portfolioInfo = await _context.Portfolio
                .SingleOrDefaultAsync(m => m.ID == id); 
            var stocks = from s in _context.Stocks.Where(m=>m.PortfolioInfo.ID==id)
                                            select s;
            if (portfolioInfo == null)
            {
                return NotFound();
            }

        switch (sortOrder)
    {
        case "Name":
            stocks = stocks.OrderBy(s => s.StockSymbol);
            break;
        case "CurrentPrice":
            stocks = stocks.OrderBy(s => s.CurrentPrice);
            break;
        case "currentprice_desc":
            stocks = stocks.OrderByDescending(s => s.CurrentPrice);
            break;
        case "PriceChange":
            stocks = stocks.OrderBy(s => s.PriceChange);
            break;
        case "pricechange_desc":
            stocks = stocks.OrderByDescending(s => s.PriceChange);
            break;
        case "PriceChangePercentage":
            stocks = stocks.OrderBy(s => s.PriceChangePercentage);
            break;
        case "pricechangepercentage_desc":
            stocks = stocks.OrderByDescending(s => s.PriceChangePercentage);
            break;
        case "Shares":
            stocks = stocks.OrderBy(s => s.Shares);
            break;
        case "shares_desc":
            stocks = stocks.OrderByDescending(s => s.Shares);
            break;
        case "CostBasis":
            stocks = stocks.OrderBy(s => s.CostBasis);
            break;
        case "costbasis_desc":
            stocks = stocks.OrderByDescending(s => s.CostBasis);
            break;
        case "MarketValue":
            stocks = stocks.OrderBy(s => s.MarketValue);
            break;
        case "marketvalue_desc":
            stocks = stocks.OrderByDescending(s => s.MarketValue);
            break;
        case "DayGain":
            stocks = stocks.OrderBy(s => s.DayGain);
            break;
        case "daygain_desc":
            stocks = stocks.OrderByDescending(s => s.DayGain);
            break;
        case "DayGainPercentage":
            stocks = stocks.OrderBy(s => s.DayGainPercentage);
            break;
        case "daygainpercentage_desc":
            stocks = stocks.OrderByDescending(s => s.DayGainPercentage);
            break;
       case "TotalGain":
            stocks = stocks.OrderBy(s => s.TotalGain);
            break;
        case "totalgain_desc":
            stocks = stocks.OrderByDescending(s => s.TotalGain);
            break;
        case "TotalGainPercentage":
            stocks = stocks.OrderBy(s => s.TotalGainPercentage);
            break;
        case "totalgainpercentage_desc":
            stocks = stocks.OrderByDescending(s => s.TotalGainPercentage);
            break;
       case "Lots":
            stocks = stocks.OrderBy(s => s.Lots);
            break;
        case "lots_desc":
            stocks = stocks.OrderByDescending(s => s.Lots);
            break;
       case "Notes":
            stocks = stocks.OrderBy(s => s.Notes);
            break;
        case "notes_desc":
            stocks = stocks.OrderByDescending(s => s.Notes);
            break;
        default:
            stocks = stocks.OrderByDescending(s => s.StockSymbol);
            break;
    }
    portfolioInfo.StockInfo = await stocks.AsNoTracking().ToListAsync();

            return View(portfolioInfo);
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioInfo info)
        {
            var snapshot = GetData.Retrieve();
            if (ModelState.IsValid)
            {
                _context.Add(snapshot);
                await _context.SaveChangesAsync();
            }
            return Redirect("/stocks/Details/" + snapshot.ID);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioInfo = await _context.Portfolio.SingleOrDefaultAsync(m => m.ID == id);
            if (portfolioInfo == null)
            {
                return NotFound();
            }
            return View(portfolioInfo);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("ID,DatePulled,NetWorth,DayGain,DayGainPercentage,TotalGain,TotalGainPercentage")] PortfolioInfo portfolioInfo)
        // {
        //     if (id != portfolioInfo.ID)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(portfolioInfo);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!PortfolioInfoExists(portfolioInfo.ID))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(portfolioInfo);
        // }

        // // GET: Stocks/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var portfolioInfo = await _context.Portfolio
        //         .SingleOrDefaultAsync(m => m.ID == id);
        //     if (portfolioInfo == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(portfolioInfo);
        // }

        // // POST: Stocks/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var portfolioInfo = await _context.Portfolio.SingleOrDefaultAsync(m => m.ID == id);
        //     _context.Portfolio.Remove(portfolioInfo);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        private bool PortfolioInfoExists(int id)
        {
            return _context.Portfolio.Any(e => e.ID == id);
        }
    }
}
