using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await deliveryService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new DeliveryResponse
            {
                Id = x.Id,
                From = x.From,
                To = x.To,
                Price = x.Price,
            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await deliveryService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти доставку с идентификатором {id}");


            return Ok(new DeliveryResponse
            {
                Id = item.Id,
                From = item.From,
                To = item.To,
                Price = item.Price,
            });
        }
    }
}
