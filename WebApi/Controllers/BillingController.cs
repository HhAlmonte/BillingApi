using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.BillingDtos;

namespace WebApi.Controllers
{
    public class BillingController : BaseController
    {
        private readonly IGenericPersistence<Billing> _billingService;
        private readonly IProductService _productService;
        private readonly IBillingService _billing;
        private readonly IMapper _mapper;

        public BillingController(IGenericPersistence<Billing> billingService,
                                 IProductService productService,
                                 IBillingService billing,
                                 IMapper mapper)
        {
            _billingService = billingService;
            _productService = productService;
            _billing = billing;
            _mapper = mapper;
        }

        [HttpPost("CreateBilling")]
        public async Task<ActionResult<ResponseBillingDto>> CreateBilling([FromForm] BillingDto billing)
        {
            var billingToCreate = _mapper.Map<Billing>(billing);

            billingToCreate.Total = await _billing.GetTotal(billing.ProductId, billing.Quantity);

            if (!await _productService.HasStock(billing.ProductId, billing.Quantity))
            {
                return BadRequest("No hay stock suficiente");
            }

            var billingCreated = await _billingService.Add(billingToCreate);
            
            _productService.SubstractStock(billing.ProductId, billing.Quantity);

            var billingToReturn = _mapper.Map<ResponseBillingDto>(billingCreated);
            
            return billingToReturn;
        }

        [HttpGet("GetBillings")]
        public async Task<ActionResult<List<ResponseBillingDto>>> GetBillings()
        {
            var billings = await _billingService.Get();
            
            var billingsToReturn = _mapper.Map<List<ResponseBillingDto>>(billings);

            return billingsToReturn;
        }

        [HttpGet("GetBillingById/{id}")]
        public async Task<ActionResult<ResponseBillingDto>> GetBillingById(int id)
        {
            var billing = await _billingService.Get(id);

            var billingToReturn = _mapper.Map<ResponseBillingDto>(billing);

            return billingToReturn;
        }
        
        [HttpDelete("DeleteBilling/{id}")]
        public async Task<ActionResult> DeleteBilling(int id)
        {
            var billing = await _billingService.Get(id);

            var deleteBilling = await _billingService.Delete(billing);

            if (deleteBilling == 0) return BadRequest();

            return Ok();
        }
    }
}