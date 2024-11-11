namespace StudentAPI.Infrastructure.IService
{
    public interface IClassService
    {
        IEnumerable<Class> GetAllClass();
        Class InsertClass(Class insertClass);
        Class UpdateClass(Class updateClass);
        Class DeleteClass(int id);
    }
}
