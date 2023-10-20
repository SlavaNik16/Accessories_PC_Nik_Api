using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await orderService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new OrderResponse
            {
                Id = x.Id,
                FIO = $"{x.ClientsModel.Name} {x.ClientsModel.Surname} {x.ClientsModel.Patronymic}",
                Phone = x.ClientsModel.Phone ?? string.Empty,
                NameService = x.ServicesModel?.Name ?? string.Empty,
                TypeComponents =x.ComponentsModel?.TypeComponents.GetDisplayName() ?? string.Empty,
                Count = x.Count,
                From = x.DeliveryModel.From,  
                To = x.DeliveryModel.To,
                Price  = x.DeliveryModel.Price,
                Comment = x.Comment,

            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await orderService.GetByIdAsync(id, cancellationToken);
            if (item == null) return NotFound($"Не удалось найти заказ с идентификатором {id}");


            return Ok(new OrderResponse
            {
                Id = item.Id,
                FIO = $"{item.ClientsModel.Name} {item.ClientsModel.Surname} {item.ClientsModel.Patronymic}",
                Phone = item.ClientsModel.Phone ?? string.Empty,
                NameService = item.ServicesModel?.Name ?? string.Empty,
                TypeComponents = item.ComponentsModel?.TypeComponents.GetDisplayName() ?? string.Empty,
                Count = item.Count,
                From = item.DeliveryModel.From,
                To = item.DeliveryModel.To,
                Price = item.DeliveryModel.Price,
                Comment = item.Comment,
            });
        }
    }
}
