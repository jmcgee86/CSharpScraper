using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ScraperDb.Models.Auth;

namespace ScraperDb.Models
{
    public class FinanceDbContext : IdentityDbContext<ApplicationUser>
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
            {
            }
        public DbSet<StockInfo> Stocks { get; set;}
        public DbSet<PortfolioInfo> Portfolio { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}