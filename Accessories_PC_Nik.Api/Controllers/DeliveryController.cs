using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.Delivery;
using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Implementations;
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
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DeliveryController"/>
        /// </summary>
        public DeliveryController(IDeliveryService deliveryService,
                IApiValidatorService validatorService,
                IMapper mapper)
        {
            this.deliveryService = deliveryService;
            this.validatorService = validatorService;
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

        /// <summary>
        /// Создаёт новую доставку
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(DeliveryResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateDeliveryRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var deliveryRequestModel = mapper.Map<DeliveryRequestModel>(request);
            var result = await deliveryService.AddAsync(deliveryRequestModel, cancellationToken);
            return Ok(mapper.Map<DeliveryResponse>(result));
        }

        /// <summary>
        /// Редактирует существующую доставку
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(DeliveryResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EditDeliveryRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<DeliveryRequestModel>(request);
            var result = await deliveryService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<DeliveryResponse>(result));
        }

        /// <summary>
        /// Удаляет существующую доставку
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await deliveryService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
