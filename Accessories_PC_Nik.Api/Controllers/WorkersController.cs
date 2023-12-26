using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.Worker;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с работниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Workers")]
    public class WorkersController : Controller
    {
        private readonly IWorkersService workersService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkersController"/>
        /// </summary>
        public WorkersController(IWorkersService workersService,
            IApiValidatorService validatorService,
            IMapper mapper)

        {
            this.workersService = workersService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех работников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkersResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await workersService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<WorkersResponse>>(result));
        }

        /// <summary>
        /// Получает работника по Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти сотрудника с идентификатором {id}");


            return Ok(mapper.Map<WorkersResponse>(item));
        }

        /// <summary>
        /// Создаёт нового работника
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(WorkersResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateWorkerRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var workerRequestModel = mapper.Map<WorkerRequestModel>(request);
            var result = await workersService.AddAsync(workerRequestModel, cancellationToken);
            return Ok(mapper.Map<WorkersResponse>(result));
        }

        /// <summary>
        /// Редактирует существующего работника
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(WorkersResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EditWorkerRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<WorkerRequestModel>(request);
            var result = await workersService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<WorkersResponse>(result));
        }

        /// <summary>
        /// Удаляет существующего работника
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await workersService.DeleteAsync(id, cancellationToken);
            return Ok();
        }

    }
}
