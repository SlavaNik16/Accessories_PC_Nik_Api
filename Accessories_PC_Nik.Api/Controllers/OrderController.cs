using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.Order;
using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с заказами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderController"/>
        /// </summary>
        public OrderController(IOrderService orderService,
            IApiValidatorService validatorService,
            IMapper mapper)
        {
            this.orderService = orderService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех заказов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await orderService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<OrderResponse>>(result));
        }

        /// <summary>
        /// Получает заказ по Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти заказ с идентификатором {id}");


            return Ok(mapper.Map<OrderResponse>(item));
        }

        /// <summary>
        /// Создаёт новый заказ
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(OrderResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var orderRequestModel = mapper.Map<OrderRequestModel>(request);
            var result = await orderService.AddAsync(orderRequestModel, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(result));
        }

        /// <summary>
        /// Редактирует существующий заказ
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(OrderResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EditOrderRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<OrderRequestModel>(request);
            var result = await orderService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<OrderResponse>(result));
        }

        /// <summary>
        /// Удаляет существующий заказ
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await orderService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
