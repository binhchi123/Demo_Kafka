namespace KafkaConsumerWorker.Application.Service
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllIngredientAsync();
        Task<Ingredient> GetIngredientByIdAsync(int ingredientId);
        Task AddIngredientAsync(IngredientDTO createIngredientDTO);
        Task UpdateIngredientAsync(IngredientDTO updateIngredientDTO);
        Task DeleteIngredientAsync(int ingredientId);
    }
}
