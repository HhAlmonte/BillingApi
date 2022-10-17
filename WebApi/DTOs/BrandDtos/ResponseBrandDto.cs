namespace WebApi.DTOs.BrandDtos
{
    public class ResponseBrandDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
