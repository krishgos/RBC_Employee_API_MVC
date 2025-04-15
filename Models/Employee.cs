using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBC_Employee_API_MVC.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(8, 2)")]
        public decimal HourlyRate { get; set; }
        public double HoursWorked { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal TotalPay => HourlyRate * (decimal)HoursWorked;
    }
}
