using Microsoft.EntityFrameworkCore;

namespace RBC_Employee_API_MVC.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
        : base(opts) { }
        public DbSet<Employee> Employees => Set<Employee>();
    }
}
