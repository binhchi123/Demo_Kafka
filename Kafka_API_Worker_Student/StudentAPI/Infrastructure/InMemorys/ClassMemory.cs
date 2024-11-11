namespace StudentAPI.Infrastructure.InMemorys
{
    public class ClassMemory
    {
        public Dictionary<string, Class> ClassesMemory { get; set; }
        public ClassMemory()
        {
            ClassesMemory = new Dictionary<string, Class>();
        }
    }
}
