using System.ComponentModel.DataAnnotations;

namespace RBC_Employee_API_MVC.Models
{
    public class EmployeeBindingTarget
    {
        [Required]
        public string EmployeeName { get; set; } = "";
        [Range(1, 1000)]
        public decimal HourlyRate { get; set; }
        [Range(1, 100)]
        public double HoursWorked { get; set; }


        public Employee ToEmployee() => new Employee()
        {
            EmployeeName = this.EmployeeName,
            HourlyRate = this.HourlyRate,
            HoursWorked = this.HoursWorked
        };
    }
}
