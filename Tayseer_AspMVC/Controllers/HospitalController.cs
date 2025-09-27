using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoposHospital _roposHospital;
        private readonly IWebHostEnvironment _env;

        public HospitalController(IUnitOfWork unitOfWork, IRoposHospital hospitalRepo, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _roposHospital = hospitalRepo;
            _env = env;
        }


        //-----------------------------------------------------------------------------------------------------------------------
        // Image

        private string? SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
                throw new InvalidOperationException("امتداد الملف غير مسموح");

            var folder = Path.Combine("uploads", "Hospital");
            var rootFolder = Path.Combine(_env.WebRootPath, folder);
            Directory.CreateDirectory(rootFolder);

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(rootFolder, fileName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                file.CopyTo(stream);
            }

            var relativePath = Path.Combine(folder, fileName).Replace('\\', '/');
            return "/" + relativePath;
        }

        private void DeleteImageIfExists(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }



        //-----------------------------------------------------------------------------------------------------------------------
        // Disability Hospital
        public IActionResult DisabilityHospital()
        {
            var disabilityHospitals = _unitOfWork.roposHospital.DisabilityHospital(); 
            return View(disabilityHospitals);
        }

        public IActionResult CreateDisabilityHospital()
        {
            // نجيب المستشفيات والإعاقات من قاعدة البيانات
            ViewBag.Hospitals = new SelectList(_unitOfWork.Hospitals.FindAll(), "Id", "Name");
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
      
        public IActionResult CreateDisabilityHospital(DisabilityHospital disabilityHospital)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DisabilityHospitals.Add(disabilityHospital);
                _unitOfWork.Save();

                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction(nameof(DisabilityHospital));
            }

            // لو فيه خطأ نرجع القوائم
            ViewBag.Hospitals = new SelectList(_unitOfWork.Hospitals.FindAll(), "Id", "Name", disabilityHospital.HospitalId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityHospital.DisabilityId);

            return View(disabilityHospital);
        }


        public IActionResult EditDisabilityHospital(int id)
        {
            var disabilityHospital = _unitOfWork.DisabilityHospitals.FindById(id);
            ViewBag.Hospitals = new SelectList(_unitOfWork.Hospitals.FindAll(), "Id", "Name", disabilityHospital.HospitalId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityHospital.DisabilityId);
            return View();
        }

        [HttpPost]
        public IActionResult EditDisabilityHospital(DisabilityHospital disabilityHospital)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DisabilityHospitals.Update(disabilityHospital);
                _unitOfWork.Save();

                TempData["Edit"] = "تم تعديل البيانات بنجاح";
                return RedirectToAction(nameof(DisabilityHospital));
            }

            // لو فيه خطأ نرجع القوائم
            ViewBag.Hospitals = new SelectList(_unitOfWork.Hospitals.FindAll(), "Id", "Name", disabilityHospital.HospitalId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityHospital.DisabilityId);

            return View(disabilityHospital);
        }


        public IActionResult DeleteDisabilityHospital(int id)
        {
            var disabilityHospital = _unitOfWork.DisabilityHospitals.FindById(id);
            ViewBag.Hospitals = new SelectList(_unitOfWork.Hospitals.FindAll(), "Id", "Name", disabilityHospital.HospitalId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityHospital.DisabilityId);
            return View();
        }

        [HttpPost]
        public IActionResult DeletePostDisabilityHospital(int id)
        {
            var disabilityHospital = _unitOfWork.DisabilityHospitals.FindById(id);
            _unitOfWork.DisabilityHospitals.Delete(disabilityHospital);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف البيانات بنجاح";
            return RedirectToAction(nameof(DisabilityHospital));
        }



        //-----------------------------------------------------------------------------------------------------------------------
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
            if (ModelState.IsValid)
            {
                if (hospital.ImageFile != null)
                {
                    var imagePath = SaveImage(hospital.ImageFile);
                    hospital.ImageUrl = imagePath;
                }
            }
                _unitOfWork.Hospitals.Add(hospital);
                _unitOfWork.Save();

                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction("Index");
            
           
        }

        // GET: Hospital/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var hospital = _roposHospital.FindById(id);
            return View(hospital);

        }

        // POST: Hospital/Edit
        [HttpPost]
        public IActionResult Edit(Hospital hospital)
        {
            var exist = _unitOfWork.Hospitals.FindById(hospital.Id);
            if (exist == null) return NotFound();

            // عدل القيم
            exist.Name = hospital.Name;
            exist.Address = hospital.Address;
            exist.Services = hospital.Services;

            if (hospital.ImageFile != null)
            {
                // حذف الصورة القديمة إذا موجودة
                DeleteImageIfExists(exist.ImageUrl);

                // حفظ الصورة الجديدة
                exist.ImageUrl = SaveImage(hospital.ImageFile);
            }

            _unitOfWork.Save();

            TempData["Update"] = "تم تحديث بيانات المركز بنجاح";
            return RedirectToAction("Index");

        }

        // GET: Hospital/Delete
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
            DeleteImageIfExists(hospital.ImageUrl);
            _unitOfWork.Hospitals.Delete(hospital);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف البيانات بنجاح";
            return RedirectToAction("Index");
        }
    }
}
