namespace CourseAPI.Service
{
    public class CourseDayService : ICourseDayService
    {
        private readonly ILogger<CourseDayService> _logger;
        private readonly CourseDayMemory           _memory;
        private readonly IProducer                 _producer;

        public CourseDayService(ILogger<CourseDayService> logger, CourseDayMemory memory, IProducer producer)
        {
            _logger   = logger;
            _memory   = memory;
            _producer = producer;
        }
        public CourseDay DeleteCourseDay(int id)
        {
            if (_memory.CourseDaysMemory.TryGetValue(id.ToString(), out var courseDay))
            {
                _memory.CourseDaysMemory.Remove(id.ToString());
                var request = new DeleteCourseDayRequest { CourseDayId = courseDay.CourseDayId };
                _producer.Produce(request);
                return courseDay;
            }
            else
            {
                _logger.LogWarning($"CourseDay with ID {id} not found");
                return null;
            }
        }

        public IEnumerable<CourseDay> GetAllCourseDays()
            => _memory.CourseDaysMemory.Values.ToList();

        public CourseDay InsertCourseDay(CourseDay insertCourseDay)
        {
            if (insertCourseDay.CourseId <= 0)
            {
                throw new ArgumentException($"Invalid CourseId: {insertCourseDay.CourseId}");
            }
            _memory.CourseDaysMemory.Add(insertCourseDay.CourseDayId.ToString(), insertCourseDay);
            var request = new InsertCourseDayRequest
            {
                CourseDayId = insertCourseDay.CourseDayId,
                CourseId    = insertCourseDay.CourseId,
                Content     = insertCourseDay.Content,
                Note        = insertCourseDay.Note,
            };
            _producer.Produce(request);
            return insertCourseDay;
        }

        public CourseDay UpdateCourseDay(CourseDay updateCourseDay)
        {
            if (updateCourseDay.CourseId <= 0)
            {
                throw new ArgumentException($"Invalid CourseId: {updateCourseDay.CourseId}");
            }
            if (_memory.CourseDaysMemory.TryGetValue(updateCourseDay.CourseDayId.ToString(), out var existingCourseDay))
            {
                _memory.CourseDaysMemory[updateCourseDay.CourseDayId.ToString()] = updateCourseDay;
                var request = new UpdateCourseDayRequest
                {
                    CourseDayId  = updateCourseDay.CourseDayId,
                    CourseId     = updateCourseDay.CourseId, 
                    Content      = updateCourseDay.Content,
                    Note         = updateCourseDay.Note,
                };
                _producer.Produce(request);
            }
            else
            {
                _logger.LogWarning($"CourseDay with ID {updateCourseDay.CourseDayId} not found");
                return null;
            }
            return updateCourseDay;
        }
    }
}
