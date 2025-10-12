using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Data;
using Tayseer_AspMVC.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Tayseer_AspMVC.Repository
{
    public class ReposHospital : MainRepository<Hospital>, IReposHospital
    {
        private readonly ApplicationDbContext _context;

        public ReposHospital(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public IEnumerable<DisabilityHospital> DisabilityHospital()
        {

            var disability =_context.DisabilityHospitals
                .Include(x => x.Hospital)
                .Include(x => x.Disability)
                .ToList();
            return disability;


        }
        public Hospital FindByUIdHospital(string uid)
        {
            var hospital = _context.Hospitals.FirstOrDefault(h => h.uid == uid);
            return hospital;
        }





    }
}
