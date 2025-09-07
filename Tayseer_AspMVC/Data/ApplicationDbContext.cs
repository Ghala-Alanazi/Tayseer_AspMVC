using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }

    }
}
