namespace StudentAPI.Requests
{
    public class UpdateClassRequest
    {
        public int    ClassId         { get; set; }
        public string ClassName       { get; set; }
        public int    NumberOfStudent { get; set; }
    }
}
