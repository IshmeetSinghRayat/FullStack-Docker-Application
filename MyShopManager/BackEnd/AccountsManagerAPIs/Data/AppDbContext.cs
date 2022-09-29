using AccountsManagerAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManagerAPIs.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }
        public DbSet<Accounts> Accounts => Set<Accounts>();
    }
}
