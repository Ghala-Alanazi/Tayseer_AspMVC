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
        // Disability School

        public IActionResult DisabilitySchool()
        {
            var Disability = _unitOfWork.Schools.DisabilitySchool();

            return View(Disability);
        }

        public IActionResult CreateDisabilitySchool()
        {
            // نجيب المدارس والإعاقات من قاعدة البيانات
            ViewBag.Schools = new SelectList(_unitOfWork.Schools.FindAll(), "Id", "Name");
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult CreateDisabilitySchool(DisabilitySchool disabilitySchool)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DisabilitySchools.Add(disabilitySchool);
                _unitOfWork.Save();

                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction(nameof(DisabilitySchool));
            }

            // لو فيه خطأ نرجع القوائم
            ViewBag.Schools = new SelectList(_unitOfWork.Schools.FindAll(), "Id", "Name", disabilitySchool.SchoolId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilitySchool.DisabilityId);

            return View(disabilitySchool);
        }

        public IActionResult EditDisabilitySchool(int id)
        {
            var disabilitySchool = _unitOfWork.DisabilitySchools.FindById(id);
            ViewBag.Schools = new SelectList(_unitOfWork.Schools.FindAll(), "Id", "Name", disabilitySchool.SchoolId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilitySchool.DisabilityId);
            return View(disabilitySchool);
        }

        [HttpPost]
        public IActionResult EditDisabilitySchool(DisabilitySchool disabilitySchool)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DisabilitySchools.Update(disabilitySchool);
                _unitOfWork.Save();

                TempData["Edit"] = "تم تعديل البيانات بنجاح";
                return RedirectToAction(nameof(DisabilitySchool));
            }

            // لو فيه خطأ نرجع القوائم
            ViewBag.Schools = new SelectList(_unitOfWork.Schools.FindAll(), "Id", "Name", disabilitySchool.SchoolId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilitySchool.DisabilityId);

            return View(disabilitySchool);
        }

        public IActionResult DeleteDisabilitySchool(int id)
        {
            var disabilitySchool = _unitOfWork.DisabilitySchools.FindById(id);
            ViewBag.Schools = new SelectList(_unitOfWork.Schools.FindAll(), "Id", "Name", disabilitySchool.SchoolId);
            ViewBag.Disabilities = new SelectList(_unitOfWork.Disabilitys.FindAll(), "Id", "Name", disabilitySchool.DisabilityId);
            return View(disabilitySchool);
        }

        [HttpPost]
        public IActionResult DeletePostDisabilitySchool(int id)
        {
            var disabilitySchool = _unitOfWork.DisabilitySchools.FindById(id);
            _unitOfWork.DisabilitySchools.Delete(disabilitySchool);
            _unitOfWork.Save();

            TempData["Delete"] = "تم حذف البيانات بنجاح";
            return RedirectToAction(nameof(DisabilitySchool));
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

        public IActionResult Index()
        {
            var schools = _unitOfWork.Schools.FindAll();
            return View(schools);
        }



        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(School school)
        {
            if (!ModelState.IsValid) return View(school);

            if (school.ImageFile != null)
                school.ImageUrl = SaveImage(school.ImageFile);

            _unitOfWork.Schools.Add(school);
            _unitOfWork.Save();

            TempData["Add"] = "تمت إضافة المدرسة بنجاح";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
            if (school == null) return NotFound();
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

            if (school.ImageFile != null)
            {
                DeleteImageIfExists(exist.ImageUrl);
                exist.ImageUrl = SaveImage(school.ImageFile);
            }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
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
