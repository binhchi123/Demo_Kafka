namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDaysController : ControllerBase
    {
        private readonly ILogger<CourseDaysController> _logger;
        private readonly ICourseDayService             _courseDayService;

        public CourseDaysController(ILogger<CourseDaysController> logger, ICourseDayService courseDayService)
        {
            _logger           = logger;
            _courseDayService = courseDayService;
        }

        [HttpGet]
        public ActionResult<List<CourseDay>> GetAllCourseDay()
        {
            try
            {
                var courseDays = _courseDayService.GetAllCourseDays();
                return Ok(courseDays);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all courseDays");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDay> GetCourseDayById(int id)
        {
            try
            {
                var courseDay = _courseDayService.GetAllCourseDays().FirstOrDefault(c => c.CourseDayId == id);
                if (courseDay == null)
                {
                    return NotFound("CourseDayId not found");
                }
                return Ok(courseDay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching courseDay by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<CourseDay> InsertCourseDay(CourseDay insertCourseDay)
        {
            try
            {
                return _courseDayService.InsertCourseDay(insertCourseDay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error insert courseDay");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public ActionResult<CourseDay> UpdateCourseDay(CourseDay updateCourseDay)
        {
            try
            {
                var caourseDay = _courseDayService.UpdateCourseDay(updateCourseDay);
                if (caourseDay == null)
                {
                    return NotFound("CourseDayId not found");
                }
                return Ok(caourseDay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update courseDay");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<CourseDay> DeleteCourseDay(int id)
        {
            try
            {
                var caourseDay = _courseDayService.DeleteCourseDay(id);
                if (caourseDay == null)
                {
                    return NotFound("CourseDayId not found");
                }
                return Ok(caourseDay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error delete courseDay");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
