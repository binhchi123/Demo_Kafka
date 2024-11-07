namespace CourseAPI.Infrastructure.InMemorys
{
    public class CourseDayMemory
    {
        public Dictionary<string,  CourseDay> CourseDaysMemory { get; set; }
        public CourseDayMemory()
        {
            CourseDaysMemory = new Dictionary<string, CourseDay>();
        }
    }
}
