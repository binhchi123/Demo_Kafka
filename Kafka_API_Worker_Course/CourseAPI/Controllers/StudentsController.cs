namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetAllStudent()
        {
            try
            {
                var students = _studentService.GetAllStudent();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all student");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            try
            {
                var student = _studentService.GetAllStudent().FirstOrDefault(s => s.StudentId == id);
                if (student == null)
                {
                    return NotFound("StudentId not found");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching student by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<Student> InsertStudent(Student insertStudent)
        {
            try
            {
                return _studentService.InsertStudent(insertStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error insert student");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public ActionResult<Student> UpdateStudent(Student updateStudent)
        {
            try
            {
                var student = _studentService.UpdateStudent(updateStudent);
                if (student == null)
                {
                    return NotFound("StudentId not found");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update student");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            try
            {
                var student = _studentService.DeleteStudent(id);
                if (student == null)
                {
                    return NotFound("StudentId not found");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error delete student");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
