namespace ReceiptAPI.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly KafkaProducer       _producer;

        public CategoryService(ICategoryRepository categoryRepository, KafkaProducer producer)
        {
            _categoryRepository = categoryRepository;
            _producer           = producer;
        }
        public async Task AddCategoryAsync(CategoryDTO createCategoryDTO)
        {
            if (string.IsNullOrWhiteSpace(createCategoryDTO.CategoryName))
            {
                throw new ArgumentException("Tên loại không để trống");
            }

            if (string.IsNullOrWhiteSpace(createCategoryDTO.Description))
            {
                throw new ArgumentException("Mô tả không để trống");
            }

            var newCategory = new Category
            {
                CategoryName = createCategoryDTO.CategoryName,
                Description  = createCategoryDTO.Description,
            };
            await _categoryRepository.AddCategoryAsync(newCategory);
            var categoryMessage = new CategoryMessage
            {
                Type     = "Add",
                Category = newCategory
            };
            await _producer.ProduceCategoryMessageAsync("Add", newCategory);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Loại nguyên liệu không tồn tại.");
            }
            await _categoryRepository.DeleteCategoryAsync(existingCategory);
            var categoryMessage = new CategoryMessage
            {
                Type = "Delete",
                Category = existingCategory
            };
            await _producer.ProduceCategoryMessageAsync("Delete", existingCategory);
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllCategoryAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }

        public async Task UpdateCategoryAsync(CategoryDTO updateCategoryDTO)
        {
            var categoryId = updateCategoryDTO.CategoryId;
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new Exception("Loại nguyên liệu không tồn tại.");
            }
            existingCategory.CategoryName = updateCategoryDTO.CategoryName;
            existingCategory.Description  = updateCategoryDTO.Description;
            await _categoryRepository.UpdateCategoryAsync(existingCategory);
            var categoryMessage = new CategoryMessage
            {
                Type = "Update",
                Category = existingCategory
            };
            await _producer.ProduceCategoryMessageAsync("Update", existingCategory);
        }
    }
}
