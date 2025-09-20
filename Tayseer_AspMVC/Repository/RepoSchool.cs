using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Data;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Repository
{
    public class RepoSchool : MainRepository<School>, IRepoSchool
    {
        private readonly ApplicationDbContext _context;
        public RepoSchool(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       

    }
}
