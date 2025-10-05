using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tayseer_AspMVC.Dtos;
using Tayseer_AspMVC.Filters;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    [SessionAuthorize]
    public class DisabilityController : Controller
    {
        
        
            private readonly IUnitOfWork _unitOfWork;
            public DisabilityController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }



            // عرض قائمة الإعاقات
            public IActionResult Index()
            {
                var disabilities = _unitOfWork.Disabilitys.FindAll();
                return View(disabilities);
            }

        //يرجع البيانات بصيغة JSON مع عدد المنتجات المرتبطة
        public IActionResult GetAll()
        {
            var disabilities = _unitOfWork.Disabilitys.FindAll();

            disabilities.Select(d => new DisabilityDto
                {
                    Id = d.Id,
                    Name = d.Name,
                   // HospitalName = d.Hospitals != null ? d.Hospitals.Name : "لا يوجد مستشفى مرتبط"
                })
                .ToList();

            return Ok(disabilities); // يرجع JSON بشكل منظم
        }


        [HttpGet]
            public IActionResult Create()
            {
                return View();
            }


            [HttpPost]
            public IActionResult Create(Disability Disabilitys)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Disabilitys.Add(Disabilitys);
                    _unitOfWork.Save();


                    TempData["Add"] = "تم اضافة البيانات بنجاح";
                    return View("Index");
                }
                else
                {
                    return View(Disabilitys);
                }
            }


            [HttpGet]

            public IActionResult Edit(int Id)
            {
                var Disa = _unitOfWork.Disabilitys.FindById(Id);
                return View(Disa);
            }


            [HttpPost]
            public IActionResult Edit(Disability Disabilitys)
            {

                _unitOfWork.Disabilitys.Update(Disabilitys);
                _unitOfWork.Save();
                TempData["Update"] = "تم تحديث البيانات بنجاح";
                return RedirectToAction("Index");
            }


            [HttpGet]
            public IActionResult Delete(int Id)
            {
                var Disa = _unitOfWork.Disabilitys.FindById(Id);

                return View(Disa);
            }


            [HttpPost]
            public IActionResult Deletepost(int Id)
            {
                var Disa = _unitOfWork.Disabilitys.FindById(Id);
                _unitOfWork.Disabilitys.Delete(Disa);
                _unitOfWork.Save();
                TempData["Remove"] = "تم حذف البيانات بنجاح";
                return RedirectToAction("Index");

            }
        
    }
}
