using Microsoft.EntityFrameworkCore;
using System.Security;
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

        }
        public IRepository <Disability> Disabilitys { get; set; }
        public IRepository <Hospital> Hospitals { get; set; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
