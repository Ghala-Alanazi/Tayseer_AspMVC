using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tayseer_AspMVC.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoposHospital _roposHospital;

        public HospitalController(IUnitOfWork unitOfWork, IRoposHospital hospitalRepo)
        {
            _unitOfWork = unitOfWork;
            _roposHospital = hospitalRepo;
        }




        public IActionResult DisabilityHospital()
        {
            var Disability = _unitOfWork.roposHospital.DisabilityHospital();

            return View(Disability);
        }



        // GET: Hospital
        [HttpGet]
        public IActionResult Index()
        {
            var hospitals = _unitOfWork.Hospitals.FindAll();
            if (hospitals.Any())
                TempData["Success"] = "تم جلب البيانات بنجاح";
            else
                TempData["Error"] = "لا توجد بيانات لعرضها";

            return View(hospitals);
        }

        // GET: Hospital/Create
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Hospital/Create
        [HttpPost]
        public IActionResult Create(Hospital hospital)
        {
          

                _unitOfWork.Hospitals.Add(hospital);
                _unitOfWork.Save();

                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction("Index");
            
           
        }

        // GET: Hospital/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var hospital = _roposHospital.FindById(id);
           
            return View(hospital);
        }

        // POST: Hospital/Edit/5
        [HttpPost]
        public IActionResult Edit(Hospital hospital)
        {
            
                _unitOfWork.Hospitals.Update(hospital);
                _unitOfWork.Save();

                TempData["Update"] = "تم تحديث البيانات بنجاح";
             
                 return RedirectToAction("Index");

        }

        // GET: Hospital/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var hospital = _unitOfWork.Hospitals.FindById(id);
           
            return View(hospital);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            var hospital = _unitOfWork.Hospitals.FindById(id);

            _unitOfWork.Hospitals.Delete(hospital);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف البيانات بنجاح";
            return RedirectToAction("Index");
        }
    }
}
