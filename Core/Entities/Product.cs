﻿namespace Core.Entities
{
    public class Product : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        
        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public int Stock { get; set; }
    }
}
