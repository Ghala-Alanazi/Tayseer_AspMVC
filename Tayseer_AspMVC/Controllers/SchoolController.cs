using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    public class SchoolController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public SchoolController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        // GET: Index
        public IActionResult Index()
        {

            var schools = _unitOfWork.Schools.FindAll();
            return View(schools);
        }

       
        // GET: Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Create


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(School school)


        { 
            _unitOfWork.Schools.Add(school);
                
              _unitOfWork.Save();
            

            TempData["Add"] = "تمت إضافة المدرسة بنجاح";
            return RedirectToAction("Index");
        }


        // GET: Edit
        public IActionResult Edit(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
          
            return View(school);
        }

        // POST: Edit
        [HttpPost]
       
        public IActionResult Edit( School school)
        {
           
                _unitOfWork.Schools.Update(school);
                _unitOfWork.Save(); // حفظ التعديلات
               
            TempData["Update"] = "تم تعديل البيانات بنجاح";
            return RedirectToAction("Index");
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
         
            return View(school);
        }

        // POST: Deletepost
        [HttpPost]
       
        public IActionResult Deletepost(int id)
        {
            var school = _unitOfWork.Schools.FindById(id);
           _unitOfWork.Schools.Delete(school);
            _unitOfWork.Save();
            TempData["Remove"] = "تم حذف البيانات بنجاح";
            return RedirectToAction("Index");
        }
    }
}
