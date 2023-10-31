using Microsoft.EntityFrameworkCore;
using Tugas_API.Context;
using Tugas_API.Models;
using Tugas_API.Repository.Interface;
using Tugas_API.ViewModel;

namespace Tugas_API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext context;
        public DepartmentRepository(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string Dept_Id)
        {
            var findData = context.Departments.Find(Dept_Id);
            if (findData != null)
            {
                context.Departments.Remove(findData);
            }
            var save = context.SaveChanges();
            return save;
        }

        public IEnumerable<Department> Get()
        {
            return context.Departments.ToList();
        }

        public Department Get(string Dept_Id)
        {
            return context.Departments.Find(Dept_Id);
        }

        public int Insert(DepartmentVM department)
        {
            var insert = new Department
            {
                Dept_Id = CustomDeptId(),
                Name = department.Name,
            };
            context.Departments.Add(insert);
            var setDepartment = context.SaveChanges();
           return setDepartment;
        }

        public int Update(Department department)
        {
            context.Entry(department).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
        private string CustomDeptId()
        {
            string currentDate = "D";
            string newNIK = "";

            var lastData = context.Departments
                .OrderByDescending(department => department.Dept_Id)
                .FirstOrDefault();

            if (lastData == null || !lastData.Dept_Id.StartsWith(currentDate))
            {
                newNIK = currentDate + "001";
            }
            else
            {
                var nikLastData = lastData.Dept_Id;
                string lastThree = nikLastData.Substring(nikLastData.Length - 3);
                int nextSequence = int.Parse(lastThree) + 1;
                newNIK = currentDate + nextSequence.ToString("000");
            }

            return newNIK;
        }
    }
}
