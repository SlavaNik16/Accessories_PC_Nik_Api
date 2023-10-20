using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientsService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new ClientsResponse
            {
                Id = x.Id,
                FI0 = $"{x.Name} {x.Surname} {x.Patronymic}",
                Phone = x.Phone ?? string.Empty
            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsService.GetByIdAsync(id,cancellationToken);
            if(item == null) return NotFound($"Не удалось найти клиента с идентификатором {id}");


            return Ok(new ClientsResponse
            {
                Id = item.Id,
                FI0 = $"{item.Name} {item.Surname} {item.Patronymic}",
                Phone = item.Phone ?? string.Empty
            });
        }
    }
}
