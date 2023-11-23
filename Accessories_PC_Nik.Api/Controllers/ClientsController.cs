using Accessories_PC_Nik.Services.Contracts.Interface;
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
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientsController"/>
        /// </summary>
        public ClientsController(IClientsService clientsService,
            IMapper mapper)
        {
            this.clientsService = clientsService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех клиентов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientsService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ClientsResponse>>(result));
        }

        /// <summary>
        /// Получает список клиента по Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsService.GetByIdAsync(id,cancellationToken);
            if(item == null) return NotFound($"Не удалось найти клиента с идентификатором {id}");
            return Ok(mapper.Map<ClientsResponse>(item));
        }
    }
}
