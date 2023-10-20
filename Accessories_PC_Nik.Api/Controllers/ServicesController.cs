using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService servicesService;

        public ServicesController(IServicesService servicesService)
        {
            this.servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await servicesService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new ServicesResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Duration = x.Duration,
                Price = x.Price,

            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти услугу с идентификатором {id}");


            return Ok(new ServicesResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Duration = item.Duration,
                Price = item.Price,
            });
        }
    }
}
