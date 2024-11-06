namespace KafkaConsumerWorker.Service
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
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
                CourseName = createCourseDTO.CourseName,
                Description = createCourseDTO.Description,
                Tuition = createCourseDTO.Tuition,
                StartDay = createCourseDTO.StartDay,
                EndDay = createCourseDTO.EndDay,
            };

            await _courseRepository.AddCourseAsync(newCourse);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var existingCourse = await _courseRepository.GetCourseByIdAsync(courseId);
            if (existingCourse == null)
            {
                return;
            }
            await _courseRepository.DeleteCourseAsync(existingCourse);
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
            existingCourse.CourseName = updateCourseDTO.CourseName;
            existingCourse.Description = updateCourseDTO.Description;
            existingCourse.Tuition = updateCourseDTO.Tuition;
            existingCourse.StartDay = updateCourseDTO.StartDay;
            existingCourse.EndDay = updateCourseDTO.EndDay;
            await _courseRepository.UpdateCourseAsync(existingCourse);
        }
    }
}
