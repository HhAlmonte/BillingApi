namespace WebApi.DTOs.CategoryDtos
{
    public class ResponseCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
