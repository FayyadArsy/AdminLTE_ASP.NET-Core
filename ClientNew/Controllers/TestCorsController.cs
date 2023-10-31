using Microsoft.AspNetCore.Mvc;

namespace ClientNew.Controllers
{
    public class TestCorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
