namespace CourseAPI.Infrastructure.IService
{
    public interface ICourseDayService
    {
        IEnumerable<CourseDay> GetAllCourseDays();
        CourseDay InsertCourseDay(CourseDay insertCourseDay);
        CourseDay UpdateCourseDay(CourseDay updateCourseDay);
        CourseDay DeleteCourseDay(int id);
    }
}
