using Tugas_API.Controllers;
using Tugas_API.Models;
using Tugas_API.ViewModel;

namespace Tugas_API.Repository.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(string NIK);
        int Insert(InsertEmployeeVM employee);
        int Update(Employee employee);
        int Delete(string NIK);
        public IEnumerable<PegawaiAktivVM> GetPegawaiAktiv();
        public IEnumerable<PegawaiAktivVM> GetPegawaideAktiv();
        public IEnumerable<PegawaiAktivVM> GetPegawaiAktivdept(PegawaiVM pegawai);



    }
}
