
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Filters;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{

    [SessionAuthorize]
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
        // Centers

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


        public IActionResult Index()
        {
            IEnumerable<Centers> centers = _unitOfWork.Centers.FindAll();
            return View(centers);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Regions = GetRegions();
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
                ViewBag.Regions = GetRegions();

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
            ViewBag.Regions = GetRegions();
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
                exist.Region = center.Region;
                exist.Address = center.Address;

            if (center.ImageFile != null)
                {
                    // حذف الصورة القديمة إذا موجودة
                    DeleteImageIfExists(exist.ImageUrl);

                    // حفظ الصورة الجديدة
                    exist.ImageUrl = SaveImage(center.ImageFile);
                }

                _unitOfWork.Save();
                ViewBag.Regions = GetRegions();

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
