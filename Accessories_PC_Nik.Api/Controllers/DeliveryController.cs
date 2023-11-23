using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с доставкой
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Delivery")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService deliveryService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DeliveryController"/>
        /// </summary>
        public DeliveryController(IDeliveryService deliveryService,
                IMapper mapper)
        {
            this.deliveryService = deliveryService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех доставок
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeliveryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await deliveryService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<DeliveryResponse>>(result));
        }

        /// <summary>
        /// Получает доставку по Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeliveryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await deliveryService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти доставку с идентификатором {id}");


            return Ok(mapper.Map<DeliveryResponse>(item));
        }
    }
}
