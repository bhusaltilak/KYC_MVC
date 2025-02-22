using Microsoft.EntityFrameworkCore;
using KYC_MVC.Models;

namespace KYC_MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<KYC> KYCs { get; set; }

    }
}