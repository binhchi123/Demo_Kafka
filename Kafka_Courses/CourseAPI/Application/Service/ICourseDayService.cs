namespace CourseAPI.Application.Service
{
    public interface ICourseDayService
    {
        Task<List<CourseDay>> GetAllCourseDayAsync();
        Task<CourseDay> GetCourseDayByIdAsync(int courseDayId);
        Task AddCourseDayAsync(CourseDayDTO createCourseDayDTO);
        Task UpdateCourseDayAsync(CourseDayDTO updateCourseDayDTO);
        Task DeleteCourseDayAsync(int courseDayId);
    }
}
