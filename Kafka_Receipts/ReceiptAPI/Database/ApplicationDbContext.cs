namespace ReceiptAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category>      Categories  { get; set; }
        public DbSet<Ingredient>    Ingredients { get; set; }
        public DbSet<Receipt>       Receipts { get; set; }
        public DbSet<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
