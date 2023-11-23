using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Delivery")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService deliveryService;
        private readonly IMapper mapper;
        public DeliveryController(IDeliveryService deliveryService,
                IMapper mapper)
        {
            this.deliveryService = deliveryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await deliveryService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DeliveryModel>>(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await deliveryService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти доставку с идентификатором {id}");


            return Ok(mapper.Map<DeliveryModel>(item));
        }
    }
}
