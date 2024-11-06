namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseDaysController : ControllerBase
    {
        private readonly CourseDbContext _context;
        private readonly ICourseDayService _courseDayService;

        public CourseDaysController(CourseDbContext context, ICourseDayService courseDayService)
        {
            _context = context;
            _courseDayService = courseDayService;
        }

        // GET: api/CourseDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDay>>> GetCoursesDays()
        {
            return await _courseDayService.GetAllCourseDayAsync();
        }

        // GET: api/CourseDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDay>> GetCourseDay(int id)
        {
            return await _courseDayService.GetCourseDayByIdAsync(id);
        }

        // PUT: api/CourseDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseDay(int id, CourseDayDTO updateCourseDayDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _courseDayService.UpdateCourseDayAsync(updateCourseDayDTO);
                return Ok("Ngày học đã được cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật ngày học: {ex.Message}");
            }
        }

        // POST: api/CourseDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDay>> PostCourseDay(CourseDayDTO createCourseDayDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _courseDayService.AddCourseDayAsync(createCourseDayDTO);
                return Ok("Ngày học đã được thêm thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm ngày học: {ex.Message}");
            }
        }

        // DELETE: api/CourseDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseDay(int id)
        {
            try
            {
                await _courseDayService.DeleteCourseDayAsync(id);
                return Ok("Ngày học đã được xóa thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa ngày học: {ex.Message}");
            }
        }

        private bool CourseDayExists(int id)
        {
            return _context.CoursesDays.Any(e => e.CourseDayId == id);
        }
    }
}
