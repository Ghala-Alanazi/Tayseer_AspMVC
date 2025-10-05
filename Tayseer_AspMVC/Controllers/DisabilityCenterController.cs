using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tayseer_AspMVC.Filters;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    [SessionAuthorize]
    public class DisabilityCenterController : Controller
    {
       

        private readonly IUnitOfWork _unitOfWork;

            public DisabilityCenterController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

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
        }

    }
