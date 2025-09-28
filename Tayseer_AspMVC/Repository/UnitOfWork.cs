
using Tayseer_AspMVC.Data;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Disabilitys = new MainRepository<Disability>(_context);
            Hospitals = new MainRepository<Hospital>(_context);
          
            Schools = new RepoSchool(_context);
            Employees = new RepoEmployee(_context);
            RepoCenters = new RepoCenter(_context);
            UserRoles = new MainRepository<UserRole>(_context);
            roposHospital = new RoposHospital(_context);
            DisabilityHospitals = new MainRepository<DisabilityHospital>(_context);
            DisabilitySchools = new MainRepository<DisabilitySchool>(_context);
            DisabilityCenters = new MainRepository<DisabilityCenter>(_context);
            Centers = new MainRepository<Centers>(_context);



        }
        public IRepository<Disability> Disabilitys { get; set; }
        public IRepository<Hospital> Hospitals { get; set; }

        public  IRoposHospital roposHospital { get; set; }
          public IRepoCenter RepoCenters { get;  }
        public IRepository<Centers> Centers { get; }
        public IRepoSchool Schools { get; }


        public IRepoEmployee Employees { get; }
       
        public IRepository<UserRole> UserRoles { get; set; }
        public IRepository<DisabilityHospital> DisabilityHospitals { get; set; }
        public IRepository<DisabilitySchool> DisabilitySchools { get; set; }
        public IRepository<DisabilityCenter> DisabilityCenters { get; set; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
