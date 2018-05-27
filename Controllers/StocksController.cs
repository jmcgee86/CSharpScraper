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

namespace ScraperDb.Controllers
{
    public class StocksController : Controller
    {
        private readonly FinanceDbContext _context;
        //private readonly PortfolioInfo _snapshot;

        public StocksController(FinanceDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Portfolio.ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioInfo = await _context.Portfolio
                .SingleOrDefaultAsync(m => m.ID == id);
            //var StockInfoModel = new StockInfo();
            //var stocks = await _context.Stocks.SingleOrDefaultAsync(m=> m.ID == id);
            var stocks = _context.Stocks.Where(m=>m.PortfolioInfo.ID==id); 
            portfolioInfo.StockInfo = await stocks.ToListAsync();


            if (portfolioInfo == null)
            {
                return NotFound();
            }

            return View(portfolioInfo);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioInfo info)
        {
            var snapshot = GetData.Retrieve();
            //snapshot = info;
            if (ModelState.IsValid)
            {
                _context.Add(snapshot);
                await _context.SaveChangesAsync();
            }
            return View(info);
        }
        // public async Task<IActionResult> Create([Bind("ID,DatePulled,NetWorth,DayGain,DayGainPercentage,TotalGain,TotalGainPercentage")] PortfolioInfo portfolioInfo)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(portfolioInfo);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(portfolioInfo);
        // }

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
        //var stocks = _context.
            return View(portfolioInfo);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DatePulled,NetWorth,DayGain,DayGainPercentage,TotalGain,TotalGainPercentage")] PortfolioInfo portfolioInfo)
        {
            if (id != portfolioInfo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioInfoExists(portfolioInfo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioInfo);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioInfo = await _context.Portfolio
                .SingleOrDefaultAsync(m => m.ID == id);
            if (portfolioInfo == null)
            {
                return NotFound();
            }

            return View(portfolioInfo);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioInfo = await _context.Portfolio.SingleOrDefaultAsync(m => m.ID == id);
            _context.Portfolio.Remove(portfolioInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioInfoExists(int id)
        {
            return _context.Portfolio.Any(e => e.ID == id);
        }
    }
}
