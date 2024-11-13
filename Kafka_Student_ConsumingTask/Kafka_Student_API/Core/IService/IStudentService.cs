namespace Kafka_Student_API.Core.IService
{
    public interface IStudentService
    {
        List<Student> GetAllStudent();
        Student InsertStudent(Student insertStudent);
        Student UpdateStudent(Student updateStudent);
        Student DeleteStudent(int id);
    }
}
