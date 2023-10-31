using Microsoft.AspNetCore.Mvc;

namespace ClientNew.Controllers
{
    public class DepartmensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
