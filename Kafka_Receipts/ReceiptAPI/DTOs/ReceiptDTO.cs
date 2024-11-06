namespace ReceiptAPI.DTOs
{
    public class ReceiptDTO
    {
        public int      ReceiptId       { get; set; }
        public DateTime CreatedDate     { get; set; } = DateTime.Now;
        public string   EmployeeName    { get; set; }
        public string   Note            { get; set; }
        public double   TotalAmout      { get; set; }

        public List<ReceiptDetailDTO> ReceiptDetails { get; set; }
    }
}
