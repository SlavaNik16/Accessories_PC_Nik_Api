using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.Component;
using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Implementations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с компонентами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Components")]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentsService componentsService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ComponentsController"/>
        /// </summary>
        public ComponentsController(IComponentsService componentsService,
             IApiValidatorService validatorService,
            IMapper mapper)
        {
            this.componentsService = componentsService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех компонентов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ComponentsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await componentsService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ComponentsResponse>>(result));
        }

        /// <summary>
        /// Получает компонент по Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComponentsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await componentsService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти компонент с идентификатором {id}");
            return Ok(mapper.Map<ComponentsResponse>(item));
        }

        /// <summary>
        /// Создаёт новый компонент
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(ComponentsResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateComponentRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var componentRequestModel = mapper.Map<ComponentRequestModel>(request);
            var result = await componentsService.AddAsync(componentRequestModel, cancellationToken);
            return Ok(mapper.Map<ComponentsResponse>(result));
        }

        /// <summary>
        /// Редактирует существующий компонент
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(ComponentsResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EditComponentRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<ComponentRequestModel>(request);
            var result = await componentsService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ComponentsResponse>(result));
        }

        /// <summary>
        /// Удаляет существующий компонент
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await componentsService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
