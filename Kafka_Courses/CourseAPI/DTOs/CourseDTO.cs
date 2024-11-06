namespace CourseAPI.DTOs
{
    public class CourseDTO
    {
        public int      CourseId    { get; set; }
        public string   CourseName  { get; set; }
        public string   Description { get; set; }
        public int      Tuition     { get; set; }
        public DateTime StartDay    { get; set; }
        public DateTime EndDay      { get; set; }
    }
}
