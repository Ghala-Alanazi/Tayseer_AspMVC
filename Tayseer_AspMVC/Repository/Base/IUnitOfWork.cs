using System.Security;
using Tayseer_AspMVC.Controllers;
using Tayseer_AspMVC.Migrations;
using Tayseer_AspMVC.Models;

namespace Tayseer_AspMVC.Repository.Base
{
    public interface IUnitOfWork
    {

        IRepository<Disability> Disabilitys { get; }
        IRepository<Hospital> Hospitals { get; }

        IReposHospital roposHospital { get; }

        IRepository<Centers> Centers { get; }

        IRepoCenter RepoCenters { get; }

        IRepoSchool Schools { get; }
        IRepoEmployee Employees { get; }
        IRepository<UserRole> UserRoles { get; }
        IRepository<Customer> Customers { get; }

        IRepository<DisabilityHospital> DisabilityHospitals { get; }
        IRepository<DisabilitySchool> DisabilitySchools { get; set; }

        IRepository<DisabilityCenter> DisabilityCenters { get; set; }

        void Save();
    }
}
