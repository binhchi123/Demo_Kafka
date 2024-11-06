namespace KafkaConsumerWorker.Service
{
    public class CourseDayService : ICourseDayService
    {
        private readonly ICourseDayRepository _courseDayRepository;
        private readonly ICourseRepository    _courseRepository;

        public CourseDayService(ICourseDayRepository courseDayRepository, ICourseRepository courseRepository)
        {
            _courseDayRepository = courseDayRepository;
            _courseRepository    = courseRepository;
        }
        public async Task AddCourseDayAsync(CourseDayDTO createCourseDayDTO)
        {
            var courseId = await _courseRepository.GetCourseByIdAsync(createCourseDayDTO.CourseId);
            if (courseId == null)
            {
                throw new ArgumentException("Khóa học không tồn tại");
            }
            if (string.IsNullOrWhiteSpace(createCourseDayDTO.Content))
            {
                throw new ArgumentNullException("Nội dung không để trống");
            }
            if (string.IsNullOrWhiteSpace(createCourseDayDTO.Note))
            {
                throw new ArgumentNullException("Ghi chú không để trống");
            }
            var courseDay = await _courseDayRepository.CountByCourseIdAsync(createCourseDayDTO.CourseId);
            if (courseDay >= 15)
            {
                throw new ArgumentException("Mỗi khóa học chỉ có tối đa 15 ngày học");
            }
            var newCourseDay = new CourseDay()
            {
                CourseId = createCourseDayDTO.CourseId,
                Content  = createCourseDayDTO.Content,
                Note     = createCourseDayDTO.Note,
            };
            await _courseDayRepository.AddCourseDayAsync(newCourseDay);
        }

        public async Task DeleteCourseDayAsync(int courseDayId)
        {
            var existingCourseDay = await _courseDayRepository.GetCourseDayByIdAsync(courseDayId);
            if (existingCourseDay == null)
            {
                throw new ArgumentException("Ngày học không tồn tại");
            }
            await _courseDayRepository.DeleteCourseDayAsync(existingCourseDay);
        }

        public async Task<List<CourseDay>> GetAllCourseDayAsync()
        {
            return await _courseDayRepository.GetAllCourseDayAsync();
        }

        public async Task<CourseDay> GetCourseDayByIdAsync(int courseDayId)
        {
            return await _courseDayRepository.GetCourseDayByIdAsync(courseDayId);
        }

        public async Task UpdateCourseDayAsync(CourseDayDTO updateCourseDayDTO)
        {
            var courseDayId = updateCourseDayDTO.CourseDayId;
            var existingCourseDay = await _courseDayRepository.GetCourseDayByIdAsync(courseDayId);
            if (existingCourseDay == null)
            {
                throw new ArgumentException("Ngày học không tồn tại");
            }
            existingCourseDay.CourseId = updateCourseDayDTO.CourseId;
            existingCourseDay.Content  = updateCourseDayDTO.Content;
            existingCourseDay.Note     = updateCourseDayDTO.Note;
            await _courseDayRepository.UpdateCourseDayAsync(existingCourseDay);
        }
    }
}
