namespace Kafka_Student_API.Producer
{
    public interface IStudentProducer
    {
        Task ProduceInsertStudentAsync(InsertStudentRequest student);
        Task ProduceUpdateStudentAsync(UpdateStudentRequest student);
        Task ProduceDeleteStudentAsync(DeleteStudentRequest student);
    }
}
