using Accessories_PC_Nik.Api.Attribute;
using Accessories_PC_Nik.Api.Infrastructures.Validator;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.ModelsRequest.AccessKey;
using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работы с ключами доступа
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "AccessKey")]
    public class AccessKeyController : ControllerBase
    {
        private readonly IAccessKeyService accessKeyService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientsController"/>
        /// </summary>
        public AccessKeyController(IAccessKeyService clientsService,
            IApiValidatorService validatorService,
            IMapper mapper)
        {
            this.accessKeyService = clientsService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех клиентов
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<AccessKeyResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await accessKeyService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<AccessKeyResponse>>(result));
        }

        /// <summary>
        /// Получает список клиента по Id
        /// </summary>
        [HttpGet("{id}")]
        [ApiOk(typeof(AccessKeyResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await accessKeyService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти ключ с идентификатором {id}");
            return Ok(mapper.Map<AccessKeyResponse>(item));
        }

        /// <summary>
        /// Создаёт нового клиента
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(AccessKeyResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateAccessKeyRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);

            var accessKeyRequestModel = mapper.Map<AccessKeyRequestModel>(request);
            var result = await accessKeyService.AddAsync(accessKeyRequestModel, cancellationToken);
            return Ok(mapper.Map<AccessKeyResponse>(result));
        }


        /// <summary>
        /// Удаляет существующего клиента
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await accessKeyService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
