using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    public class SchoolController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IRepoSchool _repoSchool;


        public SchoolController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IRepoSchool repoSchool)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _repoSchool = repoSchool;
        }






        //-----------------------------------------------------------------------------------------------------------------------
        // Images 

        private string? SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
                throw new InvalidOperationException("امتداد الملف غير مسموح");

            var folder = Path.Combine("uploads", "schools");
            var rootFolder = Path.Combine(_env.WebRootPath, folder);
            Directory.CreateDirectory(rootFolder);

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(rootFolder, fileName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                file.CopyTo(stream);
            }

            return "/" + Path.Combine(folder, fileName).Replace('\\', '/');
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
        //  School



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
            var schools = _unitOfWork.Schools.FindAll();
            return View(schools);
        }



        [HttpGet]
        public IActionResult Create() 
        {
            ViewBag.Regions = GetRegions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(School school)
        {
            if (!ModelState.IsValid) return View(school);

            if (school.ImageFile != null)
                school.ImageUrl = SaveImage(school.ImageFile);


            _unitOfWork.Schools.Add(school);
            _unitOfWork.Save();

            ViewBag.Regions = GetRegions();
            TempData["Add"] = "تمت إضافة المدرسة بنجاح";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
            if (school == null) return NotFound();
            ViewBag.Regions = GetRegions();
            return View(school);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(School school)
        {
            if (!ModelState.IsValid) return View(school);

            var exist = _unitOfWork.Schools.FindById(school.Id);
            if (exist == null) return NotFound();

            exist.Name = school.Name;
            exist.Gender = school.Gender;
            exist.Stages = school.Stages;
            exist.Region = school.Region;
            exist.Address = school.Address;

            if (school.ImageFile != null)
            {
                DeleteImageIfExists(exist.ImageUrl);
                exist.ImageUrl = SaveImage(school.ImageFile);
            }
            ViewBag.Regions = GetRegions();
            _unitOfWork.Save();
            TempData["Update"] = "تم تعديل بيانات المدرسة بنجاح";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
            if (school == null) return NotFound();

            return View(school);
        }

        public IActionResult Deletepost(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
            if (school == null) return NotFound();

            DeleteImageIfExists(school.ImageUrl);

            _unitOfWork.Schools.Delete(school);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف المدرسة بنجاح";
            return RedirectToAction("Index");
        }
    }
}
