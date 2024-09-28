using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public int Experience { get; set; }

    }
}
