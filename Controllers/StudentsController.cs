using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learn.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    [HttpGet]
    public IActionResult GetAllStudents()
    {
      string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };
      return Ok(studentNames);
    }
  }
}