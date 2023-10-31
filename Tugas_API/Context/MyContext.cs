using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using Tugas_API.Models;

namespace Tugas_API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

       
    }
 
}
