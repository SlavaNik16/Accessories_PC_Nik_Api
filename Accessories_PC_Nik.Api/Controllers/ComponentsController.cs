using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

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
                typeComponents = x.typeComponents,
                Description = x.Description,
                MaterialType = x.MaterialType,
                Price = x.Price,

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
                typeComponents = item.typeComponents,
                Description = item.Description,
                MaterialType = item.MaterialType,
                Price = item.Price,
            });
        }
    }
}
