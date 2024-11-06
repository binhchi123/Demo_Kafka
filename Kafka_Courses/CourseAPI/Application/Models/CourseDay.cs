namespace CourseAPI.Application.Models
{
    public class CourseDay
    {
        [Key]
        public int    CourseDayId { get; set; }

        [ForeignKey("Course")]
        public int    CourseId    { get; set; }

        [Required]
        public string Content     { get; set; }
        public string Note        { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }
    }
}
