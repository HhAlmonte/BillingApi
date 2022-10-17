namespace WebApi.DTOs.BillingDtos
{
    public class ResponseBillingDto
    {
        public string NamePerson { get; set; }
        public string LastNamePerson { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}