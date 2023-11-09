using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tugas_API.Repository;

namespace Tugas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly ChartRepository repository;
        public ChartController(ChartRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var AllData = repository.GetDepartmentEmployee();
            return Ok(new { status = HttpStatusCode.OK, response = "Menampilkan Seluruh Anggota Department", AllData });
        }
    }
}
