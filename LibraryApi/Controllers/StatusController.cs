using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class StatusController : Controller
    {
        ISystemTime systemTime;
        IConfiguration config;

        public StatusController(ISystemTime systemTime, IConfiguration config)
        {
            this.systemTime = systemTime;
            this.config = config;
        }

        // GET / status -> 200 ok
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            var response = new GetStatusResponse
            {
                Message = "Everything is golden!  " + config.GetValue<string>("appName"),
                CheckedBy = "Joe Schmidt",
                WhenLastChecked = systemTime.GetCurrent()
            };
            return Ok(response);
        }

        [HttpGet("employees/{employeeId:int:min(1)}/salary")]
        public ActionResult GetEmployeeSalary(int employeeId)
        {
            return Ok($"The Employee {employeeId} has a salary of $72,00.00");
        }

        [HttpGet("employees")]
        public ActionResult GetEmployees([FromQuery]string dept = "All")
        {
            return Ok($"Returning employees for department {dept}");
        }

        [HttpGet("whoami")]
        public ActionResult WhoAmi([FromHeader(Name = "User-Agent")] string agent)
        {
            return Ok($"I see you are running {agent}");
        }

        [HttpPost("employees")]
        public ActionResult HireEmployee([FromBody] EmployeeCreateRequest employeeToHire, [FromHeader(Name = "Content-Type")] string contentType)
        {
            return Ok($"Hiring {employeeToHire.LastName} as a {employeeToHire.Department} \r {contentType}");
        }
    }

    public class GetStatusResponse
    {
        public string Message { get; set; }
        public string CheckedBy { get; set; }
        public DateTime WhenLastChecked { get; set; }
    }
    public class EmployeeCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }

}
