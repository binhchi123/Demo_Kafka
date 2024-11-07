namespace CourseAPI.Infrastructure.Models
{
    public class Course
    {
        [Key]
        public int      CourseId    { get; set; }

        [Required, StringLength(10), MaxLength(10)]
        public string   CourseName  { get; set; }

        [Required]
        public string   Description { get; set; }

        [Required, Range(0, 10000000)]
        public int      Tuition     { get; set; }
        public DateTime StartDay    { get; set; } = DateTime.Now;
        public DateTime EndDay      { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<Student>?   Students   { get; set; }

        [JsonIgnore]
        public ICollection<CourseDay>? CourseDays { get; set; }
    }
}
