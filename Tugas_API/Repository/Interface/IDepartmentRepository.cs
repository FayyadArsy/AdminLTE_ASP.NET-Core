using Tugas_API.Models;
using Tugas_API.ViewModel;

namespace Tugas_API.Repository.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> Get();
        Department Get(string Dept_Id);
        int Insert(DepartmentVM department);
        int Update(Department department);
        int Delete(string Dept_Id);
    }
}
