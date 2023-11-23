using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Workers")]
    public class WorkersController : Controller
    {
        private readonly IWorkersService workersService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkersController"/>
        /// </summary>
        public WorkersController(IWorkersService workersService,
            IMapper mapper)
        {
            this.workersService = workersService;
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


            return Ok(mapper.Map<IEnumerable<WorkersResponse>>(item));
        }

    }
}
