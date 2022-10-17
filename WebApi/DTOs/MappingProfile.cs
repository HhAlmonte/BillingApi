using AutoMapper;
using Core.Entities;
using WebApi.DTOs.BillingDtos;
using WebApi.DTOs.BrandDtos;
using WebApi.DTOs.CategoryDtos;
using WebApi.DTOs.ProductsDtos;

namespace WebApi.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product> ();
            CreateMap<Product, ResponseProductDto>();

            CreateMap<BillingDto, Billing>();
            CreateMap<Billing, ResponseBillingDto>();

            CreateMap<BrandDto, Brand>();
            CreateMap<Brand, ResponseBrandDto>();

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, ResponseCategoryDto>();
        }
    }
}

