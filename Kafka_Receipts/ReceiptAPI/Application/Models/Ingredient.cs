namespace ReceiptAPI.Application.Models
{
    public class Ingredient
    {
        [Key]
        public int    IngredientId       { get; set; }

        [ForeignKey("Category")]
        public int    CategoryId         { get; set; }

        [Required, StringLength(20), MaxLength(20)]
        public string IngredientName     { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
        public double Price              { get; set; }

        [Required, StringLength(10),  MaxLength(10)]
        public string Unit               { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Số lượng kho phải lớn hơn 0")]
        public int    Quantity           { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }
    }
}
