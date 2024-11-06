namespace CourseAPI.Application.Repository
{
    public interface ICourseDayRepository
    {
        Task<List<CourseDay>> GetAllCourseDayAsync();
        Task<CourseDay> GetCourseDayByIdAsync(int courseDayId);
        Task<int> CountByCourseIdAsync(int courseId);
        Task AddCourseDayAsync(CourseDay courseDay);
        Task UpdateCourseDayAsync(CourseDay courseDay);
        Task DeleteCourseDayAsync(CourseDay courseDay);
    }
}
