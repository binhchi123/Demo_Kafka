namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ILogger<ClassesController> _logger;
        private readonly IClassService              _classService;

        public ClassesController(ILogger<ClassesController> logger, IClassService classService)
        {
            _logger       = logger;
            _classService = classService;
        }

        [HttpGet]
        public ActionResult<List<Class>> GetAllClass()
        {
            try
            {
                var classes = _classService.GetAllClass();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all class");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public ActionResult<Class> InsertClass(Class insertClass)
        {
            try
            {
                return _classService.InsertClass(insertClass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error insert class");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public ActionResult<Class> UpdateClass(Class updateClass)
        {
            try
            {
                var classes = _classService.UpdateClass(updateClass);
                if (classes == null)
                {
                    return NotFound("ClassId not found");
                }
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error update class");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Class> DeleteClass(int id)
        {
            try
            {
                var classes = _classService.DeleteClass(id);
                if (classes == null)
                {
                    return NotFound("ClassId not found");
                }
                return Ok(classes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error delete class");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
