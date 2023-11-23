using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await componentsService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ComponentsModel>>(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await componentsService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти компонент с идентификатором {id}");


            return Ok(mapper.Map<ComponentsModel>(item));
        }
    }
}
