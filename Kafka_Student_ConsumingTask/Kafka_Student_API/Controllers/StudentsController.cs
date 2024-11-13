namespace Kafka_Student_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentProducer _studentProducer;
        private readonly IStudentService  _studentService;

        public StudentsController(IStudentProducer studentProducer, IStudentService studentService)
        {
            _studentProducer = studentProducer;
            _studentService  = studentService;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAllStudent()
        {
            return _studentService.GetAllStudent();
        }

        [HttpPost]
        public IActionResult InsertStudent([FromBody] Student insertStudent)
        {
            _studentService.InsertStudent(insertStudent);
            return Ok(new { Status = "InsertStudent message sent to Kafka topic 'student-input'" });
        }

        [HttpPut]
        public  IActionResult UpdateStudent([FromBody] Student updateStudent)
        {
            _studentService.UpdateStudent(updateStudent);
            return Ok(new { Status = "UpdateStudent message sent to Kafka topic 'student-input'" });
        }

        [HttpDelete]
        public IActionResult DeleteStudent([FromBody] int id)
        {
            _studentService.DeleteStudent(id);
            return Ok(new { Status = "DeleteStudent message sent to Kafka topic 'student-input'" });
        }
    }
}
