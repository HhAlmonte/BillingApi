using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.ProductsDtos;

namespace WebApi.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IGenericPersistence<Product> _productService;
        private readonly IProductService _product;
        private readonly IMapper _mapper;

        public ProductController(IGenericPersistence<Product> productService,
                                 IProductService product,
                                 IMapper mapper)
        {
            _productService = productService;
            _product = product;
            _mapper = mapper;
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ResponseProductDto>> CreateProduct([FromForm] ProductDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            if (productEntity.Stock < 0) return BadRequest("El stock debe ser mayor a 0");

            await _productService.Add(productEntity);
            
            var productToReturn = _mapper.Map<ResponseProductDto>(productEntity);

            return productToReturn;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ResponseProductDto>>> GetProducts()
        {
            var products = await _productService.Get();
            
            var productsToReturn = _mapper.Map<List<ResponseProductDto>>(products);

            return productsToReturn;
        }

        [HttpGet("GetProductsByCategoryId/{id}")]
        public async Task<ActionResult<List<ResponseProductDto>>> GetProductsByCategoryId(int id)
        {
            var products = await _product.GetProductsByCategoryId(id);

            var productsToReturn = _mapper.Map<List<ResponseProductDto>>(products);

            return productsToReturn;
        }

        [HttpGet("GetProductsByBrandId/{id}")]
        public async Task<ActionResult<List<ResponseProductDto>>> GetProductsByBrandId(int id)
        {
            var products = await _product.GetProductsByBrandId(id);

            var productsToReturn = _mapper.Map<List<ResponseProductDto>>(products);

            return productsToReturn;
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ResponseProductDto>> GetProductById(int id)
        {
            var product = await _productService.Get(id);
            
            var productToReturn = _mapper.Map<ResponseProductDto>(product);

            return productToReturn;
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<ResponseProductDto>> UpdateProduct(int id, [FromForm] ProductDto product)
        {
            var productToUpdate = _mapper.Map<Product>(product);

            productToUpdate.Id = id;

            var updatedProduct = await _productService.Update(productToUpdate);

            if (updatedProduct == 0) return BadRequest();

            var productToReturn = _mapper.Map<ResponseProductDto>(productToUpdate);

            return productToReturn;
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<ResponseProductDto>> DeleteProduct(int id)
        {
            var productToDelete = await _productService.Get(id);

            var deletedProduct = await _productService.Delete(productToDelete);

            if (deletedProduct == 0) return BadRequest();

            var productToReturn = _mapper.Map<ResponseProductDto>(productToDelete);

            return productToReturn;
        }
    }
}
