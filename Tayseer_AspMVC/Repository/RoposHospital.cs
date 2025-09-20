using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Data;
using Tayseer_AspMVC.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Tayseer_AspMVC.Repository
{
    public class RoposHospital : MainRepository<Hospital>, IRoposHospital
    {
        private readonly ApplicationDbContext _context;

        public RoposHospital(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       

      
    }
}
