namespace KafkaConsumerWorker.Application.Models
{
    public class Receipt
    {
        public int      ReceiptId           { get; set; }
        public DateTime CreatedDate         { get; set; } = DateTime.Now;
        public string   EmployeeName        { get; set; }
        public string   Note                { get; set; }
        public double   TotalAmout          { get; set; }
    }
}
