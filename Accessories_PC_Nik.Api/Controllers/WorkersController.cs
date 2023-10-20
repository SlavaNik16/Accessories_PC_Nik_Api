using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : Controller
    {
        private readonly IWorkersService workersService;

        public WorkersController(IWorkersService workersService)
        {
            this.workersService = workersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await workersService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new WorkersResponse
            {
                Id = x.Id,
                Number = x.Number,
                Series = x.Series,
                IssuedAt = x.IssuedAt,
                IssuedBy = x.IssuedBy,
                DocumentType = x.DocumentType.GetDisplayName(),
                AccessLevel = x.AccessLevel.GetDisplayName(),

            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти сотрудника с идентификатором {id}");


            return Ok(new WorkersResponse
            {
                Id = item.Id,
                Number = item.Number,
                Series = item.Series,
                IssuedAt = item.IssuedAt,
                IssuedBy = item.IssuedBy,
                DocumentType = item.DocumentType.GetDisplayName(),
                AccessLevel = item.AccessLevel.GetDisplayName(),
            });
        }

    }
}
