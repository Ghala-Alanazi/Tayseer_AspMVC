using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Data;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Repository
{
    public class RepoCenter : MainRepository<Disability>, IRepoCenter
    {
        private readonly ApplicationDbContext _context;

        public RepoCenter(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<DisabilityCenter> DisabilityCenter()
        {

            var disability = _context.DisabilityCenters
                .Include(x => x.Centers)
                .Include(x => x.Disability)
                .ToList();
            return disability;


        }

        public Centers FindByUIdCenter(string uid)
        {
            var center = _context.Centers.FirstOrDefault(h => h.uid == uid);
            return center;
        }

    }
}
