namespace CourseAPI.Infrastructure.IService
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course InsertCourse(Course insertCourse);
        Course UpdateCourse(Course updateCourse);
        Course DeleteCourse(int id);
    }
}
