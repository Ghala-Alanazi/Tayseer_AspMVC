using Microsoft.AspNetCore.Mvc;

namespace Tayseer_AspMVC.Controllers
{
    public class HomeEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
