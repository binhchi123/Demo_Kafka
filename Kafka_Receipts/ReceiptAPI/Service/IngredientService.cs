namespace ReceiptAPI.Service
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ApplicationDbContext  _context;
        private readonly KafkaProducer         _producer;
        public IngredientService(IIngredientRepository ingredientRepository, ApplicationDbContext context, KafkaProducer producer)
        {
            _ingredientRepository = ingredientRepository;
            _context              = context;
            _producer             = producer;
        }
        public async Task AddIngredientAsync(IngredientDTO createIngredientDTO)
        {
            var categoryId = await _context.Categories.FindAsync(createIngredientDTO.CategoryId);
            if (categoryId == null)
            {
                throw new ArgumentException("Loại nguyên liệu không tồn tại.");
            }

            if (string.IsNullOrWhiteSpace(createIngredientDTO.IngredientName))
            {
                throw new ArgumentException("Tên nguyên liệu không để trống");
            }

            if (string.IsNullOrWhiteSpace(createIngredientDTO.Unit))
            {
                throw new ArgumentException("Nhập đơn vị tính");
            }

            if (createIngredientDTO.Price <= 0)
            {
                throw new ArgumentException("Giá bán phải lớn hơn 0");
            }

            if (createIngredientDTO.Quantity <= 0)
            {
                throw new ArgumentException("Số lượng kho phải lớn hơn 0");
            }

            var newIngredient = new Ingredient
            {
                CategoryId     = createIngredientDTO.CategoryId,
                IngredientName = createIngredientDTO.IngredientName,
                Price          = createIngredientDTO.Price,
                Unit           = createIngredientDTO.Unit,
                Quantity       = createIngredientDTO.Quantity
            };
            await _ingredientRepository.AddIngredientAsync(newIngredient);
            var ingredientMessage = new IngredientMessage
            {
                Type       = "Add",
                Ingredient = newIngredient
            };
            await _producer.ProduceIngredientMessageAsync("Add", newIngredient);
        }

        public async Task DeleteIngredientAsync(int ingredientId)
        {
            var existingIngredient = await _ingredientRepository.GetIngredientByIdAsync(ingredientId);
            if (existingIngredient == null)
            {
                throw new Exception("Nguyên liệu không tồn tại.");
            }
            await _ingredientRepository.DeleteIngredientAsync(existingIngredient);
            var ingredientMessage = new IngredientMessage
            {
                Type       = "Delete",
                Ingredient = existingIngredient
            };
            await _producer.ProduceIngredientMessageAsync("Delete", existingIngredient);
        }

        public async Task<List<Ingredient>> GetAllIngredientAsync()
        {
            return await _ingredientRepository.GetAllIngredientAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            return await _ingredientRepository.GetIngredientByIdAsync(ingredientId);
        }

        public async Task UpdateIngredientAsync(IngredientDTO updateIngredientDTO)
        {
            var ingredientId = updateIngredientDTO.IngredientId;
            var existingIngredient = await _ingredientRepository.GetIngredientByIdAsync(ingredientId);
            if (existingIngredient == null)
            {
                throw new Exception("Nguyên liệu không tồn tại.");
            }
            existingIngredient.CategoryId     = updateIngredientDTO.CategoryId;
            existingIngredient.IngredientName = updateIngredientDTO.IngredientName;
            existingIngredient.Price          = updateIngredientDTO.Price;
            existingIngredient.Unit           = updateIngredientDTO.Unit;
            existingIngredient.Quantity       = updateIngredientDTO.Quantity;
            await _ingredientRepository.UpdateIngredientAsync(existingIngredient);
            var ingredientMessage = new IngredientMessage
            {
                Type       = "Update",
                Ingredient = existingIngredient
            };
            await _producer.ProduceIngredientMessageAsync("Update", existingIngredient);
        }
    }
}
