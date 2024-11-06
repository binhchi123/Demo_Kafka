namespace ReceiptAPI.Producer
{
    public class CategoryMessage
    {

        public string   Type     { get; set; }
        public Category Category { get; set; }
    }
}
