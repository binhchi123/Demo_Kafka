namespace StudentAPI.Producer
{
    public interface IProducer
    {
        void Produce<TRequest>(TRequest request) where TRequest : class;
    }
}
