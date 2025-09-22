using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tayseer_AspMVC.Models;
using Tayseer_AspMVC.Repository.Base;


namespace Tayseer_AspMVC.Controllers
{
    public class EmployeesController : Controller
    {

            private readonly IUnitOfWork _unitOfWork;

            public EmployeesController(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

            }

            public IActionResult Index()
            {
                var employees = _unitOfWork.Employees.FindAllEmployee();
                CreateUserRoleSelectList();
            if (employees.Any())
                {
                    TempData["Sucees"] = "تم جلب البيانات بنجاح";
                }
                else
                {
                    TempData["Error"] = "لا توجد بيانات لعرضها";
                }
                return View(employees);
            }


        private void CreateUserRoleSelectList()
        {


            IEnumerable<UserRole> UserRoles = _unitOfWork.UserRoles.FindAll();

            SelectList selectListItems = new SelectList(UserRoles, "Id", "Name", 0);
            ViewBag.UserRoles = selectListItems;

        }

        [HttpGet]
            public IActionResult Create()
            {
            CreateUserRoleSelectList();
                return View();
            }


            [HttpPost]
            public IActionResult Create(Employee employee)
            {
                _unitOfWork.Employees.Add(employee);
                _unitOfWork.Save();
                TempData["Add"] = "تم اضافة البيانات بنجاح";
                return RedirectToAction("Index");


            }

            [HttpGet]
            public IActionResult Edit(int Id)
            {
                var cate = _unitOfWork.Employees.FindById(Id);
                CreateUserRoleSelectList();
            return View(cate);
            }


            [HttpPost]
            public IActionResult Edit(Employee emp)
            {
                _unitOfWork.Employees.Update(emp);
                _unitOfWork.Save();
                TempData["Update"] = "تم تعديل البيانات بنجاح";
                return RedirectToAction("Index");


            }

            [HttpGet]
            public IActionResult Delete(int Id)
            {
                var cate = _unitOfWork.Employees.FindById(Id);
                 CreateUserRoleSelectList();
            return View(cate);
            }


            [HttpPost]
            public IActionResult Deletepost(int Id)
            {


                var emp = _unitOfWork.Employees.FindById(Id);
                emp.IsDelete = true;
                emp.UserDelete = HttpContext.Session.GetInt32("Id") ?? 0;
                emp.DeleteDate = DateTime.Now;
                _unitOfWork.Employees.Update(emp);
                _unitOfWork.Save();
                TempData["Remove"] = "تم حذف البيانات بنجاح";
                return RedirectToAction("Index");


            }


        }
    }
