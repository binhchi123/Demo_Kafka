namespace KafkaConsumerWorker.Application.Repository
{
    public interface IReceiptRepository
    {
        Task<List<Receipt>> GetAllReceiptAsync();
        Task<Receipt> GetReceiptByIdAsync(int receiptId);
        Task<List<ReceiptDetail>> GetReceiptDetailsByReceiptIdAsync(int receiptId);
        Task DeleteReceiptDetailsAsync(int receiptId);
        Task AddReceiptAsync(Receipt receipt);
        Task AddReceiptDetailAsync(ReceiptDetail receiptDetail);
        Task UpdateReceiptAsync(Receipt receipt); 
        Task DeleteReceiptAsync(Receipt receipt);
    }
}
