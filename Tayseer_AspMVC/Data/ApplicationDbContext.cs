using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Disability> Disabilities { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
       
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Centers> Centers { get; set; }

        public virtual DbSet<DisabilityHospital> DisabilityHospitals { get; set; }


    }
}
