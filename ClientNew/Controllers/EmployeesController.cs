using Microsoft.AspNetCore.Mvc;

namespace ClientNew.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ActiveEmployees()
        {
            return View();
        }
        public IActionResult DeActiveEmployees()
        {
            return View();
        }
        public IActionResult EmployessServer()
        {
            return View();
        }
    }
}
