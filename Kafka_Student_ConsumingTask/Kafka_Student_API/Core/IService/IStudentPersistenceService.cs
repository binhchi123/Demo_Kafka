namespace Kafka_Student_API.Core.IService
{
    public interface IStudentPersistenceService
    {
        Task<Student> InsertStudent(Student insertStudent);
        Task<Student> UpdateStudent(Student updateStudent);
        Task<Student> DeleteStudent(int id);
    }
}
