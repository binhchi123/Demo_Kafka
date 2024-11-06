namespace ReceiptAPI.Application.Models
{
    public class Category
    {
        [Key]
        public int    CategoryId               { get; set; }

        [Required,    StringLength(20),        MaxLength(20)]
        public string CategoryName             { get; set; }
        public string Description              { get; set; }

        [JsonIgnore]
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
