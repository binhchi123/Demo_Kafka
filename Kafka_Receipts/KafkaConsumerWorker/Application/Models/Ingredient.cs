namespace KafkaConsumerWorker.Application.Models
{
    public class Ingredient
    {
        public int    IngredientId       { get; set; }
        public int    CategoryId         { get; set; }
        public string IngredientName     { get; set; }
        public double Price              { get; set; }
        public string Unit               { get; set; }
        public int    Quantity           { get; set; }
    }
}
