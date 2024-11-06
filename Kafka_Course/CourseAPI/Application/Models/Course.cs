namespace CourseAPI.Application.Models
{
    public class Course
    {
        [Key]
        public int      CourseId    { get; set; }

        [Required, StringLength(10), MaxLength(10)]
        public string   CourseName  { get; set; }

        [Required]
        public string   Description { get; set; }

        [Required, MaxLength(10000000)]
        public int      Tuition     { get; set; }
        public DateTime StartDay    { get; set; }
        public DateTime EndDay      { get; set; }
    }
}
