using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                Email = x.Email,
                Balance = x.Balance,

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
                Surname = item.Surname,
                Name = item.Name,
                Patronymic = item.Patronymic,
                Phone = item.Phone,
                Email = item.Email,
                Balance = item.Balance,
            });
        }
    }
}
