namespace KafkaConsumerWorker.Application.Models
{
    public class ReceiptDetail
    {
        public int ReceiptDetailId       { get; set; }
        public int IngredientId          { get; set; }
        public int ReceiptId             { get; set; }
        public int QuantitySell          { get; set; }
    }
}
