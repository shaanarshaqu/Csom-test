using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCsom.Manager.Interfaces;

namespace TestCsom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentManager studentManager;
        public StudentController(IStudentManager studentManager) 
        { 
            this.studentManager = studentManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetStdents()
        {
            try
            {
                var res = await studentManager.GetAllData();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
