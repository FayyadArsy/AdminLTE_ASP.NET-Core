using Microsoft.AspNetCore.Mvc;

namespace ClientNew.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
