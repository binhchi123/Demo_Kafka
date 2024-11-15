namespace Kafka_Employee_API.Core.Models
{
    public class Employee
    {
        [Key]
        public int        EmployeeId    { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string     Name          { get; set; }
        public DateTime   Birthday      { get; set; }
        public string     PhoneNumber   { get; set; }
        public string     Address       { get; set; }
        public string     Email         { get; set; }
    }
}
