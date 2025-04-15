using Microsoft.EntityFrameworkCore;

namespace RBC_Employee_API_MVC.Models
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Employees.Count() == 0)
            {
                context.Employees.AddRange(
                    new Employee
                    {
                        EmployeeName = "John Doe",
                        HourlyRate = 15.0M,
                        HoursWorked = 10.5
                    },
                    new Employee
                    {
                        EmployeeName = "Little Jack Horner",
                        HourlyRate = 17.0M,
                        HoursWorked = 10.0
                    },
                    new Employee
                    {
                        EmployeeName = "Socretes",
                        HourlyRate = 20.0M,
                        HoursWorked = 10.0
                    },
                    new Employee
                    {
                        EmployeeName = "Plato",
                        HourlyRate = 10.0M,
                        HoursWorked = 35.0
                    },
                   new Employee
                   {
                       EmployeeName = "Hagel",
                       HourlyRate = 20.0M,
                       HoursWorked = 10.0
                   },
                    new Employee
                    {
                        EmployeeName = "Voltair",
                        HourlyRate = 25.0M,
                        HoursWorked = 20.0
                    },
                    new Employee
                    {
                        EmployeeName = "Karl Marx",
                        HourlyRate = 27.5M,
                        HoursWorked = 20.0
                    },
                    new Employee
                    {
                        EmployeeName = "Fredrick Engles",
                        HourlyRate = 19.5M,
                        HoursWorked = 20.0
                    },
                    new Employee
                    {
                        EmployeeName = "Bertrand Russell",
                        HourlyRate = 11.5M,
                        HoursWorked = 35.5
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
