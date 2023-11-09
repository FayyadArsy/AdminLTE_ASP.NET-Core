using Microsoft.EntityFrameworkCore;
using Tugas_API.Context;
using Tugas_API.ViewModel;

namespace Tugas_API.Repository
{
    public class ChartRepository
    {
        private readonly MyContext context;
        public ChartRepository(MyContext context)
        {
            this.context = context;
        }
        public IEnumerable<ChartVM> GetDepartmentEmployee()
        {
            var query = context.Employees
                .GroupBy(employee => new { employee.Department.Name })
                .Select(group => new ChartVM
                {
                    DepartmentName = group.Key.Name,
                    Status = group.Count(employee => employee.status == true),
                    Anggota = group.Count(employee => employee.status == false)
                })
    .ToList();
            return query;
        }
    }
}
