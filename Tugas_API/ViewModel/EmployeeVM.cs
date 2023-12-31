﻿namespace Tugas_API.ViewModel
{
    public class EmployeeVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Status {  get; set; }
        public string Department_Id {  get; set; }
        public string Department_Name { get; set; }
    }
    public class InsertEmployeeVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Department_Id { get; set; }
    }

}
