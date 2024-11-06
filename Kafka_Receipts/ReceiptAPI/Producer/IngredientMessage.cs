namespace ReceiptAPI.Producer
{
    public class IngredientMessage
    {
        public string     Type       { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
