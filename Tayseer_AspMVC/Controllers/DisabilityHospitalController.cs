using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tayseer_AspMVC.Filters;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    [SessionAuthorize]
    public class DisabilityHospitalController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IReposHospital _roposHospital;
      

        public DisabilityHospitalController(IUnitOfWork unitOfWork, IReposHospital hospitalRepo)
        {
            _unitOfWork = unitOfWork;
            _roposHospital = hospitalRepo;
      
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



    }
}
