using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.Service;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с услугами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService servicesService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServicesController"/>
        /// </summary>
        public ServicesController(IServicesService servicesService,
            IApiValidatorService validatorService,
            IMapper mapper)
        {
            this.servicesService = servicesService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех услуг
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ServicesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await servicesService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ServicesResponse>>(result));
        }

        /// <summary>
        /// Получает услугу по Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServicesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти услугу с идентификатором {id}");


            return Ok(mapper.Map<ServicesResponse>(item));
        }

        /// <summary>
        /// Создаёт новую услугу
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(ServicesResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateServiceRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var serviceRequestModel = mapper.Map<ServiceRequestModel>(request);
            var result = await servicesService.AddAsync(serviceRequestModel, cancellationToken);
            return Ok(mapper.Map<ServicesResponse>(result));
        }

        /// <summary>
        /// Редактирует существующую услугу
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(ServicesResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EditServiceRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<ServiceRequestModel>(request);
            var result = await servicesService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ServicesResponse>(result));
        }

        /// <summary>
        /// Удаляет существующую услугу
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await servicesService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
