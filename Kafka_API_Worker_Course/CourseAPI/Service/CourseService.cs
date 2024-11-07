namespace CourseAPI.Service
{
    public class CourseService : ICourseService
    {
        private readonly ILogger<CourseService> _logger;
        private readonly CourseMemory           _memory;
        private readonly IProducer              _producer;

        public CourseService(ILogger<CourseService> logger, CourseMemory memory, IProducer producer)
        {
            _logger   = logger;
            _memory   = memory;
            _producer = producer;
        }
        public Course DeleteCourse(int id)
        {
            if (_memory.CoursesMemory.TryGetValue(id.ToString(), out var course))
            {
                _memory.CoursesMemory.Remove(id.ToString());
                var request = new DeleteCourseRequest { CourseId = course.CourseId };
                _producer.Produce(request);
                return course;
            }
            else
            {
                _logger.LogWarning($"Course with ID {id} not found");
                return null;
            }
        }

        public IEnumerable<Course> GetAllCourses()
            => _memory.CoursesMemory.Values.ToList();

        public Course InsertCourse(Course insertCourse)
        {
            _memory.CoursesMemory.Add(insertCourse.CourseId.ToString(), insertCourse);
            var request = new InsertCourseRequest
            {
                CourseId    = insertCourse.CourseId,
                CourseName  = insertCourse.CourseName,
                Description = insertCourse.Description,
                Tuition     = insertCourse.Tuition,
                StartDay    = insertCourse.StartDay,
                EndDay      = insertCourse.EndDay,
            };
            _producer.Produce(request);
            return insertCourse;
        }

        public Course UpdateCourse(Course updateCourse)
        {
            if (updateCourse.CourseId <= 0)
            {
                throw new ArgumentException($"Invalid CourseId: {updateCourse.CourseId}");
            }
            if (_memory.CoursesMemory.TryGetValue(updateCourse.CourseId.ToString(), out var existingCourse))
            {
                _memory.CoursesMemory[updateCourse.CourseId.ToString()] = updateCourse;
                var request = new UpdateCourseRequest
                {
                    CourseId    = updateCourse.CourseId,
                    CourseName  = updateCourse.CourseName,
                    Description = updateCourse.Description,
                    Tuition     = updateCourse.Tuition,
                    StartDay    = updateCourse.StartDay,
                    EndDay      = updateCourse.EndDay,
                };
                _producer?.Produce(request);
            }
            else
            {
                _logger.LogWarning($"Course with ID {updateCourse.CourseId} not found");
                return null;
            }
            return updateCourse;
        }
    }
}
