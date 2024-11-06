namespace CourseAPI.Application.Service
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCourseAsync();
        Task<Course> GetCourseByIdAsync(int courseId);
        Task AddCourseAsync(CourseDTO createCourseDTO);
        Task UpdateCourseAsync(CourseDTO updateCourseDTO);
        Task DeleteCourseAsync(int courseId);
    }
}
