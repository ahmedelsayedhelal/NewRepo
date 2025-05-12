using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakan_project.Repository;

namespace Sakan_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        IStudentsRepository studentRepository;
        public StudentsController(IStudentsRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
    }
}
