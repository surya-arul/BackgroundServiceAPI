using BackgroundServiceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("GetEmployeeCount")]
        public async Task<IActionResult> GetEmployeeCount()
        {
            var response = await _employeeRepository.GetEmployeesCount();

            return Ok(response);
        }
    }
}
