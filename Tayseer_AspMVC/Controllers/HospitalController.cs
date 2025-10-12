using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Tayseer_AspMVC.Filters;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    [SessionAuthorize]
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReposHospital _reposHospital;
        private readonly IWebHostEnvironment _env;

        public HospitalController(IUnitOfWork unitOfWork, IReposHospital hospitalRepo, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _reposHospital = hospitalRepo;
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
        //  Hospital

        private List<SelectListItem> GetRegions()
        {
            return new List<SelectListItem>
        {
             new SelectListItem { Value = "", Text = "اختر المنطقة", Disabled = true, Selected = true },
            new SelectListItem { Value = "شرق الرياض", Text = "شرق الرياض" },
            new SelectListItem { Value = "غرب الرياض", Text = "غرب الرياض" },
            new SelectListItem { Value = "جنوب الرياض", Text = "جنوب الرياض" },
            new SelectListItem { Value = "شمال الرياض", Text = "شمال الرياض" },
            new SelectListItem { Value = "وسط الرياض", Text = "وسط الرياض" }
        };
        }

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
            ViewBag.Regions = GetRegions();
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
            ViewBag.Regions = GetRegions();

            TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction("Index");
            
           
        }

        // GET: Hospital/Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var hospital = _reposHospital.FindById(id);
            ViewBag.Regions = GetRegions();
            return View(hospital);

        }

        // POST: Hospital/Edit
        [HttpPost]
        public IActionResult Edit(Hospital hospital)
        {
            var exist = _unitOfWork.Hospitals.FindById(hospital.Id);
            if (exist == null) return NotFound();

            // عدل القيم
            exist.uid = Guid.NewGuid().ToString();
            exist.Name = hospital.Name;
            exist.Address = hospital.Address;
            exist.Services = hospital.Services;
            exist.Region = hospital.Region;


            if (hospital.ImageFile != null)
            {
                // حذف الصورة القديمة إذا موجودة
                DeleteImageIfExists(exist.ImageUrl);

                // حفظ الصورة الجديدة
                exist.ImageUrl = SaveImage(hospital.ImageFile);
            }

            _unitOfWork.Save();
            ViewBag.Regions = GetRegions();

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
