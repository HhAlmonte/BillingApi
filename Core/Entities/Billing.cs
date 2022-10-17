namespace Core.Entities
{
    public class Billing : BaseClass
    {
        public string NamePerson { get; set; }
        public string LastNamePerson { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
