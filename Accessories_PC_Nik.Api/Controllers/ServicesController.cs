using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Services")]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService servicesService;
        private readonly IMapper mapper;

        public ServicesController(IServicesService servicesService,
            IMapper mapper)
        {
            this.servicesService = servicesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await servicesService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ServicesModel>>(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await servicesService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти услугу с идентификатором {id}");


            return Ok(mapper.Map<ServicesModel>(item));
        }
    }
}
