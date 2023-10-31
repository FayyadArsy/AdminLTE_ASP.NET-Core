using System.ComponentModel.DataAnnotations;

namespace Tugas_API.Models
{
    public class Department
    {
        [Key]
        public string? Dept_Id {get; set;}
        public string? Name { get; set;}
       
    }
}
