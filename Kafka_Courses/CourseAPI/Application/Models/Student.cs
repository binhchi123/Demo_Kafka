namespace CourseAPI.Application.Models
{
    public class Student
    {
        [Key]
        public int      StudentId     { get; set; }

        [ForeignKey("Course")]
        public int      CourseId      { get; set; }

        [Required, StringLength(20), MinLength(2)]
        public string   Name          { get; set; }
        public DateTime Birthday      { get; set; }
        public string   NativeCountry { get; set; }
        public string   Address       { get; set; }
        public string   PhoneNumber   { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }
    }
}
