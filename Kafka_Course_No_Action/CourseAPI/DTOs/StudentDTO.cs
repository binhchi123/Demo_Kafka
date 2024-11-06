namespace CourseAPI.DTOs
{
    public class StudentDTO
    {
        public int      StudentId     { get; set; }
        public int      CourseId      { get; set; }
        public string   Name          { get; set; }
        public DateTime Birthday      { get; set; }
        public string   NativeCountry { get; set; }
        public string   Address       { get; set; }
        public string   PhoneNumber   { get; set; }
    }
}
