
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
  
    public class CentersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public CentersController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
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

            var folder = Path.Combine("uploads", "centers");
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
        // Disability Center

        public IActionResult DisabilityCenter()
        {
            var disabilityCenters = _unitOfWork.RepoCenters.DisabilityCenter();
            return View(disabilityCenters);
        }

        public IActionResult CreateDisabilityCenter()
        {
            // نجيب المراكز والإعاقات من قاعدة البيانات
            ViewBag.Centers = new SelectList(_unitOfWork.Centers.FindAll(), "Id", "Name");
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateDisabilityCenter(DisabilityCenter disabilityCenter)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DisabilityCenters.Add(disabilityCenter);
                _unitOfWork.Save();

                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction(nameof(DisabilityCenter));
            }

            // لو فيه خطأ نرجع القوائم
            ViewBag.Centers = new SelectList(_unitOfWork.Centers.FindAll(), "Id", "Name", disabilityCenter.CentersId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityCenter.DisabilityId);

            return View(disabilityCenter);
        }


        public IActionResult EditDisabilityCenter(int id)
        {
            var disabilityCenter = _unitOfWork.DisabilityCenters.FindById(id);
            ViewBag.Centers = new SelectList(_unitOfWork.Centers.FindAll(), "Id", "Name", disabilityCenter.CentersId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityCenter.DisabilityId);
            return View(disabilityCenter);
        }

        [HttpPost]
        public IActionResult EditDisabilityCenter(DisabilityCenter disabilityCenter)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DisabilityCenters.Update(disabilityCenter);
                _unitOfWork.Save();

                TempData["Edit"] = "تم تعديل البيانات بنجاح";
                return RedirectToAction(nameof(DisabilityCenter));
            }

            // لو فيه خطأ نرجع القوائم
            ViewBag.Centers = new SelectList(_unitOfWork.Centers.FindAll(), "Id", "Name", disabilityCenter.CentersId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityCenter.DisabilityId);

            return View(disabilityCenter);
        }


        public IActionResult DeleteDisabilityCenter(int id)
        {
            var disabilityCenter = _unitOfWork.DisabilityCenters.FindById(id);
            ViewBag.Centers = new SelectList(_unitOfWork.Centers.FindAll(), "Id", "Name", disabilityCenter.CentersId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilityCenter.DisabilityId);
            return View(disabilityCenter);
        }

        [HttpPost]
        public IActionResult DeletePostDisabilityCenter(int id)
        {
            var disabilityCenter = _unitOfWork.DisabilityCenters.FindById(id);
            _unitOfWork.DisabilityCenters.Delete(disabilityCenter);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف البيانات بنجاح";
            return RedirectToAction(nameof(DisabilityCenter));
        }


        //-----------------------------------------------------------------------------------------------------------------------
        // Centers

        public IActionResult Index()
        {
            IEnumerable<Centers> centers = _unitOfWork.Centers.FindAll();
            return View(centers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(Centers center)
        {
            if (ModelState.IsValid)
            {
                if (center.ImageFile != null)
                {
                    var imagePath = SaveImage(center.ImageFile);
                    center.ImageUrl = imagePath;
                }

                _unitOfWork.Centers.Add(center);
                _unitOfWork.Save();

                TempData["Add"] = "تم اضافة المركز بنجاح";
                return RedirectToAction("Index");
            }
            return View(center);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var center = _unitOfWork.Centers.FindById(Id);
            if (center == null) return NotFound();
            return View(center);
        }

        [HttpPost]
        public IActionResult Edit(Centers center)
        {
         
            
                // جلب الكائن الأصلي من الـ DbContext (متتبع)
                var exist = _unitOfWork.Centers.FindById(center.Id);
                if (exist == null) return NotFound();

                // تحديث الحقول فقط
                exist.Name = center.Name;
                exist.Gender = center.Gender;
                exist.Services = center.Services;

                if (center.ImageFile != null)
                {
                    // حذف الصورة القديمة إذا موجودة
                    DeleteImageIfExists(exist.ImageUrl);

                    // حفظ الصورة الجديدة
                    exist.ImageUrl = SaveImage(center.ImageFile);
                }

                _unitOfWork.Save();

                TempData["Update"] = "تم تحديث بيانات المركز بنجاح";
                return RedirectToAction("Index");
            

        }

        public IActionResult Delete(int id)
        {
            var Centers = _unitOfWork.Centers.FindById(id);

            return View(Centers);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {

            var center = _unitOfWork.Centers.FindById(id);
            if (center == null) return NotFound();

            DeleteImageIfExists(center.ImageUrl);

            _unitOfWork.Centers.Delete(center);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف المركز بنجاح";
            return RedirectToAction("Index");
        }

    }
}
