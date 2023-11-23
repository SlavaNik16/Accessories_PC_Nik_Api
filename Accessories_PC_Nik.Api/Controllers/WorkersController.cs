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

        public WorkersController(IWorkersService workersService,
            IMapper mapper)
        {
            this.workersService = workersService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await workersService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<WorkersModel>>(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти сотрудника с идентификатором {id}");


            return Ok(mapper.Map<IEnumerable<WorkersModel>>(item));
        }

    }
}
