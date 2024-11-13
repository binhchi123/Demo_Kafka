namespace Kafka_Student_API.Core.Models
{
    public class Student
    {
        [Key]
        public int               StudentId     { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string            Name          { get; set; }
        public DateTime          Birthday      { get; set; }
        public string            Address       { get; set; }
    }
}
