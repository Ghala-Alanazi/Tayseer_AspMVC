using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HospitalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public IActionResult Index()
        {
            IEnumerable<Hospital> Hospitals = _unitOfWork.Hospitals.FindAll();
            return View(Hospitals);
        }

        private void CreateDisabilitySelectList()
        {
            IEnumerable<Disability> Disabilitys = _unitOfWork.Disabilitys.FindAll();

            SelectList selectListItems = new SelectList(Disabilitys, "Id", "Name", 0);
            ViewBag.Disabilitys = selectListItems;
        }


        [HttpGet]
        public IActionResult Create()
        {
            CreateDisabilitySelectList();
            return View();
        }


        [HttpPost]
        public IActionResult Create(Hospital hospitals)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Hospitals.Add(hospitals);
                _unitOfWork.Save();


                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return View("Index");
            }
            else
            {
                return View(hospitals);
            }
        }


            [HttpGet]

            public IActionResult Edit(int Id)
            {
                var Disa = _unitOfWork.Hospitals.FindById(Id);
                CreateDisabilitySelectList();
                return View(Disa);
            }


            [HttpPost]
            public IActionResult Edit(Hospital hospital)
            {

                _unitOfWork.Hospitals.Update(hospital);
                _unitOfWork.Save();
                TempData["Update"] = "تم تحديث البيانات بنجاح";
                return RedirectToAction("Index");
            }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var Disa = _unitOfWork.Hospitals.FindById(Id);

            return View(Disa);
        }


        [HttpPost]
        public IActionResult Deletepost(int Id)
        {
            var Disa = _unitOfWork.Hospitals.FindById(Id);
            _unitOfWork.Hospitals.Delete(Disa);
            _unitOfWork.Save();
            TempData["Remove"] = "تم حذف البيانات بنجاح";
            return RedirectToAction("Index");

        }
    }
    } 
