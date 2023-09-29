using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accessories_PC_Nik.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                Client_id = x.Client_id,
                ServicesResponse = (x.ServicesModel != null) ?  new ServicesResponse
                {
                    Id = x.ServicesModel.Id,
                    Name = x.ServicesModel.Name,
                    Description = x.ServicesModel.Description,
                    Duration = x.ServicesModel.Duration,
                    Price = x.ServicesModel.Price,
                } : null,
                ComponentsResponse = (x.ComponentsModel != null) ?  new ComponentsResponse
                {
                    Id = x.ComponentsModel.Id,
                    typeComponents = x.ComponentsModel.typeComponents,
                    Description = x.ComponentsModel.Description,
                    MaterialType = x.ComponentsModel.MaterialType,
                    Price = x.ComponentsModel.Price,
                } : null,
                Count = x.Count,
                OrderTime = x.OrderTime,
                DeliveryResponse =new DeliveryResponse
                {
                    Id = x.DeliveryModel.Id,
                    IsDelivery = x.DeliveryModel.IsDelivery,
                    From = x.DeliveryModel.From,
                    To = x.DeliveryModel.To,
                    Price = x.DeliveryModel.Price,
                },
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
                Client_id = item.Client_id,
                ServicesResponse = (item.ServicesModel != null) ?  new ServicesResponse
                {
                    Id = item.ServicesModel.Id,
                    Name = item.ServicesModel.Name,
                    Description = item.ServicesModel.Description,
                    Duration = item.ServicesModel.Duration,
                    Price = item.ServicesModel.Price,
                } : null,
                ComponentsResponse = (item.ComponentsModel != null) ? new ComponentsResponse
                {
                    Id = item.ComponentsModel.Id,
                    typeComponents = item.ComponentsModel.typeComponents,
                    Description = item.ComponentsModel.Description,
                    MaterialType = item.ComponentsModel.MaterialType,
                    Price = item.ComponentsModel.Price,
                } : null,
                Count = item.Count,
                OrderTime = item.OrderTime,
                DeliveryResponse = new DeliveryResponse
                {
                    Id = item.DeliveryModel.Id,
                    IsDelivery = item.DeliveryModel.IsDelivery,
                    From = item.DeliveryModel.From,
                    To = item.DeliveryModel.To,
                    Price = item.DeliveryModel.Price,
                },
                Comment = item.Comment,
            });
        }
    }
}
