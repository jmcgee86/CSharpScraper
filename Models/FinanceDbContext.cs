using Microsoft.EntityFrameworkCore;

namespace ScraperDb.Models
{
    public class FinanceDbContext : DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
            {
            }
        public DbSet<StockInfo> Stocks { get; set;}
        public DbSet<PortfolioInfo> Portfolio { get; set;}
    }
}