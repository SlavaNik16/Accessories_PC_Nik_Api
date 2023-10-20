using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentsService componentsService;

        public ComponentsController(IComponentsService componentsService)
        {
            this.componentsService = componentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await componentsService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new ComponentsResponse
            {
                Id = x.Id,
                TypeComponents = x.TypeComponents.GetDisplayName(),
                MaterialType = x.MaterialType.GetDisplayName(),
                Description = x.Description,
                Price   = x.Price,

            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await componentsService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти компонент с идентификатором {id}");


            return Ok(new ComponentsResponse
            {
                Id = item.Id,
                TypeComponents = item.TypeComponents.GetDisplayName(),
                MaterialType = item.MaterialType.GetDisplayName(),
                Description = item.Description,
                Price = item.Price,
            });
        }
    }
}
