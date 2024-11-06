namespace KafkaConsumerWorker.Application.Service
{
    public interface IReceiptService
    {
        Task<List<Receipt>> GetAllReceiptAsync();
        Task<Receipt> GetReceiptByIdAsync(int receiptId);
        Task AddReceiptAsync(ReceiptDTO createReceiptDTO);
        Task UpdateReceiptAsync(ReceiptDTO updateReceiptDTO);
        Task DeleteReceiptAsync(int receiptId);
    }
}
