namespace Kafka_Student_API.Core.InMemorys
{
    public class StudentMemory
    {
        public Dictionary<string, Student> StudentsMemory { get; set; }
        public StudentMemory()
        {
            StudentsMemory = new Dictionary<string, Student>();
        }
    }
}
