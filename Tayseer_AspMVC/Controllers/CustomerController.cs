using Microsoft.AspNetCore.Mvc;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;

namespace Tayseer_AspMVC.Controllers
{
    public class CustomerController : Controller
    
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Index
        public IActionResult Index()
        {
            var customers = _unitOfWork.Customers.FindAll();
            return View(customers);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customers.Add(customer);
                _unitOfWork.Save();
                TempData["Add"] = "تمت إضافة العميل بنجاح";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Edit GET
        public IActionResult Edit(int id)
        {
            var customer = _unitOfWork.Customers.FindById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // Edit POST
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Save();
                TempData["Edit"] = "تم تعديل بيانات العميل بنجاح";
               return RedirectToAction("Index");
            }
            return View(customer);
        }

        // Delete GET
        public IActionResult Delete(int id)
        {
            var customer = _unitOfWork.Customers.FindById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // Delete POST
       
        public IActionResult Deletepost(int id)
        {
            var customer = _unitOfWork.Customers.FindById(id);
            if (customer == null) return NotFound();

            _unitOfWork.Customers.Delete(customer);
            _unitOfWork.Save();
            TempData["Delete"] = "تم حذف العميل بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}

