using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BrandDtos;

namespace WebApi.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IGenericPersistence<Brand> _brandService;
        private readonly IMapper _mapper;

        public BrandController(IGenericPersistence<Brand> brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpPost("CreateBrand")]
        public async Task<ActionResult<ResponseBrandDto>> CreateBrand([FromForm] BrandDto brand)
        {
            var brandToCreate = _mapper.Map<Brand>(brand);

            await _brandService.Add(brandToCreate);

            var brandToReturn = _mapper.Map<ResponseBrandDto>(brandToCreate);

            return brandToReturn;
        }

        [HttpGet("GetBrands")]
        public async Task<ActionResult<List<ResponseBrandDto>>> GetBrands()
        {
            var brands = await _brandService.Get();

            var brandsToReturn = _mapper.Map<List<ResponseBrandDto>>(brands);

            return brandsToReturn;
        }

        [HttpGet("GetBrandById/{id}")]
        public async Task<ActionResult<ResponseBrandDto>> GetBrandById(int id)
        {
            var brand = await _brandService.Get(id);

            var brandToReturn = _mapper.Map<ResponseBrandDto>(brand);

            return brandToReturn;
        }

        [HttpPut("UpdateBrand/{id}")]
        public async Task<ActionResult<ResponseBrandDto>> UpdateBrand([FromForm] int id, BrandDto brand)
        {
            var brandToUpdate = _mapper.Map<Brand>(brand);
            
            brandToUpdate.Id = id;
            
            var updatedBrand = await _brandService.Update(brandToUpdate);

            if (updatedBrand == 0) return BadRequest();

            var brandToReturn = _mapper.Map<ResponseBrandDto>(updatedBrand);

            return brandToReturn;
        }
        
        [HttpDelete("DeleteBrand/{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.Get(id);
            
            var deletedBrand = await _brandService.Delete(brand);

            if (deletedBrand == 0) return BadRequest();

            return Ok();
        }
    }
}
