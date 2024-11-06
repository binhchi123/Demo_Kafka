namespace CourseAPI.Service
{
    public class CourseService : ICourseService
    {
        private readonly ILogger<CourseService> _logger;
        private readonly ICourseRepository      _courseRepository;
        private readonly CourseProducer         _courseProducer;

        public CourseService(ILogger<CourseService> logger, ICourseRepository courseRepository, CourseProducer courseProducer)
        {
            _logger              = logger;
            _courseRepository    = courseRepository;
            _courseProducer      = courseProducer;
        }
        public async Task AddCourseAsync(CourseDTO createCourseDTO)
        {
            if (string.IsNullOrWhiteSpace(createCourseDTO.CourseName))
            {
                throw new ArgumentNullException("Khóa học không thể trống");
            }

            if (createCourseDTO.Tuition < 100000 || createCourseDTO.Tuition > 10000000)
            {
                throw new ArgumentException("Học phí phải lớn hơn 100000 và nhỏ hơn 10000000");
            }

            var newCourse = new Course
            {
                CourseName  = createCourseDTO.CourseName,
                Description = createCourseDTO.Description,
                Tuition     = createCourseDTO.Tuition,
                StartDay    = createCourseDTO.StartDay,
                EndDay      = createCourseDTO.EndDay,
            };

            await _courseRepository.AddCourseAsync(newCourse);
     
            var courseMessage = new CourseMessage
            {
                Type   = "Add",
                Course = newCourse
            };
            await _courseProducer.ProduceCourseMessageAsync("Add", newCourse);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var existingCourse = await _courseRepository.GetCourseByIdAsync(courseId);
            if (existingCourse == null)
            {
                _logger.LogWarning("Khóa học không tồn tại.");
                return;
            }
            await _courseRepository.DeleteCourseAsync(existingCourse);
            var courseMessage = new CourseMessage
            {
                Type   = "Delete",
                Course = existingCourse
            };
            await _courseProducer.ProduceCourseMessageAsync("Delete", existingCourse);
        }

        public async Task<List<Course>> GetAllCourseAsync()
        {
            return await _courseRepository.GetAllCourseAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _courseRepository.GetCourseByIdAsync(courseId);
        }

        public async Task UpdateCourseAsync(CourseDTO updateCourseDTO)
        {
            var courseId = updateCourseDTO.CourseId;
            var existingCourse = await _courseRepository.GetCourseByIdAsync(courseId);
            if (existingCourse == null)
            {
                throw new ArgumentException("Khóa học không tồn tại");
            }
            existingCourse.CourseName  = updateCourseDTO.CourseName;
            existingCourse.Description = updateCourseDTO.Description;
            existingCourse.Tuition     = updateCourseDTO.Tuition;
            existingCourse.StartDay    = updateCourseDTO.StartDay;
            existingCourse.EndDay      = updateCourseDTO.EndDay;
            await _courseRepository.UpdateCourseAsync(existingCourse);
         
            var courseMessage = new CourseMessage
            {
                Type   = "Update",
                Course = existingCourse
            };
            await _courseProducer.ProduceCourseMessageAsync("Update", existingCourse);
        }
    }
}
