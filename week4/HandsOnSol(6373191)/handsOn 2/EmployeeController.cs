using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin,POC")]
    [Route("api/emp")]
    [ApiController]
    //[AllowAnonymous]
    //[CustAuthFilter]
    public class EmployeeController : ControllerBase
    {
       
        private List<employee> _employees;

        public EmployeeController()
        {
            _employees = GetStandardEmployeeList();
        }
        private List<employee> GetStandardEmployeeList()
        {
            return new List<employee>
                {
                   new employee{
                    Id = 1,
                    Name = "John Doe",
                    Salary = 50000,
                    Permanent = true,
                    Department = new Department { Id = 1, Name = "IT" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 1, Name = "C#" },
                        new Skill { Id = 2, Name = "ASP.NET" }
                    },
                    DateOfBirth = new DateTime(1990, 1, 1)
                   }
            };
        }


        [HttpGet]

        [ProducesResponseType(typeof(IEnumerable<employee>), 200)]
        public ActionResult<IEnumerable<employee>> GetStandard()
        {
            return Ok(_employees);
        }

        [HttpPost]
        [ProducesResponseType(typeof(employee), 201)]   
  public ActionResult<employee> CreateEmployee([FromBody] employee newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest("Invalid employee data.");
            }
            // Validate required fields
            if (string.IsNullOrWhiteSpace(newEmployee.Name) ||
                newEmployee.Department == null ||
                newEmployee.Skills == null ||
                newEmployee.DateOfBirth == default)
            {
                return BadRequest("Missing required employee fields.");
            }
            newEmployee.Id = _employees.Count + 1;
            _employees.Add(newEmployee);
            return CreatedAtAction(nameof(GetStandard), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(employee), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<employee> UpdateEmployee(int id, [FromBody] employee updatedEmployee)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id.");
            }
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found.");
            }
           
            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Salary = updatedEmployee.Salary;
            existingEmployee.Permanent = updatedEmployee.Permanent;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.Skills = updatedEmployee.Skills;
            existingEmployee.DateOfBirth = updatedEmployee.DateOfBirth;
            return Ok(existingEmployee);
        }

        [HttpDelete("{id}")]    
        [ProducesResponseType(204)]     
        public IActionResult DeleteEmployee(int id)
        {
            var employeeToDelete = _employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete == null)
            {
                return NotFound("Employee not found.");
            }
            _employees.Remove(employeeToDelete);
            return NoContent();
        }

        [HttpGet("throw")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> ThrowException()
        {
            throw new Exception("This is a test exception from GET method.");
        }

        [Authorize]
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {
            return Ok("Only authenticated users can access this");
        }

    }
}

