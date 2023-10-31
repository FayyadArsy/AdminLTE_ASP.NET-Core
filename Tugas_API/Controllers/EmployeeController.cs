using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tugas_API.Models;
using Tugas_API.Repository;
using Tugas_API.Repository.Interface;
using Tugas_API.ViewModel;

namespace Tugas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository repository;
        public EmployeeController(EmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("paging")]
        public ActionResult PostData(JqueryDatatableParam param)
        {
            var draw = param.draw;
            var start = param.iDisplayStart;
            var length = param.iDisplayLength;
            var searchValue = param.search?.value; // Mengakses nilai pencarian

            // Selanjutnya, Anda dapat menggunakan nilai-nilai ini dalam operasi bisnis Anda
            var employees = repository.GetEmployees(start, length, searchValue);

            var totalRecords = repository.GetTotalRecords();
            var totalDisplayRecords = repository.GetTotalDisplayRecords(searchValue);

            return Ok(new
            {
                draw = draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalDisplayRecords,
                data = employees
            });
        }

        [HttpGet("TestCors")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }

        [HttpGet]
        public ActionResult Get()
        {
            var allData = repository.Get();
            return Ok(new { status = HttpStatusCode.OK, response = "Menampilkan Seluruh Data", allData });
        }
        [HttpPost]
        public virtual ActionResult Insert(InsertEmployeeVM employee)
        {
            try
            {
                
                var phoneExists = repository.CheckPhoneNumber(employee.PhoneNumber);

             

                if (phoneExists)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, response = "Nomor HP sudah ada dalam database." });
                }

                repository.Insert(employee);

                return Ok(new { status = HttpStatusCode.OK, response = "Data Berhasil Ditambah" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, response = "Gagal Menambah Data", ErrorMessage = e.Message });
            }
        }
        [HttpGet("{NIK}")]
        public virtual ActionResult Get(string NIK)
        {

            var get = repository.Get(NIK);
            if (get == null)
            {
                /*return StatusCode(StatusCodes.Status404NotFound, "Error message");*/
                return NotFound(new { response = "Data Tidak Ditemukan", get });
            }
            else
                return Ok(new { status = HttpStatusCode.OK, response = "Data Ditemukan", get });
        }
        [HttpPut("{NIK}")]
        public virtual ActionResult Update(Employee employee)
        {

            try
            {
             
                var phoneExists = repository.CheckPhoneNumberEdit(employee.NIK, employee.PhoneNumber);

              

                if (phoneExists)
                {
                    return Ok(new { status = HttpStatusCode.Created, response = "Nomor HP sudah ada dalam database." });
                }

                var updateResult = repository.Update(employee);

                if (updateResult == null)
                {
                    return NotFound(new { status = HttpStatusCode.NotFound, response = "Data Tidak Ditemukan" });
                }

                return Ok(new { status = HttpStatusCode.OK, response = "Data Berhasil Diubah" });
            }
            catch (Exception e)
            {
                return BadRequest(new { response = "Gagal", ErrorMessage = e.Message });
            }
        }
        [HttpDelete("{NIK}")]
        public virtual ActionResult Delete(string NIK)
        {

            try
            {
                var get = repository.Delete(NIK);
                return Ok(new { status = HttpStatusCode.OK, response = "Data Berhasil Dihapus" });
            }
            catch (Exception e)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, response = "Data Tidak Ditemukan", e.Message });
            }
        }
        [HttpGet("Pegawaiaktif")]
        public ActionResult GetPegawaiAktiv()
        {
            var allData = repository.GetPegawaiAktiv();
            return Ok(new { status = HttpStatusCode.OK, response = "Menampilkan Seluruh Data pegawai aktiv", allData });
        }
        [HttpGet("Pegawaideaktif")]
        public ActionResult GetPegawaideAktiv()
        {
            var allData = repository.GetPegawaideAktiv();
            return Ok(new { status = HttpStatusCode.OK, response = "Menampilkan Seluruh Data pegawai resign", allData });
        }

        [HttpGet("Pegawaideaktif_dept")]

        public ActionResult GetPegawaiAktivdept(PegawaiVM pegawai)
        {
            var allData = repository.GetPegawaideAktiv();
            return Ok(new { status = HttpStatusCode.OK, response = "Menampilkan Seluruh Data pegawai resign", allData });
        }

        [HttpGet("GetDetailedEmployee")]
        public virtual ActionResult GetDetailedEmployee()
        {
            var get = repository.GetDetailedEmployee();
            if (get.Count() != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = get.Count() + " Data Ditemukan", Data = get });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data Tidak Ditemukan" });
            }
        }
    }
}
