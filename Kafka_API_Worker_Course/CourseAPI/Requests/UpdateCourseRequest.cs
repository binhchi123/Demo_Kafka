namespace CourseAPI.Requests
{
    public class UpdateCourseRequest
    {
        public int      CourseId    { get; set; }
        public string   CourseName  { get; set; }
        public string   Description { get; set; }
        public int      Tuition     { get; set; }
        public DateTime StartDay    { get; set; } = DateTime.Now;
        public DateTime EndDay      { get; set; } = DateTime.Now;
    }
}
