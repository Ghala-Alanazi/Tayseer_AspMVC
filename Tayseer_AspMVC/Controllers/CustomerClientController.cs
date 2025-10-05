using Microsoft.AspNetCore.Mvc;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    public class CustomerClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
          
            return View();
        }
        public IActionResult IndexHospital()
        {
            var hospitals = _unitOfWork.Hospitals.FindAll();
            
            return View(hospitals);
        }

        public IActionResult IndexSchool()
        {
            var schools = _unitOfWork.Schools.FindAll();
            return View(schools);
        }

        public IActionResult IndexCenter()
        {
            IEnumerable<Centers> centers = _unitOfWork.Centers.FindAll();
            return View(centers);
        }


    }
}
