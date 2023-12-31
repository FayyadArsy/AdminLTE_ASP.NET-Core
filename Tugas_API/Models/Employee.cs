﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tugas_API.Models
{
    public class Employee
    {
        [Key]
        public string? NIK { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [JsonIgnore]
        public Department? Department { get; set; }
        [ForeignKey("Department")]
        public string? Department_Id {  get; set; }
        public bool? status { get; set; }

    }
}
