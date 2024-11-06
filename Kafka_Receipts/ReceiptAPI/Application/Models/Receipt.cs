namespace ReceiptAPI.Application.Models
{
    public class Receipt
    {
        [Key]
        public int      ReceiptId           { get; set; }
        public DateTime CreatedDate         { get; set; } = DateTime.Now;

        [Required]
        public string   EmployeeName        { get; set; }
        public string   Note                { get; set; }
        public double   TotalAmout          { get; set; }

        [JsonIgnore]
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
