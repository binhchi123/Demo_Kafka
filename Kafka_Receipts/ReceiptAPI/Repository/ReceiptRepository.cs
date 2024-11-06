
namespace ReceiptAPI.Repository
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        public ReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddReceiptAsync(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
        }
        public async Task AddReceiptDetailAsync(ReceiptDetail receiptDetail)
        {
            _context.ReceiptDetails.Add(receiptDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReceiptAsync(Receipt receipt)
        {
            _context?.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReceiptDetailsAsync(int receiptId)
        {
            var rds = await _context.ReceiptDetails.Where(rd => rd.ReceiptId == receiptId).ToListAsync();
            if (rds.Count > 0)
            {
                foreach(var rd in rds)
                {
                    var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.IngredientId == rd.IngredientId);
                    if (ingredient != null) {
                        ingredient.Quantity += rd.QuantitySell;
                        _context.Ingredients.Update(ingredient);
                    }
                }
                _context.ReceiptDetails.RemoveRange(rds);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Receipt>> GetAllReceiptAsync()
        {
            return await _context.Receipts.ToListAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int receiptId)
        {
            return await _context.Receipts.FirstOrDefaultAsync(r => r.ReceiptId == receiptId);
        }

        public async Task<List<ReceiptDetail>> GetReceiptDetailsByReceiptIdAsync(int receiptId)
        {
            return await _context.ReceiptDetails.Where(rd => rd.ReceiptId == receiptId).ToListAsync();
        }

        public async Task UpdateReceiptAsync(Receipt receipt)
        {
            _context.Receipts.Update(receipt);
            await _context.SaveChangesAsync();
        }
    }
}
