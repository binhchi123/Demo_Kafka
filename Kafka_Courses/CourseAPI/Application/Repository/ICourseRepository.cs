namespace CourseAPI.Application.Repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourseAsync();
        Task<Course> GetCourseByIdAsync(int courseId);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(Course course);
    }
}
