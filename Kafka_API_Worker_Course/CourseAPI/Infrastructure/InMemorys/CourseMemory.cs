namespace CourseAPI.Infrastructure.InMemorys
{
    public class CourseMemory
    {
        public Dictionary<string, Course> CoursesMemory { get; set; }
        public CourseMemory()
        {
            CoursesMemory = new Dictionary<string, Course>();
        }
    }
}
