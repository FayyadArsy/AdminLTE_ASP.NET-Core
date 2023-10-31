using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Security.Principal;
using Tugas_API.Context;
using Tugas_API.Controllers;
using Tugas_API.Models;
using Tugas_API.Repository.Interface;
using Tugas_API.ViewModel;

namespace Tugas_API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }
        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }
        public IEnumerable<EmployeeVM> GetEmployees(int start, int length, string searchValue)
        {
            var query = context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(e => e.FirstName.Contains(searchValue) || e.LastName.Contains(searchValue) || e.Email.Contains(searchValue));
            }

            var employees = query.Skip(start).Take(length).Select(a => new EmployeeVM
            {
                NIK = a.NIK,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                Department_Id = a.Department.Dept_Id,
                Department_Name = a.Department.Name,
                Status = (bool)a.status
            }).ToList();

            return employees;
        }

        public int GetTotalRecords()
        {
            return context.Employees.Count();
        }

        public int GetTotalDisplayRecords(string searchValue)
        {
            var query = context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(e => e.FirstName.Contains(searchValue) || e.LastName.Contains(searchValue) || e.Email.Contains(searchValue));
            }

            return query.Count();
        }

        public int GetTotalEmployeeCount()
        {
            return context.Employees.Count();
        }
        public Employee Get(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            return entity;
        }
        public int Delete(string NIK)
        {
            var findData = context.Employees.Find(NIK);
            if (findData != null)
            {
                findData.status = false;
            }
            var save = context.SaveChanges();
            return save;
        }
        public IEnumerable<PegawaiAktivVM> GetPegawaideAktiv()
        {
            var query = (from employee in context.Employees
                         join department in context.Departments on employee.Department_Id equals department.Dept_Id
                         where employee.status == false

                         select new PegawaiAktivVM
                         {
                             fullname = employee.FirstName + " " + employee.LastName,

                             email = employee.Email,

                             department = department.Name
                         }).ToList();

            return query;
        }
        public IEnumerable<PegawaiAktivVM> GetPegawaiAktiv()
        {
            var query = (from employee in context.Employees
                         join department in context.Departments on employee.Department_Id equals department.Dept_Id where employee.status==true
                         
                         select new PegawaiAktivVM
                         {
                             fullname = employee.FirstName + " " + employee.LastName,
                           
                             email = employee.Email,
                            
                            department = department.Name
                    }).ToList();
            return query;
        }
        public IEnumerable<PegawaiAktivVM> GetPegawaiAktivdept(PegawaiVM pegawai)
        {
            var query = (from employee in context.Employees
                         join department in context.Departments on employee.Department_Id equals department.Dept_Id
                         where department.Dept_Id == pegawai.department_ID

                         select new PegawaiAktivVM
                         {
                             fullname = employee.FirstName + " " + employee.LastName,

                             email = employee.Email,

                             department = department.Name
                         }).ToList();
            return query;
        }
        public int Insert(InsertEmployeeVM employee)
        {
            var generatedEmail = CustomEmail(employee.FirstName, employee.LastName);
            var insert = new Employee
            {
                NIK = CustomNIK(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = generatedEmail,
    

                Address = employee.Address,
                status = employee.Status,
                Department_Id = employee.Department_Id
            };
            context.Employees.Add(insert);
            var setEmployee = context.SaveChanges();
            if (setEmployee > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private string CustomNIK()
        {
            string currentDate = DateTime.Now.ToString("ddMMyy");
            string newNIK = "";

            var lastData = context.Employees
                .OrderByDescending(employee => employee.NIK)
                .FirstOrDefault();

            if (lastData == null || !lastData.NIK.StartsWith(currentDate))
            {
                newNIK = currentDate + "001";
            }
            else
            {
                var nikLastData = lastData.NIK;
                string lastThree = nikLastData.Substring(nikLastData.Length - 3);
                int nextSequence = int.Parse(lastThree) + 1;
                newNIK = currentDate + nextSequence.ToString("000");
            }

            return newNIK;
        }
        private string CustomEmail(string firstName, string lastName)
        {
            string baseEmail = $"{firstName}.{lastName}@berca.co.id";
            string generatedEmail = baseEmail;
            int counter = 1;

            while (context.Employees.Any(e => e.Email == generatedEmail))
            {
                generatedEmail = $"{baseEmail.Substring(0, baseEmail.IndexOf('@'))}{counter:D3}@berca.co.id";
                counter++;
            }

            return generatedEmail;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
        public bool CheckPhoneNumber(string phone)
        {
            var data = context.Employees.AsNoTracking().FirstOrDefault(employee => employee.PhoneNumber == phone);
            if (data == null)
            {
                return false;
            }
            return true;
        }
        public bool CheckPhoneNumberEdit(string NIK, string phone)
        {
            var existingEmployeeWithPhone = context.Employees
            .AsNoTracking()
            .FirstOrDefault(employee => employee.PhoneNumber == phone);

            if (existingEmployeeWithPhone == null)
            {
                return false; 
            }

            if (existingEmployeeWithPhone.NIK == NIK)
            {
                return false; 
            }

            return true;
        }
        public IEnumerable<object> GetDetailedEmployee()
        {
            var getDetailedEmployee = context.Employees.Include(d => d.Department).ToList();
            var detailedEmployee = getDetailedEmployee.Select(a => new EmployeeVM
            {
                NIK = a.NIK,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                Department_Id = a.Department.Dept_Id,
                Department_Name = a.Department.Name,
                Status = (bool)a.status
            });
            return detailedEmployee;
        }



    }
}
