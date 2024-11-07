namespace CourseAPI.Infrastructure.InMemorys
{
    public class StudentMemory
    {
        public Dictionary<string , Student> StudentsMemory { get; set; }
        public StudentMemory()
        {
            StudentsMemory = new Dictionary<string , Student>();
        }
    }
}
