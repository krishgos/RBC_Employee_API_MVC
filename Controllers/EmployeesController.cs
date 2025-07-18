using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RBC_Employee_API_MVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RBC_Employee_API_MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private DataContext context;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(DataContext ctx, ILogger<EmployeesController> logger)
        {
            context = ctx;
            _logger = logger;
        }

        // GET: api/employees
        [HttpGet]
        public IAsyncEnumerable<Employee> GetEmployees()
        {
            _logger.LogInformation($"Get all employees ...");
            return context.Employees.AsAsyncEnumerable();
        }

        // GET: api/employees/{employeeNumber}
        [HttpGet("{employeeNumber}")]
        public async Task<IActionResult> GetProduct(int employeeNumber)
        {
            _logger.LogInformation($"Searching Employees with employeeNumber = {employeeNumber} ...");
            Employee? emp = await context.Employees.FindAsync(employeeNumber);
            if (emp == null)
                return NotFound();
            return Ok(emp);
        }

        /***********************************************************************************
         * Following is an example of running two Tasks Concurrently, when we have multiple Tasks
         * with await, where the tasks are waited sequentially
         * 
         * If employeeNumber1 and employeeNumber2 are independent, and you want to start both calls 
         * concurrently, use Task without await immediately:  
         * **********************************************************************************/

        public async Task<IActionResult> GetSameEmployee(int employeeNumber1, int employeeNumber2)
        {
            _logger.LogInformation($"Searching Employees with employeeNumbers = {employeeNumber1}, {employeeNumber2} ...");

            // Start both tasks concurrently
            var emp1Task = context.Employees.FindAsync(employeeNumber1);
            var emp2Task = context.Employees.FindAsync(employeeNumber2);

            // Await both
            Employee? emp1 = await emp1Task;
            Employee? emp2 = await emp2Task;

            if (emp1 != emp2)
                return NotFound();

            return Ok(emp1); // or emp2, since you're checking they are equal
        }

        // GET: api/employees/search?name=John
        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            _logger.LogInformation($"Searching Employees who have {name} in their EmployeeName");
            var matches = context.Employees.Where(e => e.EmployeeName.ToLower().Contains(name.ToLower())).ToList();
            return Ok(matches);
        }

        // GET: api/employees?page=1&pageSize=50
        [HttpGet("paginated")]
        public IActionResult GetAllEmployeesPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            _logger.LogInformation("Fetching all employees paginated");
            var paginated = context.Employees.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(paginated);
        }

        // POST: api/employees
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Employee>> Create(EmployeeBindingTarget target)
        {
            _logger.LogInformation("Inserting new employee ...");
            Employee emp = target.ToEmployee();
            await context.Employees.AddAsync(emp);
            await context.SaveChangesAsync();
            return Created($"Employee/{emp.EmployeeNumber}", target);
        }

        // PUT: api/employees/{employeeNumber}
        [HttpPut]
        public async Task UpdateProduct(Employee employee)
        {
            _logger.LogInformation($"Updating existing employee {employee.EmployeeName} ...");
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }

        // DELETE: api/employees/{employeeNumber}
        [HttpDelete("{employeeNumber}")]
        public async Task DeleteProduct(int employeeNumber)
        {
            _logger.LogInformation($"Deleting employee with Employee Number = {employeeNumber} ...");
            context.Employees.Remove(new Employee() { EmployeeNumber = employeeNumber });
            await context.SaveChangesAsync();
        }

    }
}
