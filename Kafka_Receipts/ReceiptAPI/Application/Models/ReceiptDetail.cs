namespace ReceiptAPI.Application.Models
{
    public class ReceiptDetail
    {
        [Key]
        public int ReceiptDetailId       { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId          { get; set; }

        [ForeignKey("Receipt")]
        public int ReceiptId             { get; set; }
        public int QuantitySell          { get; set; }

        [JsonIgnore]
        public Ingredient Ingredient { get; set; }

        [JsonIgnore]
        public Receipt    Receipt    { get; set; }
    }
}
