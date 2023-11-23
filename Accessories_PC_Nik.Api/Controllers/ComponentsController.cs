using Accessories_PC_Nik.Services.Contracts.Interface;
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
        private readonly IMapper mapper;

        public ComponentsController(IComponentsService componentsService,
            IMapper mapper)
        {
            this.componentsService = componentsService;
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
    }
}
