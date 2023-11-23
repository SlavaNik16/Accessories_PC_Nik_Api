using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService clientsService;
        private readonly IMapper mapper;

        public ClientsController(IClientsService clientsService,
            IMapper mapper)
        {
            this.clientsService = clientsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientsService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ClientsModel>>(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsService.GetByIdAsync(id,cancellationToken);
            if(item == null) return NotFound($"Не удалось найти клиента с идентификатором {id}");


            return Ok(mapper.Map<ClientsModel>(item));
        }
    }
}
