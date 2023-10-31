using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using Tugas_API.Models;
using Tugas_API.Repository;
using Tugas_API.Repository.Interface;
using Tugas_API.ViewModel;

namespace Tugas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository repository;
        public DepartmentController(DepartmentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var allData = repository.Get();
            return Ok(new { status = HttpStatusCode.OK, response = "Menampilkan Seluruh Data", allData });
        }
        [HttpPost]
        public virtual ActionResult Insert(DepartmentVM insert)
        {
            try
            {
                repository.Insert(insert);
                return Ok(new { status = HttpStatusCode.OK, response = "Data Berhasil Ditambah" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, response = "Gagal Menambah Data", ErrorMessage = e.Message });
            }
        }
        [HttpGet("{dept_id}")]
        public virtual ActionResult Get(string dept_id)
        {

            var get = repository.Get(dept_id);
            if (get == null)
            {
                /*return StatusCode(StatusCodes.Status404NotFound, "Error message");*/
                return NotFound(new { response = "Data Tidak Ditemukan", get });
            }
            else
                return Ok(new { status = HttpStatusCode.OK, response = "Data Ditemukan", get });
        }
        [HttpPut("{dept_id}")]
        public virtual ActionResult Update(Department department)
        {

            try
            {
                var updateResult = repository.Update(department);

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
        [HttpDelete("{dept_id}")]
        public virtual ActionResult Delete(string dept_id)
        {

            var delete = repository.Delete(dept_id);
            if (delete >= 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Berhasil Dihapus", Data = delete });
            }
            else if (delete == 0)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Data dengan Id " + dept_id + " Tidak Ditemukan", Data = delete });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Terjadi Kesalahan", Data = delete });
            }
        }
    }
}
