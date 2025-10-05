using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tayseer_AspMVC.Filters;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    [SessionAuthorize]
    public class DisabilitySchoolController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepoSchool _repoSchool;


        public DisabilitySchoolController(IUnitOfWork unitOfWork,  IRepoSchool repoSchool)
        {
            _unitOfWork = unitOfWork;
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
    }
}
