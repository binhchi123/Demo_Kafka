namespace KafkaConsumerWorker.Service
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository    _receiptRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ApplicationDbContext  _context;

        public ReceiptService(IReceiptRepository receiptRepository, IIngredientRepository ingredientRepository, 
                              ApplicationDbContext context)
        {
            _receiptRepository    = receiptRepository;
            _ingredientRepository = ingredientRepository;
            _context              = context;
        }
        public async Task AddReceiptAsync(ReceiptDTO createReceiptDTO)
        {
            if (!DateTime.TryParse(createReceiptDTO.CreatedDate.ToString(), out var createdDate))
            {
                throw new ArgumentException("Ngày lập không hợp lệ định dạng đúng yyyy/MM/dd");
            }

            if (string.IsNullOrWhiteSpace(createReceiptDTO.EmployeeName))
            {
                throw new ArgumentException("Nhân viên lập không để trống");
            }
            var receipt = new Receipt
            {
                CreatedDate     = createReceiptDTO.CreatedDate,
                EmployeeName    = createReceiptDTO.EmployeeName,
                Note            = createReceiptDTO.Note,
                TotalAmout      = 0
            };

            await _receiptRepository.AddReceiptAsync(receipt);
            double totalAmout = 0;
            foreach (var detail in createReceiptDTO.ReceiptDetails)
            {
                var ingredient = await _ingredientRepository.GetIngredientByIdAsync(detail.IngredientId);
                if (ingredient == null)
                    throw new KeyNotFoundException("Nguyên liệu không tồn tại.");

                if (detail.QuantitySell <= 0)
                    throw new ArgumentException("Nhập số lượng bán");

                if (ingredient.Quantity < detail.QuantitySell)
                    throw new InvalidOperationException("Số lượng tồn kho không đủ");

                var receiptDetail = new ReceiptDetail
                {
                    ReceiptId      = receipt.ReceiptId,
                    IngredientId   = detail.IngredientId,
                    QuantitySell   = detail.QuantitySell
                };

                await _receiptRepository.AddReceiptDetailAsync(receiptDetail);

                ingredient.Quantity -= detail.QuantitySell;
                totalAmout += detail.QuantitySell * ingredient.Price;
            }

            receipt.TotalAmout = totalAmout;
            await _receiptRepository.UpdateReceiptAsync(receipt);
        }

        public async Task DeleteReceiptAsync(int receiptId)
        {
            var existingReceipt = await _receiptRepository.GetReceiptByIdAsync(receiptId);
            if (existingReceipt == null)
            {
                throw new Exception("Phiếu thu không tồn tại.");
            }
            await _receiptRepository.DeleteReceiptAsync(existingReceipt);
        }

        public async Task<List<Receipt>> GetAllReceiptAsync()
        {
            return await _receiptRepository.GetAllReceiptAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int receiptId)
        {
            return await _receiptRepository.GetReceiptByIdAsync(receiptId);
        }

        public async Task UpdateReceiptAsync(ReceiptDTO updateReceiptDTO)
        {
            var receiptId = updateReceiptDTO.ReceiptId;
            var existingReceipt = await _receiptRepository.GetReceiptByIdAsync(receiptId);

            if (existingReceipt == null)
            {
                throw new Exception("Phiếu thu không tồn tại.");
            }

            existingReceipt.CreatedDate  = updateReceiptDTO.CreatedDate;
            existingReceipt.EmployeeName = updateReceiptDTO.EmployeeName;
            existingReceipt.Note         = updateReceiptDTO.Note;

            // Xóa các chi tiết cũ của phiếu thu
            var existingReceiptDetails = await _receiptRepository.GetReceiptDetailsByReceiptIdAsync(receiptId);
            foreach (var detail in existingReceiptDetails)
            {
                var ingredient = await _ingredientRepository.GetIngredientByIdAsync(detail.IngredientId);
                if (ingredient != null)
                {
                    // Hoàn lại số lượng nguyên liệu vào kho
                    ingredient.Quantity += detail.QuantitySell;
                    await _ingredientRepository.UpdateIngredientAsync(ingredient);
                }
            }

            await _receiptRepository.DeleteReceiptDetailsAsync(receiptId);

            double totalAmount = 0;

            // Thêm các chi tiết mới từ DTO
            foreach (var detail in updateReceiptDTO.ReceiptDetails)
            {
                var ingredient = await _ingredientRepository.GetIngredientByIdAsync(detail.IngredientId);
                if (ingredient == null)
                {
                    throw new KeyNotFoundException("Nguyên liệu không tồn tại.");
                }

                if (detail.QuantitySell <= 0)
                {
                    throw new ArgumentException("Nhập số lượng bán.");
                }

                if (ingredient.Quantity < detail.QuantitySell)
                {
                    throw new InvalidOperationException("Số lượng tồn kho không đủ.");
                }

                // Thêm chi tiết mới vào phiếu thu
                var receiptDetail = new ReceiptDetail
                {
                    ReceiptId = receiptId,
                    IngredientId = detail.IngredientId,
                    QuantitySell = detail.QuantitySell
                };

                await _receiptRepository.AddReceiptDetailAsync(receiptDetail);

                // Cập nhật tồn kho
                ingredient.Quantity -= detail.QuantitySell;
                await _ingredientRepository.UpdateIngredientAsync(ingredient);

                // Cộng dồn số tiền
                totalAmount += detail.QuantitySell * ingredient.Price;
            }

            // Cập nhật lại tổng số tiền của phiếu thu
            existingReceipt.TotalAmout = totalAmount;
            await _receiptRepository.UpdateReceiptAsync(existingReceipt);
        }
    }
}
