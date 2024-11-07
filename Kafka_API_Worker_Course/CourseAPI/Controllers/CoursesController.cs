namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService            _courseService;

        public CoursesController(ILogger<CoursesController> logger, ICourseService courseService)
        {
            _logger        = logger;
            _courseService = courseService;
        }

        [HttpGet]
        public ActionResult<List<Course>> GetAllCourse()
        {
            try
            {
                var courses = _courseService.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all courses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            try
            {
                var course = _courseService.GetAllCourses().FirstOrDefault(c => c.CourseId == id);
                if (course == null)
                {
                    return NotFound("CourseId not found");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching course by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<Course> InsertCourse(Course insertCourse)
        {
            try
            {
                return _courseService.InsertCourse(insertCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error insert courses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public ActionResult<Course> UpdateCourse(Course updateCourse)
        {
            try
            {
                var caourse = _courseService.UpdateCourse(updateCourse);
                if (caourse == null)
                {
                    return NotFound("CourseId not found");
                }
                return Ok(caourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update courses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourse(int id)
        {
            try
            {
                var caourse = _courseService.DeleteCourse(id);
                if (caourse == null)
                {
                    return NotFound("CourseId not found");
                }
                return Ok(caourse);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error delete courses");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
