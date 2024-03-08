using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //http://localhost:portNumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] students = { "John", "Jane", "Tom", "Harry" };
            return Ok(students);
        }
    }
}
