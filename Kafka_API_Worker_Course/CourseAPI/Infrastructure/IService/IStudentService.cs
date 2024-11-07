namespace CourseAPI.Infrastructure.IService
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudent();
        Student InsertStudent(Student insertStudent);
        Student UpdateStudent(Student updateStudent);
        Student DeleteStudent(int id);
    }
}
