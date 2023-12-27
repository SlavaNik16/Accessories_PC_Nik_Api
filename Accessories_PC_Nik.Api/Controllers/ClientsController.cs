using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.Client;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с клиентами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService clientsService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientsController"/>
        /// </summary>
        public ClientsController(IClientsService clientsService,
            IApiValidatorService validatorService,
            IMapper mapper)
        {
            this.clientsService = clientsService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех клиентов
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<ClientsResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientsService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ClientsResponse>>(result));
        }

        /// <summary>
        /// Получает список клиента по Id
        /// </summary>
        [HttpGet("{id}")]
        [ApiOk(typeof(ClientsResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти клиента с идентификатором {id}");
            return Ok(mapper.Map<ClientsResponse>(item));
        }

        /// <summary>
        /// Создаёт нового клиента
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(ClientsResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateClientRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var clientRequestModel = mapper.Map<ClientRequestModel>(request);
            var result = await clientsService.AddAsync(clientRequestModel, cancellationToken);
            return Ok(mapper.Map<ClientsResponse>(result));
        }

        /// <summary>
        /// Редактирует существующего клиента
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(ClientsResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EditClientRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var model = mapper.Map<ClientRequestModel>(request);
            var result = await clientsService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ClientsResponse>(result));
        }

        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await clientsService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
